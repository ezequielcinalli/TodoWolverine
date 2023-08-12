namespace TodoWolverine.Api.Models;

public class DomainException : Exception
{
    public DomainException(string message) : base(message)
    {
    }
}