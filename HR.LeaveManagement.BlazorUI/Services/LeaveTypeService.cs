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
    /// <param name="client"></param>
    public LeaveTypeService(IClient client) : base(client)
    {
        
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="leaveType"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task<Response<Guid>> CreateLeaveType(LeaveTypeViewModel leaveType)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task<Response<Guid>> DeleteLeaveType(int id)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task<LeaveTypeViewModel> GetLeaveTypeDetails(int id)
    {
        throw new NotImplementedException();
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
        var leaveTypeDtos = await this._client.LeaveTypesAllAsync();
        // Map LeaveTypeDto to LeaveTypeViewModel
        var leaveTypeViewModels = leaveTypeDtos
            .Select(dto => new LeaveTypeViewModel
            {
                Id = dto.Id,
                Name = dto.Name,
                DefaultDays = dto.DefaultDays
            })
            .ToList()
            .AsReadOnly();

        return leaveTypeViewModels;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="leaveType"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task<Response<Guid>> UpdateLeaveType(int id, LeaveTypeViewModel leaveType)
    {
        throw new NotImplementedException();
    }
}
