﻿using Conventions.Domain.Entities;

namespace ConventionDomain.Application.Dtos.Organizer;

public class OrganizerCreateDto
{
    public Guid AccountId { get; init; }
    public OrganizerRole? Role { get; init; }
}