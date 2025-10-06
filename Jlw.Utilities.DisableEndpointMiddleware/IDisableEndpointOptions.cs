using System.Collections.Generic;

namespace Jlw.Utilities.DisableEndpointMiddleware
{
    public interface IDisableEndpointOptions
    {
        IEnumerable<string>? AllowedPaths { get; set; }
        string? BasePath { get; set; }
        string? RedirectUrl { get; set; }
        int DefaultStatusCode { get; set; }
        string? DefaultStatusMessage { get; set; }
    }
}