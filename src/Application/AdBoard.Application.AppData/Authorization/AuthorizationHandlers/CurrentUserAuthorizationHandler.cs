using System.Security.Claims;
using AdBoard.Application.AppData.Authorization.Requirements.Operation;
using AdBoard.Contracts.User;
using Microsoft.AspNetCore.Authorization;

namespace AdBoard.Application.AppData.Authorization.AuthorizationHandlers;

/// <summary>
/// Отвечает за права доступа пользователя к пользовательским данным
/// </summary>
public class CurrentUserAuthorizationHandler :
    AuthorizationHandler<OperationRequirement, UserDto>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationRequirement requirement, UserDto resource)
    {
        var userIdClaim = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
        if (userIdClaim is null) return Task.CompletedTask;
        var userId = Guid.Parse(userIdClaim.Value);
        
        if (userId == resource.Id) context.Succeed(requirement);

        return Task.CompletedTask;
    }
}