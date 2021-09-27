using Gmail_Search_Client.Models;

namespace Gmail_Search_Client.Controllers
{
    public interface IUseCaseAdapter
    {
        /// <summary>
        /// Returns the abstract page of all email messages
        /// </summary>
        /// <returns></returns>
        public AbstractMessagePage GetAllMessages();
        /// <summary>
        /// Returns the abstract page of all unread email messages
        /// </summary>
        /// <returns></returns>
        public AbstractMessagePage GetNotReadMessages();
        /// <summary>
        /// Returns the abstract page of all starred email messages
        /// </summary>
        /// <returns></returns>
        public AbstractMessagePage GetStarredMessages();
        /// <summary>
        /// Returns the abstract page of all archived email messages
        /// </summary>
        /// <returns></returns>
        public AbstractMessagePage GetArchivedMessages();
        /// <summary>
        /// Returns the selected abstract page
        /// </summary>
        /// <returns></returns>
        public AbstractMessagePage GetPageByNumber(int pageNumber);
        /// <summary>
        /// Returns a message found on a unique id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public AbstractMessage GetMessageById(string id);

    }
}
