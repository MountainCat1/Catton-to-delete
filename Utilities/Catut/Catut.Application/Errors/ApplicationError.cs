﻿using Catut.Application.Dtos;

namespace Catut.Application.Errors;

public abstract class ApplicationError : Exception
{

    public ApplicationError(string? message) : base(message)
    {
    }

    public ApplicationError(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    public abstract int StatusCode { get; }

    public virtual ErrorContent ToErrorContent()
    {
        return new ErrorContent()
        {
            Message = Message,
            StatusCode = StatusCode,
        };
    }

}
