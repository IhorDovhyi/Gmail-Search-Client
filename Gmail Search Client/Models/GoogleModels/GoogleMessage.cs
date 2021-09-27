using System;
using System.Collections.Generic;

namespace Gmail_Search_Client.Models.GoogleModels
{
    public class GoogleMessage : AbstractMessage
    {
        /// <summary>
        /// Sets the date the message was received
        /// </summary>
        /// <param name="dateTimeToSet"></param>
        public override void SetDate(DateTime dateTimeToSet)
        {
            this.DateReceived = DataTimeHelper.ToCurrentTimeZone(dateTimeToSet);
        }

        /// <summary>
        /// Sets the full text of the message
        /// </summary>
        /// <param name="massege"></param>
        public override void SetMessageBody(string massege)
        {
            byte[] data = StringHelper.FromBase64ForUrlString(base64ForUrlInput: massege);
            this.MessageBody = StringHelper.EncodingUtf8(data);
        }

        /// <summary>
        /// Sets message labels
        /// </summary>
        /// <param name="labelsToSet"></param>
        public override void SetLabels(List<string> labelsToSet)
        {
            foreach (var label in labelsToSet)
            {
                this.Labels.Add(label.Replace('_', ' '));
            }
        }

        /// <summary>
        /// Returns the full text of the message with labels
        /// </summary>
        /// <returns></returns>
        public override string GetLabelsWithMessageBody()
        {
            string labelsString = string.Empty;

            foreach (var label in this.Labels)
            {
                labelsString += label + " ";
            }

            return labelsString + this.MessageBody;
        }
    }
}
