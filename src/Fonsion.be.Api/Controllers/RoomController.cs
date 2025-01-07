using Fonsion.be.Application.Common.Helpers;
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

    
    [HttpGet]
    public async Task<IActionResult> GetAllRooms([FromQuery] DateTime? fromDate, [FromQuery] DateTime? toDate, [FromQuery] int page = 0, [FromQuery] int pageSize = 5)
    {
        
        var queryObject = new QueryObject
        {
            FromDate = fromDate,
            ToDate = toDate,
            Page = page,
            PageSize = pageSize
        };

        var query = new GetAllRoomsQuery(queryObject);

        var response = await _mediator.Send(query);

        return Ok(response);
    }

 
}