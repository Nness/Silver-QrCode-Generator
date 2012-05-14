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
    internal sealed class iCalContent : QrContent, IDataErrorInfo
    {
        #region Creation

        internal static iCalContent CreateNewContent()
        {
            return new iCalContent();
        }

        internal static iCalContent CreateContent(
            string summary,
            DateTime? starttime,
            DateTime? endtime
        )
        {
            return new iCalContent
            {
                Summary = summary,
                StartTime = starttime,
                EndTime = endtime
            };
        }

        internal iCalContent()
        { }

        #endregion

        #region Properties

        /// <summary>
        /// Get/Set Calender event summary
        /// </summary>
        internal string Summary
        { get; set; }

        /// <summary>
        /// Get/Set Calender event start time
        /// </summary>
        internal DateTime? StartTime
        { get; set; }

        /// <summary>
        /// Get/Set Calender event end time
        /// </summary>
        internal DateTime? EndTime
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
                builder.Append("BEGIN:VEVENT\n");
                StringAppend.BuilderAppendNewLine(ref builder, Summary, "SUMMARY:");
                StringAppend.BuilderAppendNewLine(ref builder, StringMethod.iCalDateTimeRevamp(StartTime), "DTSTART:");
                StringAppend.BuilderAppendNewLine(ref builder, StringMethod.iCalDateTimeRevamp(EndTime), "DTEND:");
                builder.Append("END:VEVENT");
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
            "Summary",
            "StartTime",
            "EndTime"
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
                case "Summary":
                    error = StringValidation.ValidateRequired(propertyName, Summary);
                    break;
                case "StartTime":
                    error = this.ValidateStartTime();
                    break;
                case "EndTime":
                    error = this.ValidateEndTime();
                    break;
                default:
                    Debug.Fail("Unexpected property being validated on iCal: " + propertyName);
                    break;
            }

            return error;
        }

        private string ValidateStartTime()
        {
            if (StringValidation.IsDateTimeMissing(StartTime))
                return "Start Time" + Resources.Error_Missing_Required_Field;
            else if (StringValidation.IsATimeBiggerThanBTime(StartTime, EndTime))
                return Resources.Error_iCal_StartTime_Bigger;
            return null;
        }

        private string ValidateEndTime()
        {
            if (StringValidation.IsDateTimeMissing(EndTime))
                return "End Time" + Resources.Error_Missing_Required_Field;
            else if (StringValidation.IsATimeBiggerThanBTime(StartTime, EndTime))
                return Resources.Error_iCal_StartTime_Bigger;
            return null;
        }

        #endregion

    }
}
