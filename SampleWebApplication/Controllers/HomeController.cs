using System.Net.Mime;
using Jlw.Extensions.Identity.Mock;
using Jlw.Utilities.Data;
using Jlw.Utilities.DisableEndpointMiddleware;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TUser = Jlw.Extensions.Identity.Stores.ModularBaseUser;

namespace SampleWebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<TUser> _userManager;
        private readonly SignInManager<TUser> _signInManager;
        private readonly DisableIdentityEndpointOptions _endpointOptions;
        private int userIterator = 1000;


        public HomeController(UserManager<TUser> userManager, SignInManager<TUser> signInManager, IOptions<DisableIdentityEndpointOptions> endpointOptions)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _endpointOptions = endpointOptions.Value;
        }

        // GET
        [HttpGet("/")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("/api/[action]")]
        [Produces(MediaTypeNames.Application.Json)]
        public object UpdateAllowedPath(PathModel o)
        {
            var list = (List<string>)_endpointOptions.AllowedPaths!;
            string path = o.Path ?? "";
            bool enabled = o.Enabled ?? false;
            if (string.IsNullOrWhiteSpace(path))
                return new { };

            if (enabled)
            {
                if (!list.Contains(path, StringComparer.InvariantCultureIgnoreCase))
                    list.Add(path);
            }
            else
            {
                foreach (var val in list.ToArray())
                {
                    if (val.Equals(path, StringComparison.InvariantCultureIgnoreCase))
                        list.Remove(val);
                }
            }

            return new PathModel{ Path = path, Enabled = list.Contains(path, StringComparer.InvariantCultureIgnoreCase)};
        }

        [HttpPost("/api/[action]")]
        [Produces(MediaTypeNames.Application.Json)]
        public object UpdateBasePath(ValueModel<string> o)
        {
            string path = o.Value ?? "";

            return new ValueModel<string> { Value = _endpointOptions.BasePath = path };
        }

        [HttpPost("/api/[action]")]
        [Produces(MediaTypeNames.Application.Json)]
        public object UpdateRedirectUrl(ValueModel<string> o)
        {
            string path = o.Value ?? "";

            return new ValueModel<string> { Value = _endpointOptions.RedirectUrl = path };
        }

        [HttpPost("/api/[action]")]
        [Produces(MediaTypeNames.Application.Json)]
        public object UpdateDefaultStatusCode(ValueModel<int> o)
        {
            var val = o.Value;
            if (val < 1)
                return new { };

            _endpointOptions.DefaultStatusCode = val;
            return new ValueModel<int> { Value = _endpointOptions.DefaultStatusCode };
        }

        [HttpPost("/api/[action]")]
        [Produces(MediaTypeNames.Application.Json)]
        public object UpdateDefaultStatusMessage(ValueModel<string> o)
        {
            var val = o.Value;
            _endpointOptions.DefaultStatusMessage = val;
            return new ValueModel<string> { Value = _endpointOptions.DefaultStatusMessage};
        }


        [Route("~/impersonate")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> Impersonate([FromForm] string username = null)
        {
            if (!string.IsNullOrWhiteSpace(username))
            {
                //var user = _userManager.FindByNameAsync(User.Identity.Name).Result;
                var impersonatedUser = await _userManager.FindByNameAsync(username);
                if (impersonatedUser == null)
                {
                    MockUserStore<TUser>.AddMockedUser(
                        MockedUserExtensions.GetNewUser<TUser>(new
                        {
                            Id = userIterator++,
                            UserName = username,
                            NormalizedUserName = username.ToUpper(),
                            Email = username,
                            NormalizedEmail = username.ToUpper(),
                            PasswordHash = "test",
                            EmailConfirmed = true
                        }),
                        new IdentityUserClaim<string>[] { }
                    );
                    impersonatedUser = await _userManager.FindByNameAsync(username);
                }

                if (impersonatedUser != null)
                {
                    //var impersonatedUser = await _userManager.FindByIdAsync(contact.ContactId.ToString());
                    var userPrincipal = await _signInManager.CreateUserPrincipalAsync(impersonatedUser);
                    // sign out the current user
                    await _signInManager.SignOutAsync();
                    await HttpContext.SignInAsync(IdentityConstants.ApplicationScheme, userPrincipal);
                    return RedirectToAction("Index", "Home");
                }
            }

            return View("Impersonate");
        }

        public class PathModel
        {
            public string? Path { get; set; }
            public bool? Enabled { get; set; }
        }

        public class ValueModel<T>
        {
            public T? Value { get; set; }
        }

    }
}