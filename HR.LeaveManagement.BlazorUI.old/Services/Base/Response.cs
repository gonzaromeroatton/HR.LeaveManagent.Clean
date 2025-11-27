namespace HR.LeaveManagement.BlazorUI.Services.Base;

/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
public sealed class Response<T>
{
    /// <summary>
    /// 
    /// </summary>
    public string Message { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string ValidationErrors { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public T Data { get; set; }
}
