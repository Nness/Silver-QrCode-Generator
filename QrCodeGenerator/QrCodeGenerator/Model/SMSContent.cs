/*
 *  Silver QrCode Generator - Windows WPF application to generate QrCode.
 *  Copyright (c) 2012 Canxing(Jason) He
 *
 *  This program is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation, either version 3 of the License, or
 *  (at your option) any later version.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with this program.  If not, see <http://www.gnu.org/licenses/>. 
*/
using System;
using System.ComponentModel;
using QrCodeGenerator.Properties;
using System.Diagnostics;
using System.Text;

namespace QrCodeGenerator.Model
{
    internal sealed class SMSContent : QrContent, IDataErrorInfo
    {
        #region Creation

        internal static SMSContent CreateNewContent()
        {
            return new SMSContent();
        }

        internal static SMSContent CreateContent(
            string phone,
            string message
        )
        {
            return new SMSContent
            {
                Phone = phone,
                Message = message,
            };
        }

        internal SMSContent()
        { }

        #endregion

        #region Properties

        /// <summary>
        /// Get/Set Email Address
        /// </summary>
        internal string Phone
        { get; set; }

        /// <summary>
        /// Get/Set Email's Subject
        /// </summary>
        internal string Message
        { get; set; }

        #endregion

        #region IDataErrorInfo Members

        string IDataErrorInfo.Error
        { get { return null; } }

        string IDataErrorInfo.this[string PropertyName]
        {
            get
            {
                return this.GetValidationError(PropertyName);
            }
        }

        #endregion

        #region Inheritance QrContent

        internal override string GetContent
        {
            get
            {
                if (!this.IsValid)
                    return string.Empty;

                StringBuilder builder = new StringBuilder();
                builder.Append("SMSTO:");
                builder.Append(StringMethod.MeCardPhoneRevamp(Phone));
                builder.Append(":");
                builder.Append(Message.TrimStart(' ').TrimEnd(' '));
                return builder.ToString();
            }
        }

        #endregion

        #region Validation

        internal override bool IsValid
        {
            get
            {
                foreach (string property in ValidatedProperties)
                    if (GetValidationError(property) != null)
                        return false;
                return true;
            }
        }

        private static readonly string[] ValidatedProperties =
        {
            "Phone",
            "Message"
        };

        private string GetValidationError(string propertyName)
        {
            if (Array.IndexOf(ValidatedProperties, propertyName) < 0)
            {
                return null;
            }

            string error = null;

            switch (propertyName)
            {
                case "Phone":
                    error = StringValidation.ValidatePhone(Phone, true);
                    break;
                case "Message":
                    error = StringValidation.ValidateRequired(propertyName, this.Message);
                    break;
                default:
                    Debug.Fail("Unexpected property being validated on SMSContent: " + propertyName);
                    break;
            }

            return error;
        }

        #endregion
    }
}
