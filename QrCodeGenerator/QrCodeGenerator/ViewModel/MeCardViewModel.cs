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
    internal sealed class MeCardViewModel : QrViewModelBase, INotifyPropertyChanged, IDataErrorInfo
    {
        private readonly MeCardContent m_MeCardContent;

        public event PropertyChangedEventHandler PropertyChanged = null;

        internal override string GetContent()
        {
            return m_MeCardContent.GetContent;
        }

        #region Construction

        internal MeCardViewModel()
        {
            m_MeCardContent = new MeCardContent();
        }

        internal MeCardViewModel(MeCardContent mecardContent)
        {
            if (mecardContent == null)
                throw new ArgumentNullException("mecardContent");

            m_MeCardContent = mecardContent;
        }

        #endregion

        #region Properties

        public string FirstName
        {
            get
            {
                return m_MeCardContent.FirstName;
            }
            set
            {
                if (m_MeCardContent.FirstName == value)
                    return;
                m_MeCardContent.FirstName = value;
                PropertyChanged.Raise(() => FirstName);
            }
        }

        public string LastName
        {
            get
            {
                return m_MeCardContent.LastName;
            }
            set
            {
                if (m_MeCardContent.LastName == value)
                    return;
                m_MeCardContent.LastName = value;
                PropertyChanged.Raise(() => LastName);
            }
        }

        public string Phone
        {
            get
            {
                return m_MeCardContent.Phone;
            }
            set
            {
                if (m_MeCardContent.Phone == value)
                    return;
                m_MeCardContent.Phone = value;
                PropertyChanged.Raise(() => Phone);
            }
        }

        public string Email
        {
            get
            {
                return m_MeCardContent.Email;
            }
            set
            {
                if (m_MeCardContent.Email == value)
                    return;
                m_MeCardContent.Email = value;
                PropertyChanged.Raise(() => Email);
            }
        }

        public string Url
        {
            get
            {
                return m_MeCardContent.Url;
            }
            set
            {
                if (m_MeCardContent.Url == value)
                    return;
                m_MeCardContent.Url = value;
                PropertyChanged.Raise(() => Url);
            }
        }

        public DateTime? Birthday
        {
            get
            {
                return m_MeCardContent.Birthday;
            }
            set
            {
                if (m_MeCardContent.Birthday == value)
                    return;
                m_MeCardContent.Birthday = value;
                PropertyChanged.Raise(() => Birthday);
            }
        }

        public string Memo
        {
            get
            {
                return m_MeCardContent.Memo;
            }
            set
            {
                if (m_MeCardContent.Memo == value)
                    return;
                m_MeCardContent.Memo = value;
                PropertyChanged.Raise(() => Memo);
            }
        }

        public string Address
        {
            get
            {
                return m_MeCardContent.Address;
            }
            set
            {
                if (m_MeCardContent.Address == value)
                    return;
                m_MeCardContent.Address = value;
                PropertyChanged.Raise(() => Address);
            }
        }

        #endregion

        #region IDataErrorInfo

        string IDataErrorInfo.Error
        {
            get { return (m_MeCardContent as IDataErrorInfo).Error; }
        }

        string IDataErrorInfo.this[string propertyName]
        {
            get
            {
                string error = null;

                error = (m_MeCardContent as IDataErrorInfo)[propertyName];

                return error;
            }
        }

        #endregion

        internal override void Clear()
        {
            this.FirstName = string.Empty;
            this.LastName = string.Empty;
            this.Phone = string.Empty;
            this.Email = string.Empty;
            this.Url = string.Empty;
            this.Birthday = null;
            this.Memo = string.Empty;
            this.Address = string.Empty;
        }
    }
}
