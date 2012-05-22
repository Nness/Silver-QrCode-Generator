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

namespace QrCodeGenerator.Model
{
    internal sealed class UrlContent : QrContent, IDataErrorInfo
    {

        #region Creation

        internal static UrlContent CreateNewContent()
        {
            return new UrlContent();
        }

        internal static UrlContent CreateContent(
            string url
        )
        {
            return new UrlContent
            {
                Url = url
            };
        }

        internal UrlContent()
        { }

        #endregion

        #region Properties

        /// <summary>
        /// Get/sets the Url address
        /// </summary>
        internal string Url { get; set; }
        #endregion

        #region IDataErrorInfo Members

        string IDataErrorInfo.Error { get { return null; } }

        string IDataErrorInfo.this[string PropertyName]
        {
            get 
            {
                if (PropertyName == "Url")
                    return GetValidationError();
                else
                {
                    Debug.Fail("Unexpected property being validated on UrlContent: " + PropertyName);
                    return null;
                }
            }
        }

        #endregion

        #region Inheritance QrContent

        internal override string GetContent
        {
            get 
            {
                if (string.IsNullOrWhiteSpace(Url))
                    return string.Empty;
                return Url.TrimStart(' ').TrimEnd(' '); 
            }
        } 

        #endregion

        #region Validation

        internal override bool IsValid
        {
            get 
            {
                if (GetValidationError() != null)
                    return false;
                return true;
            }
        }

        private string GetValidationError()
        {
            return StringValidation.ValidateUrl(this.Url, true);
        }

        #endregion

    }
}
