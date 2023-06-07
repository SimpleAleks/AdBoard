using System.Security.Claims;
using AdBoard.Application.AppData.Authorization.Requirements.Operation;
using AdBoard.Contracts.Comment;
using Microsoft.AspNetCore.Authorization;

namespace AdBoard.Application.AppData.Authorization.AuthorizationHandlers;

/// <summary>
/// Разрешает доступ, если пользователь владелец комментария
/// </summary>
public class UserIsCommentOwnerAuthorizationHandler : AuthorizationHandler<OperationRequirement, CommentDto>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationRequirement requirement, CommentDto resource)
    {
        var userIdClaim = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
        if (userIdClaim is null) return Task.CompletedTask;
        var userId = Guid.Parse(userIdClaim.Value);

        if (userId == resource.User.Id) context.Succeed(requirement);
        
        return Task.CompletedTask;
    }
}