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
using System.Linq;
using System.ComponentModel;
using QrCodeGenerator.Model;

namespace QrCodeGenerator.ViewModel
{
    internal sealed class SMSViewModel : QrViewModelBase, INotifyPropertyChanged, IDataErrorInfo
    {
        private readonly SMSContent m_SMSContent;

        public event PropertyChangedEventHandler PropertyChanged = null;

        internal override string GetContent()
        {
            return m_SMSContent.GetContent;
        }

        #region Construction

        internal SMSViewModel(SMSContent smsContent)
        {
            if (smsContent == null)
                throw new ArgumentNullException("smsContent");

            m_SMSContent = smsContent;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Get/Set Email Address
        /// </summary>
        internal string Phone
        {
            get
            {
                return m_SMSContent.Phone;
            }
            set
            {
                if (m_SMSContent.Phone == value)
                    return;
                m_SMSContent.Phone = value;
                PropertyChanged.Raise(() => Phone);
            }
        }

        /// <summary>
        /// Get/Set Email's Subject
        /// </summary>
        internal string Message
        {
            get
            {
                return m_SMSContent.Message;
            }
            set
            {
                if (m_SMSContent.Message == value)
                    return;
                m_SMSContent.Message = value;
                PropertyChanged.Raise(() => Message);
            }
        }

        #endregion

        #region IDataErrorInfo

        string IDataErrorInfo.Error
        {
            get { return (m_SMSContent as IDataErrorInfo).Error; }
        }

        string IDataErrorInfo.this[string propertyName]
        {
            get
            {
                string error = null;

                error = (m_SMSContent as IDataErrorInfo)[propertyName];

                return error;
            }
        }

        #endregion

    }
}
