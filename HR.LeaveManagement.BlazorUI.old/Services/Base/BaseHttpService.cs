namespace HR.LeaveManagement.BlazorUI.Services.Base;

/// <summary>
/// 
/// </summary>
public class BaseHttpService
{
    /// <summary>
    /// 
    /// </summary>
    protected readonly IClient _client;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="client"></param>
    public BaseHttpService(IClient client)
    {
        this._client = client;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="Guid"></typeparam>
    /// <param name="apiException"></param>
    /// <returns></returns>
    protected Response<Guid> ConvertApiExceptions<Guid>(ApiException apiException)
    {
        if (apiException.StatusCode == 400)
        {
            return new Response<Guid>()
            {
                Message = "Invalid data was submitted.",
                ValidationErrors = apiException.Response,
                Success = false
            };
        }
        else if (apiException.StatusCode == 404)
        {
            return new Response<Guid>()
            {
                Message = "The requested item was not found.",
                Success = false
            };
        }
        else
        {
            return new Response<Guid>()
            {
                Message = "Something went wrong, please try again.",
                Success = false
            };
        }
    }
}
