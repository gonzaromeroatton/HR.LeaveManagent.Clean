using HR.LeaveManagement.Application.Features.LeaveRequest.Commands.CancelLeaveRequest;
using HR.LeaveManagement.Application.Features.LeaveRequest.Commands.ChangeLeaveRequestApproval;
using HR.LeaveManagement.Application.Features.LeaveRequest.Commands.DeleteLeaveRequest;
using HR.LeaveManagement.Application.Features.LeaveRequest.Commands.UpdateLeaveRequest;
using HR.LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestDetail;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HR.LeaveManagement.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class LeaveRequestController : ControllerBase
{
    private readonly IMediator _mediator;

    public LeaveRequestController(IMediator mediator)
    {
        this._mediator = mediator;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<LeaveRequestDetailsDto>> Get(int id)
    {
        var leaveRequest = await this._mediator.Send(new GetLeaveRequestDetailQuery { Id = id });
        return Ok(leaveRequest);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(400)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Put(UpdateLeaveRequestCommand leaveRequest)
    {
        await this._mediator.Send(leaveRequest);
        return NoContent();
    }

    [HttpPut]
    [Route("CancelRequest")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(400)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> CancelRequest(CancelLeaveRequestCommand cancelLeaveRequest)
    {
        await this._mediator.Send(cancelLeaveRequest);
        return NoContent();
    }

    [HttpPut]
    [Route("ChangeLeaveRequest")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(400)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> UpdateApproval(ChangeLeaveRequestApprovalCommand updateApprovalRequest)
    {
        await this._mediator.Send(updateApprovalRequest);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteLeaveRequestCommand { Id = id };
        await this._mediator.Send(command);
        return NoContent();
    }
}
