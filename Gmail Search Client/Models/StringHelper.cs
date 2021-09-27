using System;
using System.Text;

namespace Gmail_Search_Client.Models
{
    static public class StringHelper
    {
        /// <summary>
        /// Converts the specified string, which encodes binary data as base-64 digits, to an equivalent 8-bit unsigned integer array.
        /// </summary>
        /// <param name="base64ForUrlInput"></param>
        /// <returns></returns>
        public static byte[] FromBase64ForUrlString(string base64ForUrlInput)
        {
            int padChars = (base64ForUrlInput.Length % 4) == 0 ? 0 : (4 - (base64ForUrlInput.Length % 4));
            StringBuilder result = new StringBuilder(value: base64ForUrlInput, capacity: base64ForUrlInput.Length + padChars);
            result.Append(value: String.Empty.PadRight(totalWidth: padChars, paddingChar: '='));
            result.Replace(oldChar: '-', newChar: '+');
            result.Replace(oldChar: '_', newChar: '/');
            return Convert.FromBase64String(s: result.ToString());
        }

        /// <summary>
        /// Gets an encoding for the UTF-8 format. Returns: An encoding for the UTF-8 format.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string EncodingUtf8(byte[] data)
        {
            return Encoding.UTF8.GetString(bytes: data);
        }
    }
}
