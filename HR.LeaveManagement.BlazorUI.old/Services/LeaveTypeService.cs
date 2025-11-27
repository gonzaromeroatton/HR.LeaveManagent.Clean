using AutoMapper;
using HR.LeaveManagement.BlazorUI.Contracts;
using HR.LeaveManagement.BlazorUI.Modules;
using HR.LeaveManagement.BlazorUI.Services.Base;

namespace HR.LeaveManagement.BlazorUI.Services;

/// <summary>
/// 
/// </summary>
public class LeaveTypeService : BaseHttpService, ILeaveTypeService
{
    /// <summary>
    /// 
    /// </summary>
    private readonly IMapper _mapper;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="client"></param>
    public LeaveTypeService(IClient client, IMapper mapper) : base(client)
    {
        this._mapper = mapper;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="leaveType"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<Response<Guid>> CreateLeaveType(LeaveTypeViewModel leaveType)
    {
        try
        {
            var createLeaveTypeCommand = this._mapper.Map<CreateLeaveTypeCommand>(leaveType);
            await this._client.LeaveTypesPOSTAsync(createLeaveTypeCommand);

            return new Response<Guid>() { Success = true, Message = "Leave Type created successfully." };
        }
        catch(ApiException ex)
        {
            return ConvertApiExceptions<Guid>(ex);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<Response<Guid>> DeleteLeaveType(int id)
    {
        try
        {
            await this._client.LeaveTypesDELETEAsync(id);

            return new Response<Guid>() { Success = true, Message = "Leave Type deleted successfully." };
        }
        catch (ApiException ex)
        {
            return ConvertApiExceptions<Guid>(ex);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<LeaveTypeViewModel> GetLeaveTypeDetails(int id)
    {
        var leaveType = await this._client.LeaveTypesGETAsync(id);

        return this._mapper.Map<LeaveTypeViewModel>(leaveType);
    }

    /// <summary>
    /// Retrieves a read-only list of leave types.
    /// </summary>
    /// <remarks>This method asynchronously fetches all leave types from the underlying data source and maps
    /// them  to a collection of <see cref="LeaveTypeViewModel"/> objects. The returned list is read-only to  ensure
    /// immutability.</remarks>
    /// <returns>A task that represents the asynchronous operation. The task result contains a read-only list of  <see
    /// cref="LeaveTypeViewModel"/> objects, where each object represents a leave type.</returns>
    public async Task<IReadOnlyList<LeaveTypeViewModel>> GetLeaveTypes()
    {
        var leaveTypes = await this._client.LeaveTypesAllAsync();

        // Map LeaveTypeDto to LeaveTypeViewModel
        return this._mapper.Map<IReadOnlyList<LeaveTypeViewModel>>(leaveTypes);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="leaveType"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<Response<Guid>> UpdateLeaveType(int id, LeaveTypeViewModel leaveType)
    {
        try
        {
            var updateLeaveTypeCommand = this._mapper.Map<UpdateLeaveTypeCommand>(leaveType);

            await this._client.LeaveTypesPUTAsync(id.ToString(), updateLeaveTypeCommand);

            return new Response<Guid>() { Success = true, Message = "Leave Type updated successfully." };
        }
        catch(ApiException ex)
        {
            return ConvertApiExceptions<Guid>(ex);
        }
    }
}
