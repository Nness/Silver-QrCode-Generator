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
    internal sealed class EmailViewModel : QrViewModelBase, INotifyPropertyChanged, IDataErrorInfo
    {
        private readonly EmailContent m_EmailContent;

        public event PropertyChangedEventHandler PropertyChanged = null;

        internal override string GetContent()
        {
            return m_EmailContent.GetContent;
        }

        #region Construction

        internal EmailViewModel()
        {
            m_EmailContent = new EmailContent();
        }

        internal EmailViewModel(EmailContent emailContent)
        {
            if (emailContent == null)
                throw new ArgumentNullException("emailContent");

            m_EmailContent = emailContent;
        }

        #endregion

        #region Properties

        public string Email
        {
            get
            {
                return m_EmailContent.Email;
            }
            set
            {
                if (m_EmailContent.Email == value)
                    return;

                m_EmailContent.Email = value;
                PropertyChanged.Raise(() => Email);
            }
        }

        public string Subject
        {
            get
            {
                return m_EmailContent.Subject;
            }
            set
            {
                if (m_EmailContent.Subject == value)
                    return;
                m_EmailContent.Subject = value;
                PropertyChanged.Raise(() => Subject);
            }
        }

        public string Content
        {
            get
            {
                return m_EmailContent.Content;
            }
            set
            {
                if (m_EmailContent.Content == value)
                    return;
                m_EmailContent.Content = value;
                PropertyChanged.Raise(() => Content);
            }
        }

        #endregion

        #region IDataErrorInfo

        string IDataErrorInfo.Error
        {
            get { return (m_EmailContent as IDataErrorInfo).Error; }
        }

        string IDataErrorInfo.this[string propertyName]
        {
            get
            {
                string error = null;

                error = (m_EmailContent as IDataErrorInfo)[propertyName];

                return error;
            }
        }

        #endregion

        internal override void Clear()
        {
            this.Email = string.Empty;
            this.Subject = string.Empty;
            this.Content = string.Empty;
        }

    }
}
