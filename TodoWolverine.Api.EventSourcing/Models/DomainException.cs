namespace TodoWolverine.Api.EventSourcing.Models;

public class DomainException : Exception
{
    public DomainException(string message) : base(message)
    {
    }
}