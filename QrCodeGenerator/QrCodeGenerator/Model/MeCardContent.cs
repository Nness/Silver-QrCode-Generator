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
    internal sealed class MeCardContent : QrContent, IDataErrorInfo
    {
        #region Creation

        internal static MeCardContent CreateNewContent()
        {
            return new MeCardContent();
        }

        internal MeCardContent()
        { }

        #endregion

        #region Properties

        internal string FirstName
        { get; set; }

        internal string LastName
        { get; set; }

        internal string Phone
        { get; set; }

        internal string Email
        { get; set; }

        internal string Url
        { get; set; }

        internal DateTime? Birthday
        { get; set; }

        internal string Memo
        { get; set; }

        internal string Address
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
                builder.Append("MECARD:");
                //Name
                StringAppend.BuilderAppendMeCardName(ref builder, LastName, FirstName);
                //Phone
                StringAppend.BuilderAppendSemicolon(ref builder, StringMethod.MeCardPhoneRevamp(Phone), "TEL:");
                //Email
                StringAppend.BuilderAppendSemicolon(ref builder, StringMethod.MeCardStringRevamp(Email), "EMAIL:");
                //URL
                StringAppend.BuilderAppendSemicolon(ref builder, StringMethod.MeCardStringRevamp(Url), "URL:");
                //Birthday
                StringAppend.BuilderAppendSemicolon(ref builder, StringMethod.MeCardBirthRevamp(Birthday), "BDAY:");
                //Memo
                StringAppend.BuilderAppendSemicolon(ref builder, StringMethod.MeCardStringRevamp(Memo), "NOTE:");
                //Address
                StringAppend.BuilderAppendSemicolon(ref builder, StringMethod.MeCardStringRevamp(Address), "ADR:");
                builder.Append(';');
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
            "FirstName",
            "Phone",
            "Email",
            "Url",
            "Birthday"
        };

        private string GetValidationError(string propertyName)
        {
            //Check if property is in validation list.
            //If not, then it's not require to be validated.
            if (Array.IndexOf(ValidatedProperties, propertyName) < 0)
            {
                return null;
            }

            string error = null;

            switch (propertyName)
            {
                case "FirstName":
                    error = StringValidation.ValidateRequired(propertyName, FirstName);
                    break;
                case "Phone":
                    error = StringValidation.ValidatePhone(Phone, false);
                    break;
                case "Email":
                    error = StringValidation.ValidateEmail(Email, false);
                    break;
                case "Url":
                    error = StringValidation.ValidateUrl(Url, false);
                    break;
                case "Birthday":
                    error = StringValidation.ValidateBirthday(Birthday);
                    break;
                default:
                    Debug.Fail("Unexpected property being validated on MeCardContent: " + propertyName);
                    break;
            }

            return error;
        }

        #endregion
    }
}
