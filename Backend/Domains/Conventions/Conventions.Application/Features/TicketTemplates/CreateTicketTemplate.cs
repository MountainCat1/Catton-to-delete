﻿using Catut.Application.Errors;
using ConventionDomain.Application.Authorization;
using ConventionDomain.Application.Dtos.TicketTemplate;
using ConventionDomain.Application.Extensions;
using ConventionDomain.Application.Services;
using Conventions.Domain.Entities;
using Conventions.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace ConventionDomain.Application.Features.TicketTemplates;

public class CreateTicketTemplateRequest : IRequest<TicketTemplateDto>
{
    public required TicketTemplateCreateDto TicketCreateDto { get; init; }
    public required Guid ConventionId { get; init; }
}

public class CreateTicketTemplateRequestHandler : IRequestHandler<CreateTicketTemplateRequest, TicketTemplateDto>
{
    private readonly IConventionRepository _conventionRepository;
    private readonly IAuthorizationService _authorizationService;
    private readonly IUserAccessor _userAccessor;

    public CreateTicketTemplateRequestHandler(
        IConventionRepository conventionRepository,
        IAuthorizationService authorizationService,
        IUserAccessor userAccessor)
    {
        _conventionRepository = conventionRepository;
        _authorizationService = authorizationService;
        _userAccessor = userAccessor;
    }

    public async Task<TicketTemplateDto> Handle(CreateTicketTemplateRequest request, CancellationToken cancellationToken)
    {
        var convention = await _conventionRepository.GetOneWithAsync(request.ConventionId, 
            x => x.Organizers, 
            x => x.TicketTemplates);

        if (convention is null)
            throw new NotFoundError($"Convention with an id ({request.ConventionId}) was not found");

        await _authorizationService.AuthorizeAndThrowAsync(_userAccessor.User, convention, Policies.ManageTicketTemplates);

        var dto = request.TicketCreateDto;
        var authoriId = _userAccessor.User.GetUserId();
        
        var ticketTemplate = TicketTemplate.Create(
            name: dto.Name, 
            description: dto.Description, 
            price: dto.Price,
            conventionId: convention.Id, 
            authorId: authoriId);

        convention.AddTicketTemplate(ticketTemplate);

        await _conventionRepository.SaveChangesAsync();

        return ticketTemplate.ToDto();
    }
}