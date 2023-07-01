using FluentValidation.Results;

namespace Catut.Application.Errors;

public class BadRequestError : ApplicationError
{
    public override int StatusCode => 404;
    public override string ErrorName => "Bad Request";
    
    public IEnumerable<KeyValuePair<string, string>> Errors { get; }
    

    public BadRequestError() : base()
    {
        
    }
    public BadRequestError(string? message) : base(message)
    {
        
    }
    
    public BadRequestError(IEnumerable<ValidationFailure> validationFailures) : base("Validation failed")
    {
        Errors = validationFailures.Select(failure => new KeyValuePair<string, string>(failure.PropertyName, failure.ErrorMessage));
    }
}