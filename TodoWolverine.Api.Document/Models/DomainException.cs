﻿namespace TodoWolverine.Api.Document.Models;

public class DomainException : Exception
{
    public DomainException(string message) : base(message)
    {
    }
}