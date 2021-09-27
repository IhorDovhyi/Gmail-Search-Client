using System;
using System.Collections.Generic;

namespace Gmail_Search_Client.Models
{
    public abstract class AbstractMessage
    {
        public string MessageId { get; set; }
        public string EmailTitles { get; set; }
        public string From { get; set; }
        public DateTime DateReceived { get; set; }
        public string MessageBody { get; set; }
        public List<string> Labels { get; set; }

        public AbstractMessage()
        {
            Labels = new List<string>();
        }

        /// <summary>
        /// Sets the full text of the message
        /// </summary>
        /// <param name="massege"></param>
        public virtual void SetMessageBody(string massege)
        {
            MessageBody = massege;
        }

        /// <summary>
        /// Sets the date the message was received
        /// </summary>
        /// <param name="dateTimeToSet"></param>
        public virtual void SetDate(DateTime dateTimeToSet)
        {
            DateReceived = dateTimeToSet;
        }

        /// <summary>
        /// Sets message labels
        /// </summary>
        /// <param name="labelsToSet"></param>
        public virtual void SetLabels(List<string> labelsToSet)
        {
            Labels = labelsToSet;
        }

        /// <summary>
        /// Returns the full text of the message with labels
        /// </summary>
        /// <returns></returns>
        public virtual string GetLabelsWithMessageBody()
        {
            return Labels.ToString() + MessageBody;
        }
    }
}
