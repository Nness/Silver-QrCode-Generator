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
using System.Text.RegularExpressions;
using QrCodeGenerator.Properties;

namespace QrCodeGenerator.Model
{
    internal static class StringValidation
    {
        /// <remarks>
        /// Check IsStringMissing before perform this validation. 
        /// </remarks>
        internal static bool IsValidUrl(string url)
        {
            string trimUrl = url.TrimStart(' ').TrimEnd(' ');
            try
            {
                Uri uri = new Uri(trimUrl);
                return true;
            }
            catch (UriFormatException)
            {
                return false;
            }
        }

        internal static bool IsStringMissing(string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        private const string EmailReg = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

        /// <remarks>
        /// Check IsStringMissing before perform this validation. 
        /// </remarks>
        internal static bool IsValidEmail(string email)
        {
            string trimEmail = email.TrimStart(' ').TrimEnd(' ');

            Regex re = new Regex(EmailReg, RegexOptions.IgnoreCase);

            return re.IsMatch(trimEmail);
        }

        internal const string PhoneReg = @"^[+]?[0-9\(\)\- ]{1,24}$";

        internal static bool IsValidPhone(string phone)
        {
            string trimPhone = phone.TrimStart(' ').TrimEnd(' ');

            Regex re = new Regex(PhoneReg, RegexOptions.IgnoreCase);

            return re.IsMatch(trimPhone);
        }

        internal static bool IsDateTimeMissing(DateTime? datetime)
        {
            if (!datetime.HasValue)
                return true;
            return false;
        }

        internal static bool IsBirthday(DateTime? birthday)
        {
            DateTime bday = birthday.GetValueOrDefault();
            if (bday > DateTime.Today)
                return false;
            return true;
        }

        internal static bool IsATimeBiggerThanBTime(DateTime? ATime, DateTime? BTime)
        {
            DateTime atime = ATime.GetValueOrDefault();
            DateTime btime = BTime.GetValueOrDefault();

            if (atime > btime)
                return true;
            else
                return false;
        }


        #region Validation

        internal static string ValidateRequired(string propertyName, string propertyValue)
        {
            if (StringValidation.IsStringMissing(propertyValue))
                return propertyName + Resources.Error_Missing_Required_Field;
            return null;
        }

        internal static string ValidatePhone(string phoneValue, bool isRequired)
        {
            if (StringValidation.IsStringMissing(phoneValue))
                return isRequired ? Resources.Error_Missing_Phone : null;
            else if (!StringValidation.IsValidPhone(phoneValue))
                return Resources.Error_Invalid_Phone;
            return null;
        }

        internal static string ValidateEmail(string emailValue, bool isRequired)
        {
            if (StringValidation.IsStringMissing(emailValue))
                return isRequired ? Resources.Error_Missing_Email : null;
            else if (!StringValidation.IsValidEmail(emailValue))
                return Resources.Error_Invalid_Email_Format;
            return null;
        }

        internal static string ValidateUrl(string urlValue, bool isRequired)
        {
            if (StringValidation.IsStringMissing(urlValue))
                return isRequired ? Resources.Error_MissingUrl : null;
            else if (!StringValidation.IsValidUrl(urlValue))
                return Resources.Error_InvalidUrl;
            return null;
        }

        internal static string ValidateBirthday(DateTime? bdayValue)
        {
            if (StringValidation.IsDateTimeMissing(bdayValue))
                return null;
            else if (!StringValidation.IsBirthday(bdayValue))
                return Resources.Error_Invalid_Birthday;
            return null;
        }

        #endregion

    }
}
