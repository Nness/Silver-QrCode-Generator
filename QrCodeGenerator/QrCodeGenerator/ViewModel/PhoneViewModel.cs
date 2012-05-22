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
    internal sealed class PhoneViewModel : QrViewModelBase, INotifyPropertyChanged, IDataErrorInfo
    {
        private readonly PhoneContent m_PhoneContent;

        public event PropertyChangedEventHandler PropertyChanged = null;

        internal override string GetContent()
        {
            return m_PhoneContent.GetContent;
        }

        #region Construction

        internal PhoneViewModel()
        {
            m_PhoneContent = new PhoneContent();
        }

        internal PhoneViewModel(PhoneContent phoneContent)
        {
            if (phoneContent == null)
                throw new ArgumentNullException("phoneContent");

            m_PhoneContent = phoneContent;
        }

        #endregion

        #region Properties

        public string Phone
        {
            get
            {
                return m_PhoneContent.Phone;
            }
            set
            {
                if (m_PhoneContent.Phone == value)
                    return;
                m_PhoneContent.Phone = value;
                PropertyChanged.Raise(() => Phone);
            }
        }

        #endregion

        #region IDataErrorInfo

        string IDataErrorInfo.Error
        {
            get { return (m_PhoneContent as IDataErrorInfo).Error; }
        }

        string IDataErrorInfo.this[string propertyName]
        {
            get
            {
                string error = null;

                error = (m_PhoneContent as IDataErrorInfo)[propertyName];

                return error;
            }
        }

        #endregion

        internal override void Clear()
        {
            this.Phone = string.Empty;
        }
    }
}
