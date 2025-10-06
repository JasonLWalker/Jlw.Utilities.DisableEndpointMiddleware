using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace Jlw.Utilities.DisableEndpointMiddleware
{
    /// <summary>
    /// Example utility class to disable all default Microsoft Identity UI endpoints
    /// except those specified in the AllowedPaths string array.
    /// </summary>
    public class DisableIdentityEndpointOptions : IDisableEndpointOptions
    {
        public IEnumerable<string>? AllowedPaths { get; set; } = new List<string>   // Set as List so that the values are mutable at runtime for the sample app.
        {
            "/Identity/Error", 
            "/Identity/Account/AccessDenied",
            "/Identity/Account/Lockout",
            "/Identity/Account/Login", 
            "/Identity/Account/Logout"
        };
        public string? BasePath { get; set; } = "/Identity";
        public string? RedirectUrl { get; set; }
        public int DefaultStatusCode { get; set; } = Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound;
        public string? DefaultStatusMessage { get; set; }
    }
}