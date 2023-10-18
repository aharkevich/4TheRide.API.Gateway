using System.Net;
using Microsoft.AspNetCore.Mvc;
using TheRide.API.Models;

namespace TheRide.API.Helpers;

/// <summary>
/// Status converters.
/// </summary>
public static class StatusConverters
{
    /// <summary>
    /// Converts status to error model.
    /// </summary>
    /// <param name="status">The response status.</param>
    /// <param name="message">The message to insert into an error model.</param>
    /// <exception cref="InvalidOperationException">Success status must not be converted to the error result.</exception>
    /// <returns><see cref="ObjectResult"/> with error model <seealso cref="ErrorModel"/></returns>
    public static ObjectResult ConvertToErrorResult(HttpStatusCode status, string message)
    {
        if (status != HttpStatusCode.OK)
        {
            var errorCode = (int)status;

            return new ObjectResult(new ErrorModel
            {
                Message = message,
                ErrorCode = errorCode.ToString(),
                ErrorDetails = string.Empty
            })
            {
                StatusCode = errorCode
            };
        }

        throw new InvalidOperationException("Success status can not be converted to the error result.");
    }
}
