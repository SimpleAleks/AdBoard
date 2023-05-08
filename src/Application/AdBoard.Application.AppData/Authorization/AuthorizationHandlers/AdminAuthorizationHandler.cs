using System.Security.Claims;
using AdBoard.Application.AppData.Authorization.Requirements.Operation;
using AdBoard.Contracts.Advert;
using Microsoft.AspNetCore.Authorization;

namespace AdBoard.Application.AppData.Authorization.AuthorizationHandlers;

/// <summary>
/// Разрешает права доступа администратора
/// </summary>
public class AdminAuthorizationHandler :
    AuthorizationHandler<OperationRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationRequirement requirement)
    {
        var userRole = context.User.Claims.First(c => c.Type == ClaimTypes.Role).Value;
        if (userRole == Constants.AdminAuthorizationRole) context.Succeed(requirement);
        
        return Task.CompletedTask;
    }
}