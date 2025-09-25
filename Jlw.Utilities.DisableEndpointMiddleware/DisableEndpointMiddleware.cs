using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace Jlw.Utilities.DisableEndpointMiddleware
{
    /// <summary>
    /// Middleware to block designated endpoints from being reached by responding with the
    /// status code and message specified by the TOptions class.
    /// </summary>
    /// <typeparam name="TOptions"></typeparam>
    public class DisableEndpointMiddleware<TOptions> where TOptions : class, IDisableEndpointOptions
    {
        private readonly RequestDelegate _next;
        private readonly TOptions _options;

        /// <summary>
        /// Constructor to initialize the middleware
        /// </summary>
        /// <param name="next">The next middleware delegate to pass the context along to.</param>
        /// <param name="options">The configuration options for this middleware</param>
        public DisableEndpointMiddleware(RequestDelegate next, IOptions<TOptions> options)
        {
            _next = next;
            _options = options.Value;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Does the requested path start with the Base path that we are blocking?
            if (context.Request.Path.StartsWithSegments(_options.BasePath))
            {
                // Are there any paths that should be allowed within the base path?
                if (_options.AllowedPaths?.Count() > 0)
                {
                    // does the requested path start with any of the allowed paths?
                    if (_options.AllowedPaths.Any(path => context.Request.Path.StartsWithSegments(path)))
                    {
                        // pass request along to the next handler for processing
                        await _next(context);
                        return;
                    }
                }

                // Path does not match, and should be blocked.

                // Set response status code to the configured default code.
                context.Response.StatusCode = _options.DefaultStatusCode;

                // If there is a configured default status message, then output the text
                if (!string.IsNullOrWhiteSpace(_options.DefaultStatusMessage))
                    await context.Response.WriteAsync(_options.DefaultStatusMessage);

                // return immediately to halt further page processing.
                return;
            }

            // Path not matched, pass request along to next handler.
            await _next(context);
        }
    }
}
