﻿using FluentResults;
using Microsoft.AspNetCore.Mvc;
using TodoWolverine.Api.Models;

namespace TodoWolverine.Api.Extensions;

public static class HttpResponseExtensions
{
    public static IActionResult ToHttpResponse<T>(this Result<T> result)
    {
        if (result.IsSuccess)
        {
            if (typeof(T) == typeof(Success)) return new OkResult();
            return new OkObjectResult(result.Value);
        }

        return new BadRequestObjectResult(
            new ResponseValidationError(result.Errors.Select(x => x.Message).ToList()));
    }
}