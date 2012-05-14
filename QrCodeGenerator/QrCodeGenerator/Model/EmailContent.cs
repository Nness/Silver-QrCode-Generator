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
    internal sealed class EmailContent : QrContent, IDataErrorInfo
    {
        #region Creation

        internal static EmailContent CreateNewContent()
        {
            return new EmailContent();
        }

        internal static EmailContent CreateContent(
            string email,
            string subject,
            string content
        )
        {
            return new EmailContent
            {
                Email = email,
                Subject = subject,
                Content = content
            };
        }

        internal EmailContent()
        { }

        #endregion

        #region Properties

        /// <summary>
        /// Get/Set Email Address
        /// </summary>
        internal string Email
        { get; set; }

        /// <summary>
        /// Get/Set Email's Subject
        /// </summary>
        internal string Subject
        { get; set; }

        /// <summary>
        /// Get/Set Email's Content
        /// </summary>
        internal string Content
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
                builder.Append("SMTP:");
                builder.Append(Email.TrimStart(' ').TrimEnd(' '));
                builder.Append(":");
                builder.Append(Subject.TrimStart(' ').TrimEnd(' '));
                builder.Append(":");
                builder.Append(Content.TrimStart(' ').TrimEnd(' '));
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
            "Email",
            "Subject",
            "Content"
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
                case "Email":
                    error = StringValidation.ValidateEmail(Email, true);
                    break;
                case "Subject":
                    error = StringValidation.ValidateRequired(propertyName, this.Subject);
                    break;
                case "Content":
                    error = StringValidation.ValidateRequired(propertyName, this.Content);
                    break;
                default:
                    Debug.Fail("Unexpected property being validated on EmailContent: " + propertyName);
                    break;
            }

            return error;
        }

        #endregion
    }
}
