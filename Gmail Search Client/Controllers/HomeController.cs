using Gmail_Search_Client.Controllers.GoogleUseCase;
using Gmail_Search_Client.Models;
using Google.Apis.Auth.AspNetCore3;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Gmail_Search_Client.Controllers
{
    public class HomeController : Controller
    {
        private GmailService _service;
        private AbstractMessagePage _messagePage;
        public HomeController()
        { }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Authorize]
        [GoogleScopedAuthorize(GmailService.ScopeConstants.GmailReadonly)]
        public IActionResult LoginGoogle()
        {
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            }
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        /// <summary>
        /// After authorization, creates a page for messages from Gmail
        /// </summary>
        /// <param name="auth"></param>
        /// <returns></returns>
        [Authorize]
        [GoogleScopedAuthorize(GmailService.ScopeConstants.GmailReadonly)]
        public async Task<IActionResult> Mail([FromServices] IGoogleAuthProvider auth)
        {
            GoogleCredential cred = await auth.GetCredentialAsync();
            this._service = new GmailService(new BaseClientService.Initializer
            {
                HttpClientInitializer = cred
            });
            _messagePage = new GoogleMessagePage(_service);
            var gmailServiceUseCase = new GmailServiceUseCase(this._service);
            _messagePage = gmailServiceUseCase.GetAllMessages();

            return View(_messagePage);
        }
    }
}