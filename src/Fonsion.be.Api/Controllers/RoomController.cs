﻿using Fonsion.be.Application.Rooms.Commands;
using Fonsion.be.Contracts.Rooms;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Fonsion.be.Api.Controllers;


[ApiController]
[Route("[controller]")]
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
}