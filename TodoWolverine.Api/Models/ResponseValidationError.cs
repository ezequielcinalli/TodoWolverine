namespace TodoWolverine.Api.Models;

public record ResponseValidationError(List<string> Errors)
{
    public ResponseValidationError(string error) : this(new List<string> { error })
    {
    }
}