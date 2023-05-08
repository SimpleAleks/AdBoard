using Microsoft.AspNetCore.Http;

namespace AdBoard.Application.AppData;

/// <summary>
/// Статический класс содержащий набор констант использующихся в приложении
/// </summary>
public static class Constants
{
    // Константы операций, которые может производить пользователи
    public static readonly string CreateOperationName = "Create";
    public static readonly string ReadOperationName = "Read";
    public static readonly string UpdateOperationName = "Update";
    public static readonly string DeleteOperationName = "Delete";

    // Константы ролей, которые могут быть у пользователей
    public static readonly string UserAuthorizationRole = "User";
    public static readonly string AdminAuthorizationRole = "Admin";
    public static readonly string DefaultAuthorizationRole = "User";
}