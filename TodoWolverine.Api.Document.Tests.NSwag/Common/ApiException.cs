using System.Diagnostics.CodeAnalysis;

namespace TodoWolverine.Api.Document.Tests.NSwag.Common;

[ExcludeFromCodeCoverage]
public class ApiException : Exception
{
    public ApiException(string message, int statusCode, string? response,
        IReadOnlyDictionary<string, IEnumerable<string>> headers, Exception? innerException)
        : base(
            message + "\n\nStatus: " + statusCode + "\nResponse: \n" +
            response?.Substring(0, response.Length >= 512 ? 512 : response.Length), innerException)
    {
        StatusCode = statusCode;
        Response = response;
        Headers = headers;
    }

    public int StatusCode { get; private set; }

    public string? Response { get; }

    public IReadOnlyDictionary<string, IEnumerable<string>> Headers { get; private set; }

    public override string ToString()
    {
        return string.Format("HTTP Response: \n\n{0}\n\n{1}", Response, base.ToString());
    }
}

[ExcludeFromCodeCoverage]
public class ApiException<TResult> : ApiException
{
    public ApiException(string message, int statusCode, string response,
        IReadOnlyDictionary<string, IEnumerable<string>> headers, TResult result, Exception? innerException)
        : base(message, statusCode, response, headers, innerException)
    {
        Result = result;
    }

    public TResult Result { get; private set; }
}