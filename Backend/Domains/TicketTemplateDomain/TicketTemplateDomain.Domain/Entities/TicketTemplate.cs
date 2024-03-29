﻿using Catut.Domain.Abstractions;

namespace TicketTemplateDomain.Domain.Entities;

public class TicketTemplate : Entity
{
    private TicketTemplate(string description, decimal price, Guid conventionId)
    {
        Description = description;
        Price = price;
        ConventionId = conventionId;
    }

    public static TicketTemplate Create(string description, decimal price, Guid conventionId)
    {
        var newEntity = new TicketTemplate(description, price, conventionId);

        return newEntity;
    }

    public Guid Id { get; set; }

    public string Description { get; set; } = null!;
    public decimal Price { get; set; }

    public Guid ConventionId { get; set; }

    public void Update(TicketTemplateUpdate update)
    {
        Description = update.Description;
        Price = update.Price;
    }
}

public record TicketTemplateUpdate(string Description, decimal Price) : IEntityUpdate<TicketTemplate>
{
}