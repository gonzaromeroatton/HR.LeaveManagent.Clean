using HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.DeleteLeaveAllocation;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocationDetails;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocations;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HR.LeaveManagement.Api.Controllers;

public class LeaveAllocationsController : ControllerBase
{
    private readonly IMediator _mediator;

    public LeaveAllocationsController(IMediator mediator)
    {
        this._mediator = mediator;
    }

    // GET: LeaveAllocationsController
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<LeaveAllocationDto>>> Get(bool isLoggedInUser = false)
    {
        var leaveAllocations = await this._mediator.Send(new GetLeaveAllocationsQuery());
        
        return Ok(leaveAllocations);
    }

    // GET: LeaveAllocationsController/Details/5
    [HttpGet("{id}")]
    public async Task<ActionResult<LeaveAllocationDto>> Get(int id)
    {
        var leaveAllocation = this._mediator.Send(new GetLeaveAllocationDetailsQuery { Id = id });

        return Ok(leaveAllocation);
    }

    // GET: LeaveAllocationsController/Create
    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Post(CreateLeaveAllocationCommand leaveAllocation)
    {
        var response = await this._mediator.Send(leaveAllocation);

        return CreatedAtAction(nameof(Get), new { id = response });
    }

    // POST: LeaveAllocationsController/Create
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(400)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Put(UpdateLeaveAllocationCommand leaveAllocation)
    {
        await this._mediator.Send(leaveAllocation);

        return NoContent();
    }

    // GET: LeaveAllocationsController/Edit/5
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteLeaveAllocationCommand { Id = id };

        await this._mediator.Send(command);

        return NoContent();
    }
}
