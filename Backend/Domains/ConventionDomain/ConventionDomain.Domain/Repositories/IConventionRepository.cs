﻿using ConventionDomain.Domain.Abstractions;
using ConventionDomain.Domain.Entities;

namespace ConventionDomain.Domain.Repositories;

public interface IConventionRepository : IRepository<Convention>
{
    Task<ICollection<Convention>> GetByOrganizatorId(Guid accountId);
}