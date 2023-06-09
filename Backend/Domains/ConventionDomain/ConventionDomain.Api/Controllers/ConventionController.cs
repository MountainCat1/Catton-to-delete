﻿using ConventionDomain.Application.Dtos.Convention;
using ConventionDomain.Application.Extensions;
using ConventionDomain.Application.Features.ConventionFeature;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ConventionDomain.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/convention")]
public class ConventionController : Controller
{
    private readonly IMediator _mediator;

    public ConventionController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create(ConventionCreateDto conventionCreateDto)
    {
        var request = new CreateConventionRequest()
        {
            ConventionCreateDto = conventionCreateDto
        };

        await _mediator.Send(request);

        return Ok();
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var request = new GetConventionRequest()
        {
            Id = id
        };
        
        var result = await _mediator.Send(request);

        return Ok(result);
    }
    
    [HttpGet("")]
    public async Task<IActionResult> GetAll()
    {
        var request = new GetAllConventionsRequest()
        {
            AccountId = User.GetUserId()
        };
        
        var result = await _mediator.Send(request);

        return Ok(result);
    }
    
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] ConventionUpdateDto updateDto)
    {
        var request = new UpdateConventionRequest()
        {
            Id = id,
            UpdateDto = updateDto
        };

        await _mediator.Send(request);

        return Ok();
    }
}