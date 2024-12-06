using Fonsion.be.Application.Rooms.Commands;
using Fonsion.be.Application.Rooms.Queries.GetAllRooms;
using Fonsion.be.Contracts.Rooms;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fonsion.be.Api.Controllers;


[ApiController]
[Route("/api/[controller]")]
public class RoomController: ControllerBase
{


    private readonly IMediator _mediator;

    public RoomController(IMediator mediator)
    {
        _mediator = mediator;
    }


   
    [HttpPost]

    public async Task<IActionResult> CreateRoom(CreateRoomRequest request)
    {
        var command = new CreateRoomCommand(request);

        var response = await _mediator.Send(command);

        return Ok(response);


    }

    
    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetAllRooms()
    {

        var query = new GetAllRoomsQuery();

        var response = await _mediator.Send(query);

        return Ok(response);
    }
}