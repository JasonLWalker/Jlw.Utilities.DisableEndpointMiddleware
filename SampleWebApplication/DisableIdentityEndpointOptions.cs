using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace Jlw.Utilities.DisableEndpointMiddleware
{

    /// <summary>
    /// Example utility class to disable all default Microsoft Identity UI endpoints except for Login and Logout
    /// </summary>
    public class DisableIdentityEndpointOptions : IDisableEndpointOptions
    {
        public IEnumerable<string>? AllowedPaths { get; set; } = new string[] { "/Identity/Account/Login", "/Identity/Account/Logout" };

        public string BasePath { get; set; } = "/Identity";
        public int DefaultStatusCode { get; set; } = StatusCodes.Status404NotFound;
        public string DefaultStatusMessage { get; set; } = "";
    }
}