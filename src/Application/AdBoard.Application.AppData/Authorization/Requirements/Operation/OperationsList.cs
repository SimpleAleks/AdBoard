namespace AdBoard.Application.AppData.Authorization.Requirements.Operation;

/// <summary>
/// Лист возможных операций, которые пользователь может производить в системе
/// </summary>
public static class OperationsList
{
    public static OperationRequirement Create = new() { Operation = Constants.CreateOperationName };
    public static OperationRequirement Read = new() { Operation = Constants.ReadOperationName };
    public static OperationRequirement Update = new() { Operation = Constants.UpdateOperationName };
    public static OperationRequirement Delete = new() { Operation = Constants.DeleteOperationName };
}