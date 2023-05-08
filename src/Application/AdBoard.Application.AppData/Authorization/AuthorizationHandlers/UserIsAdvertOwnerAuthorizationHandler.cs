using System.Security.Claims;
using AdBoard.Application.AppData.Authorization.Requirements.Operation;
using AdBoard.Contracts.Advert;
using Microsoft.AspNetCore.Authorization;

namespace AdBoard.Application.AppData.Authorization.AuthorizationHandlers;

/// <summary>
/// Разрешает доступ, если пользователь владелец объявления
/// </summary>
public class UserIsAdvertOwnerAuthorizationHandler :
    AuthorizationHandler<OperationRequirement, AdvertDto>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationRequirement requirement, AdvertDto resource)
    {
        var userIdClaim = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
        if (userIdClaim is null) return Task.CompletedTask;
        var userId = Guid.Parse(userIdClaim.Value);

        if (userId == resource.User.Id) context.Succeed(requirement);
        
        return Task.CompletedTask;
    }
}