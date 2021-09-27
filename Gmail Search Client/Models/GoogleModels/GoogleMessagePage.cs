using Gmail_Search_Client.Models.GoogleModels;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using static Google.Apis.Gmail.v1.UsersResource;

namespace Gmail_Search_Client.Models
{
    public class GoogleMessagePage : AbstractMessagePage
    {
        private const string userId = "me";
        private const string pageQIs = "is: ";
        private GmailService _gmailService { get; set; }

        public GoogleMessagePage(GmailService gmailService) : base()
        {
            _gmailService = gmailService;
            this.ViewPages = new Dictionary<int, List<string>>();
            this.Messages = new List<AbstractMessage>();
            this.MessageIds = new List<string>();
        }

        #region Public methods
        /// <summary>
        /// Returns the abstract page Gmail messages
        /// </summary>
        /// <returns></returns>
        public override AbstractMessagePage GetMessagePage()
        {
            return CreateMessagePage(gmailService: _gmailService, pageNumber: this.PageNumber);
        }

        /// <summary>
        /// Returns the selected abstract page
        /// </summary>
        /// <returns></returns>
        public override AbstractMessagePage GetPageByNumber(int pageNumber)
        {
            this.PageNumber = pageNumber;
            return CreateMessagePage(gmailService: _gmailService, pageNumber: pageNumber);
        }

        private GoogleMessagePage CreateMessagePage(GmailService gmailService, int pageNumber)
        {
            _gmailService = gmailService;
            var listRequest = ListRequest(googleQ: this._pageType);
            var list = listRequest.ExecuteAsync();
            foreach (var message in list.Result.Messages)
            {
                this.MessageIds.Add(item: message.Id);
            }
            this.PageNumber = pageNumber;
            this.Messages = CreateListMessages(massegeIds: this.GetPage(pageNumber: this._pageNumber, 
                                               numberMessagesInPage: this._messagesInPage));
            return this;
        }

        /// <summary>
        /// Returns a message found on a unique id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override AbstractMessage GetMessageById(string id)
        {
            return GetMessageDetails(id: id);
        }
        #endregion

        #region Private methods
        private MessagesResource.ListRequest ListRequest(string googleQ = "")
        {
            var listRequest = _gmailService.
                     Users.
                     Messages.
                     List(userId: userId);
            listRequest.MaxResults = this._messagesCount;
            listRequest.Q = pageQIs + googleQ;
            return listRequest;
        }

        private List<AbstractMessage> CreateListMessages(List<string> massegeIds)
        {
            var toReturn = new List<AbstractMessage>();

            foreach (var id in massegeIds)
            {
                toReturn.Add(item: GetMessageDetails(id));
            }

            return toReturn;
        }

        private GoogleMessage GetMessageDetails(string id)
        {
            var toCreate = _gmailService.
                           Users.
                           Messages.
                           Get(userId: userId, id: id).
                           Execute();

            return CreateGoogleMessage(message: toCreate);
        }

        private GoogleMessage CreateGoogleMessage(Message message)
        {
            GoogleMessage toReturn = new GoogleMessage();
            toReturn.MessageId = message.Id;
            toReturn.EmailTitles = message.Snippet;
            toReturn.From = message.Payload.Headers.First(x => x.Name == "From").Value;
            toReturn.SetDate(DataTimeHelper.FromUnixTimeMilliseconds(inputValue: message.InternalDate.Value));
            toReturn.SetLabels(message.LabelIds.ToList());

            if (message.Payload.Parts != null)
            {
                foreach (MessagePart part in message.Payload.Parts)
                {
                    if (part.MimeType == "text/html")
                    {
                        toReturn.SetMessageBody(massege: part.Body.Data);
                    }
                }
            }
            return toReturn;
        }

        private Dictionary<int, List<string>> CreateViewPages(int numberMessagesInPage)
        {
            var key = 1;
            var list = new List<string>();
            if (MessageIds != null)
            {
                if (MessageIds.Count < numberMessagesInPage)
                {
                    numberMessagesInPage = MessageIds.Count;
                }
                foreach (var id in MessageIds)
                {
                    list.Add(item: id);
                    if (list.Count == numberMessagesInPage)
                    {
                        var listToDictionary = new List<string>();
                        listToDictionary = list;
                        ViewPages.Add(key: key, value: listToDictionary);
                        key++;
                        list = new List<string>();
                    }
                }
            }
            return ViewPages;
        }

        private List<string> GetPage(int pageNumber, int numberMessagesInPage)
        {
            var dictionaryToSearch = CreateViewPages(numberMessagesInPage: numberMessagesInPage);
            var listToReturn = dictionaryToSearch.GetValueOrDefault(key: pageNumber);
            return listToReturn;
        }

        #endregion
    }
}
