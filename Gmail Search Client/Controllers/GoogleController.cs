using Gmail_Search_Client.Controllers.GoogleUseCase;
using Gmail_Search_Client.Models;
using Google.Apis.Auth.AspNetCore3;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gmail_Search_Client.Controllers
{
    [Authorize]
    [GoogleScopedAuthorize(GmailService.ScopeConstants.GmailReadonly)]
    public class GoogleController : Controller
    {
        private GmailService _service;
        private AbstractMessagePage _messagePage;

        public GoogleController([FromServices] IGoogleAuthProvider auth)
        {
            GoogleCredential cred = auth.GetCredentialAsync().Result;
            this._service = new GmailService(initializer: new BaseClientService.Initializer
            {
                HttpClientInitializer = cred
            });
        }

        [Authorize]
        public IActionResult LoginGoogle()
        {
            return RedirectToAction(actionName: "Index", controllerName: "Home", routeValues: new { area = "Home" });
        }

        /// <summary>
        /// After authorization, creates a page for all messages from Gmail
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public IActionResult All()
        {
            var gmailServiceUseCase = new GmailServiceUseCase(gmailService: this._service);
            _messagePage = gmailServiceUseCase.GetAllMessages();

            return View(viewName: "Mail", model: _messagePage);
        }

        /// <summary>
        /// After authorization, creates a page for unread messages from Gmail
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public IActionResult NotRead()
        {
            var gmailServiceUseCase = new GmailServiceUseCase(gmailService: this._service);
            _messagePage = gmailServiceUseCase.GetNotReadMessages();

            return View(viewName: "Mail", model: _messagePage);
        }

        /// <summary>
        /// After authorization, creates a page for starred messages from Gmail
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Starred()
        {
            var gmailServiceUseCase = new GmailServiceUseCase(gmailService: this._service);
            _messagePage = gmailServiceUseCase.GetStarredMessages();

            return View(viewName: "Mail", model: _messagePage);
        }

        /// <summary>
        /// After authorization, creates a page for archived messages from Gmail
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public IActionResult Archived()
        {
            var gmailServiceUseCase = new GmailServiceUseCase(gmailService: this._service);
            _messagePage = gmailServiceUseCase.GetArchivedMessages();

            return View(viewName: "Mail", model: _messagePage);
        }

        /// <summary>
        /// Go to the next page
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public IActionResult Next(int pageNumber)
        {
            pageNumber++;
            var gmailServiceUseCase = new GmailServiceUseCase(gmailService: this._service);

            _messagePage = gmailServiceUseCase.GetPageByNumber(pageNumber: pageNumber);
            return View(viewName: "Mail", model: _messagePage);
        }

        /// <summary>
        /// Go to the previous page
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public IActionResult Previous(int pageNumber)
        {
            pageNumber--;
            if (pageNumber == 0)
            {
                pageNumber = 1;
            }
            var gmailServiceUseCase = new GmailServiceUseCase(gmailService: this._service);

            _messagePage = gmailServiceUseCase.GetPageByNumber(pageNumber: pageNumber);
            return View(viewName: "Mail", model: _messagePage);
        }

        /// <summary>
        /// Go to the selected page
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public IActionResult GetPageByNumber(int pageNumber)
        {
            var gmailServiceUseCase = new GmailServiceUseCase(gmailService: this._service);
            _messagePage = gmailServiceUseCase.GetPageByNumber(pageNumber: pageNumber);

            return View(viewName: "Mail", model: _messagePage);
        }

        /// <summary>
        /// Receives the full text of the selected message
        /// </summary>
        /// <param name="messageId"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public IActionResult FullMessage(string messageId)
        {
            var gmailServiceUseCase = new GmailServiceUseCase(gmailService: this._service);
            var toReturn = gmailServiceUseCase.GetMessageById(id: messageId).GetLabelsWithMessageBody();

            return PartialView(viewName: "FullMessage", model: toReturn);
        }
    }
}
