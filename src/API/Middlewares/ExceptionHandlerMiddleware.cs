using Microsoft.AspNetCore.Diagnostics;

namespace ChatApp.API.Middlewares
{
    public class ExceptionHandlerMiddleware //: IExceptionHandler
    {
        /* public async ValueTask<bool> TryHandleAsync( */
        /*     HttpContext httpContext, */
        /*     Exception exception, */
        /*     CancellationToken cancellationToken */
        /* ) */
        /* { */
        /*     var (status, title, detail) = exception switch */
        /*     { */
        /*         UnauthorizedAccessException => ( */
        /*             StatusCodes.Status401Unauthorized, */
        /*             "Unauthorized", */
        /*             "Login required to access" */
        /*         ), */
        /*         ValidatorException => ( */
        /*             StatusCodes.Status400BadRequest, */
        /*             "One or more validation errors occurred.", */
        /*             "Payload does not algin requirements" */
        /*         ), */
        /*         _ => ( */
        /*             StatusCodes.Status500InternalServerError, */
        /*             "Internal Server Error", */
        /*             "Something went wrong. Please try again" */
        /*         ), */
        /*     }; */

        /*     ApiErrorResponse response = new(httpContext) */
        /*     { */
        /*         Title = title, */
        /*         Status = status, */
        /*         Type = "about:blank", */
        /*         Detail = detail, */
        /*     }; */

        /*     if (exception is ValidatorException ex) */
        /*     { */
        /*         response.Errors = ex.ToDictionary(); */
        /*     } */
        /*     else if (status == StatusCodes.Status500InternalServerError) */
        /*     { */
        /*         Console.WriteLine(exception.Message); */
        /*     } */

        /*     httpContext.Response.StatusCode = status; */
        /*     httpContext.Response.ContentType = "application/problem+json"; */

        /*     await httpContext.Response.WriteAsJsonAsync(response); */

        /*     return true; */
        /* } */
    }
}
