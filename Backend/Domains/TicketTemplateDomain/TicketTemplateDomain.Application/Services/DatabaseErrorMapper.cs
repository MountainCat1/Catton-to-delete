﻿using Catut.Application.Errors;
using Catut.Infrastructure.Abstractions;
using Catut.Infrastructure.Errors.Database;

namespace TicketTemplateDomain.Application.Services;


public class DatabaseErrorMapper : IDatabaseErrorMapper
{
    public Exception Map(DatabaseException exception)
    {
        if (exception is ItemNotFoundException)
            return new NotFoundError(null, exception);

        return exception;
    }
}