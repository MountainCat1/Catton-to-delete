﻿using ConventionDomain.Application.Authorization;
using ConventionDomain.Application.Dtos.Convention;
using ConventionDomain.Application.Extensions;
using ConventionDomain.Application.Services;
using Conventions.Domain.Entities;
using Conventions.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace ConventionDomain.Application.Features.ConventionFeature;

public class UpdateConventionRequest : IRequest
{
    public required Guid Id { get; init; }
    public required ConventionUpdateDto UpdateDto { get; init; }
}

public class UpdateConventionRequestHandler : IRequestHandler<UpdateConventionRequest>
{
    private readonly IConventionRepository _conventionRepository;
    private readonly IAuthorizationService _authorizationService;
    private readonly IUserAccessor _userAccessor;

    public UpdateConventionRequestHandler(
        IConventionRepository conventionRepository,
        IAuthorizationService authorizationService,
        IUserAccessor userAccessor)
    {
        _conventionRepository = conventionRepository;
        _authorizationService = authorizationService;
        _userAccessor = userAccessor;
    }

    public async Task Handle(UpdateConventionRequest request, CancellationToken cancellationToken)
    {
        var convention = await _conventionRepository.GetOneRequiredAsync(request.Id);

        await _authorizationService.AuthorizeAndThrowAsync(_userAccessor.User, convention, Policies.ReadConvention);

        var updateDto = request.UpdateDto;

        var update = new ConventionUpdate()
        {
            Active = updateDto.Active,
            Description = updateDto.Description,
            Name = updateDto.Name
        };

        convention.Update(update);

        await _conventionRepository.SaveChangesAsync();
    }
}