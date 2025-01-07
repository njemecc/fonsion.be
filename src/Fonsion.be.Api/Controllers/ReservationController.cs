using Fonsion.be.Application.Common.Dtos.Reservations;
using Fonsion.be.Application.Reservations.Commands.CreateReservation;
using Fonsion.be.Application.Reservations.Queries;
using Fonsion.be.Application.Reservations.Queries.GetReservationsByUserId;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Fonsion.be.Api.Controllers;



[ApiController]
[Route("/api/[controller]")]
public class ReservationController : ControllerBase
{
    private readonly IMediator _mediator;

    public ReservationController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    
    [HttpPost]

    public async Task<IActionResult> CreateReservation([FromBody] CreateReservationRequest request)
    {
        var command = new CreateReservationCommand(request);

        var response = await _mediator.Send(command);

        return Ok(response);


    }
    
    [HttpGet("occupied-dates/{roomId}")]

    public async Task<IActionResult> GetReservedDaysByRoomId(Guid roomId)
    {
        var query = new GetReservedDatesByRoomIdQuery(roomId);
        
        var response = await _mediator.Send(query);
        
        return Ok(response);
    }

    [HttpGet("getByUser/{userId}")]
    public async Task<IActionResult> GetReservationsByUserId(Guid userId)
    {
        var query = new GetReservationsByUserIdQuery(userId);
        
        var response =  await _mediator.Send(query);
        
        return Ok(response);
    }
    
}