using Gmail_Search_Client.Models;
using Google.Apis.Gmail.v1;

namespace Gmail_Search_Client.Controllers.GoogleUseCase
{
    public class GmailServiceUseCase : IUseCaseAdapter
    {
        private GoogleMessagePage _googleMessagePage;

        public GmailServiceUseCase(GmailService gmailService)
        {
            _googleMessagePage = new GoogleMessagePage(gmailService: gmailService);
        }

        /// <summary>
        /// Returns the abstract page of all Gmail messages
        /// </summary>
        /// <returns></returns>
        public AbstractMessagePage GetAllMessages()
        {
            _googleMessagePage.PageType = PossiblePageTypes.None;
            return _googleMessagePage.GetMessagePage();
        }

        /// <summary>
        /// Returns the abstract page of all unread Gmail messages
        /// </summary>
        /// <returns></returns>
        public AbstractMessagePage GetNotReadMessages()
        {
            _googleMessagePage.PageType = PossiblePageTypes.NotRead;
            return _googleMessagePage.GetMessagePage();
        }

        /// <summary>
        /// Returns the abstract page of all starred Gmail messages
        /// </summary>
        /// <returns></returns>
        public AbstractMessagePage GetStarredMessages()
        {
            _googleMessagePage.PageType = PossiblePageTypes.Starred;
            return _googleMessagePage.GetMessagePage();
        }

        /// <summary>
        /// Returns the abstract page of all archived Gmail messages
        /// </summary>
        /// <returns></returns>
        public AbstractMessagePage GetArchivedMessages()
        {
            _googleMessagePage.PageType = PossiblePageTypes.Archived;
            return _googleMessagePage.GetMessagePage();
        }

        /// <summary>
        /// Returns the selected abstract page
        /// </summary>
        /// <returns></returns>
        public AbstractMessagePage GetPageByNumber(int pageNumber)
        {
            return _googleMessagePage.GetPageByNumber(pageNumber: pageNumber);
        }

        /// <summary>
        /// Returns a message found on a unique id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public AbstractMessage GetMessageById(string id)
        {
            return _googleMessagePage.GetMessageById( id: id);
        }
    }
}
