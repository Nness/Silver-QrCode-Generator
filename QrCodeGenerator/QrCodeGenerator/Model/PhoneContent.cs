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
    internal sealed class PhoneContent : QrContent, IDataErrorInfo
    {
        #region Creation

        internal static PhoneContent CreateNewContent()
        {
            return new PhoneContent();
        }

        internal static PhoneContent CreateContent(
            string phone
        )
        {
            return new PhoneContent
            {
                Phone = phone
            };
        }

        internal PhoneContent()
        { }

        #endregion

        #region Properties

        /// <summary>
        /// Get/Set Phone Num
        /// </summary>
        internal string Phone
        { get; set; }

        #endregion

        #region IDataErrorInfo Members

        string IDataErrorInfo.Error { get { return null; } }

        string IDataErrorInfo.this[string PropertyName]
        {
            get
            {
                if (PropertyName == "Phone")
                    return GetValidationError();
                else
                {
                    Debug.Fail("Unexpected property being validated on PhoneContent: " + PropertyName);
                    return null;
                }
            }
        }

        #endregion

        #region Inheritance QrContent

        internal override string GetContent
        {
            get { return "TEL:" + StringMethod.MeCardPhoneRevamp(Phone); }
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
            return StringValidation.ValidatePhone(Phone, true);
        }

        #endregion

    }
}
