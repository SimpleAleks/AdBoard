using Microsoft.AspNetCore.Authorization;

namespace AdBoard.Application.AppData.Authorization.Requirements.Operation;

/// <summary>
/// Ограничение авторизации на основе операций с данными
/// </summary>
public class OperationRequirement : IAuthorizationRequirement
{
    public string Operation { get; set; }
}