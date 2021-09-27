using Gmail_Search_Client.Models.GoogleModels;
using System.Collections.Generic;

namespace Gmail_Search_Client.Models
{
    public class AbstractMessagePage
    {
        public List<AbstractMessage> Messages;
        public List<string> MessageIds;
        public Dictionary<int, List<string>> ViewPages;
        internal int _pageNumber = 1;
        internal int _messagesInPage = 10;
        internal int _messagesCount = 100;
        internal string _pageType = "";

        public AbstractMessagePage()
        {
            Messages = new List<AbstractMessage>();
            MessageIds = new List<string>();
            ViewPages = new Dictionary<int, List<string>>();
        }

        #region Variables
        /// <summary>
        /// You can use only allowed page types. 
        /// (archived, unread, starred). It is empty by default.
        /// </summary>
        public string PageType
        {
            get { return _pageType; }
            set
            {
                _pageType = !CheckPageType(value) ? _pageType = "" : _pageType = value;
            }
        }

        /// <summary>
        /// Cannot be less than 10
        /// </summary>
        public int MessagesInPage
        {
            get { return _messagesInPage; }
            set { _messagesInPage = value < 10 ? _messagesInPage = 10 : _messagesInPage = value; }
        }

        /// <summary>
        /// Cannot be less than 10 and more than 10
        /// </summary>
        public int PageNumber
        {
            get { return _pageNumber; }
            set { _pageNumber = value < 1 ? _pageNumber = 10 : _pageNumber = value; }
        }

        private bool CheckPageType(string pageType)
        {
            return pageType == PossiblePageTypes.None ||
                   pageType == PossiblePageTypes.Archived ||
                   pageType == PossiblePageTypes.NotRead ||
                   pageType == PossiblePageTypes.Starred;
        }
        #endregion

        #region Virtual methods
        /// <summary>
        /// Returns the abstract page email messages
        /// </summary>
        /// <returns></returns>
        public virtual AbstractMessagePage GetMessagePage()
        {
            return this;
        }

        /// <summary>
        /// Returns the selected abstract page
        /// </summary>
        /// <returns></returns>
        public virtual AbstractMessagePage GetPageByNumber(int pageNumber)
        {
            this.PageNumber = pageNumber;
            return this;
        }

        /// <summary>
        /// Returns a message found on a unique id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual AbstractMessage GetMessageById(string id)
        {
            return new GoogleMessage();
        }
        #endregion
    }
}
