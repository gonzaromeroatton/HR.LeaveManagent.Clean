using HR.LeaveManagement.BlazorUI.Contracts;
using HR.LeaveManagement.BlazorUI.Modules;
using Microsoft.AspNetCore.Components;

namespace HR.LeaveManagement.BlazorUI.Pages.LeaveTypes;

/// <summary>
/// 
/// </summary>
public partial class Index
{
    /// <summary>
    /// 
    /// </summary>
    [Inject]
    public NavigationManager NavigationManager { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [Inject]
    public ILeaveTypeService LeaveTypeService { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public IReadOnlyList<LeaveTypeViewModel> LeaveTypes { get; private set; }

    /// <summary>
    /// 
    /// </summary>
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// 
    /// </summary>
    protected void CreateLeaveType()
    {
        this.NavigationManager.NavigateTo("/leavetypes/create");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    protected void AllocateLeaveType(int id)
    {

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    protected void EditLeaveType(int id)
    {
        this.NavigationManager.NavigateTo($"/leavetypes/edit/{id}");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    protected void DetailsLeaveType(int id)
    {
        this.NavigationManager.NavigateTo($"/leavetypes/details/{id}");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    protected async Task DeleteLeaveType(int id)
    {
        var response = await this.LeaveTypeService.DeleteLeaveType(id);

        if (response.Success)
            StateHasChanged();
        else
            this.Message = response.Message;
    }

    /// <summary>
    /// Asynchronously initializes the component and retrieves the list of leave types.
    /// </summary>
    /// <remarks>This method is called by the Blazor framework during the component's initialization
    /// phase. It retrieves leave type data from the associated service and populates the <see cref="LeaveTypes"/>
    /// property.</remarks>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    protected override async Task OnInitializedAsync()
    {
        this.LeaveTypes = await this.LeaveTypeService.GetLeaveTypes();
    }
}