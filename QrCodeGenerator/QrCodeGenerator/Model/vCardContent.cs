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
    internal sealed class vCardContent : QrContent, IDataErrorInfo
    {

        #region Creation

        internal static vCardContent CreateNewContent()
        {
            return new vCardContent();
        }

        internal vCardContent()
        { }

        #endregion

        #region Properties

        internal string FirstName
        { get; set; }

        internal string LastName
        { get; set; }

        internal string Mobile
        { get; set; }

        internal string Title
        { get; set; }

        internal string Organization
        { get; set; }

        internal string PhoneHome
        { get; set; }

        internal string PhoneWork
        { get; set; }

        internal string FaxHome
        { get; set; }

        internal string FaxWork
        { get; set; }

        internal string Email
        { get; set; }

        internal string URL
        { get; set; }

        internal string Note
        { get; set; }

        internal DateTime? BirthDay
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
                builder.Append("BEGIN:VCARD\n");
                //Name
                StringAppend.BuilderAppendvCardName(ref builder, LastName, FirstName);
                //Mobile
                StringAppend.BuilderAppendNewLine(ref builder, StringMethod.MeCardPhoneRevamp(Mobile), "TEL;CELL:");
                //Title
                StringAppend.BuilderAppendNewLine(ref builder, Title, "TITLE:");
                //Organisation
                StringAppend.BuilderAppendNewLine(ref builder, Organization, "ORG:");
                //Phone (Home)
                StringAppend.BuilderAppendNewLine(ref builder, StringMethod.MeCardPhoneRevamp(PhoneHome), "TEL;HOME:");
                //Phone (Work)
                StringAppend.BuilderAppendNewLine(ref builder, StringMethod.MeCardPhoneRevamp(PhoneWork), "TEL;WORK");
                //Fax
                StringAppend.BuilderAppendNewLine(ref builder, StringMethod.MeCardPhoneRevamp(FaxHome), "TEL;HOME;FAX:");
                //Fax (Work)
                StringAppend.BuilderAppendNewLine(ref builder, StringMethod.MeCardPhoneRevamp(FaxWork), "TEL;WORK;FAX:");
                //Email
                StringAppend.BuilderAppendNewLine(ref builder, Email, "EMAIL:");
                //URL
                StringAppend.BuilderAppendNewLine(ref builder, URL, "URL:");
                //Note
                StringAppend.BuilderAppendNewLine(ref builder, Note, "NOTE:");
                //BirthDay
                StringAppend.BuilderAppendNewLine(ref builder, StringMethod.MeCardBirthRevamp(BirthDay), "BDAY:");
                //Address
                StringAppend.BuilderAppendNewLine(ref builder, Address, "ADR;WORK:");
                builder.Append("END:VCARD");
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
            "Mobile",
            "PhoneHome",
            "PhoneWork",
            "FaxHome",
            "FaxWork",
            "Email",
            "URL",
            "BirthDay"
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
                case "Mobile":
                    error = StringValidation.ValidatePhone(Mobile, false);
                    break;
                case "PhoneHome":
                    error = StringValidation.ValidatePhone(PhoneHome, false);
                    break;
                case "PhoneWork":
                    error = StringValidation.ValidatePhone(PhoneWork, false);
                    break;
                case "FaxHome":
                    error = StringValidation.ValidatePhone(FaxHome, false);
                    break;
                case "FaxWork":
                    error = StringValidation.ValidatePhone(FaxWork, false);
                    break;
                case "Email":
                    error = StringValidation.ValidateEmail(Email, false);
                    break;
                case "URL":
                    error = StringValidation.ValidateUrl(URL, false);
                    break;
                case "BirthDay":
                    error = StringValidation.ValidateBirthday(BirthDay);
                    break;
                default:
                    Debug.Fail("Unexpected property being validated on vCardContent: " + propertyName);
                    break;
            }

            return error;
        }

        #endregion

    }
}
