using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AdBoard.Application.AppData.Contexts.Advert.Repositories;
using AdBoard.Application.AppData.Contexts.Authentication.Exceptions;
using AdBoard.Contracts.User;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace AdBoard.Application.AppData.Contexts.Authentication.Services;

using User = AdBoard.Domain.User.User;

/// <inheritdoc cref="IAuthenticationService"/>
public class AuthenticationService : IAuthenticationService
{
    private readonly IUserRepository _userRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;

    public AuthenticationService(
        IUserRepository userRepository, 
        IHttpContextAccessor httpContextAccessor,
        IMapper mapper,
        IConfiguration configuration)
    {
        _userRepository = userRepository;
        _httpContextAccessor = httpContextAccessor;
        _mapper = mapper;
        _configuration = configuration;
    }
    
    public async Task<Guid> Register(CreateUserDto dto, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<User>(dto);
        user.Password = EncryptPassword(dto.Password);
        user.Role = Constants.DefaultAuthorizationRole;
        var result = await _userRepository.Add(user, cancellationToken);
        return result.Id;
    }

    public async Task<string> Login(LoginUserDto dto, CancellationToken cancellationToken)
    {
        var existingUser = await _userRepository.FindWhere(u => u.Login == dto.Login, cancellationToken);
        
        if (existingUser is null) throw new InvalidLoginDataException();
        
        var encryptedPassword = EncryptPassword(dto.Password);
        if (existingUser.Password != encryptedPassword) throw new InvalidLoginDataException();

        var claims = new List<Claim>()
        {
            new(ClaimTypes.NameIdentifier, existingUser.Id.ToString()!),
            new(ClaimTypes.Name, existingUser.Login),
            new(ClaimTypes.Role, existingUser.Role)
        };

        var secretKey = _configuration["Jwt:SecurityKey"]!;
        var issuer = _configuration["Jwt:Issuer"]!;
        var audience = _configuration["Jwt:Audience"]!;

        var token = new JwtSecurityToken(
            claims: claims,
            issuer: issuer,
            audience: audience,
            expires: DateTime.UtcNow.AddHours(6),
            notBefore: DateTime.UtcNow,
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
                SecurityAlgorithms.HmacSha256)
        );

        var result = new JwtSecurityTokenHandler().WriteToken(token);

        return result;
    }

    private string EncryptPassword(string password)
    {
        var md5 = MD5.Create();
        var passBytes = Encoding.UTF8.GetBytes(password);
        var hashBytes = md5.ComputeHash(passBytes);
        return Convert.ToHexString(hashBytes);
    }
}