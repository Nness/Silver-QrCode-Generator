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
    internal sealed class vCardViewModel : QrViewModelBase, INotifyPropertyChanged, IDataErrorInfo
    {
        private readonly vCardContent m_vCardContent;

        public event PropertyChangedEventHandler PropertyChanged = null;

        internal override string GetContent()
        {
            return m_vCardContent.GetContent;
        }

        #region Construction

        internal vCardViewModel(vCardContent vcardContent)
        {
            if (vcardContent == null)
                throw new ArgumentNullException("vcardContent");

            m_vCardContent = vcardContent;
        }

        #endregion

        #region Properties

        internal string FirstName
        {
            get
            {
                return m_vCardContent.FirstName;
            }
            set
            {
                if (m_vCardContent.FirstName == value)
                    return;
                m_vCardContent.FirstName = value;
                PropertyChanged.Raise(() => FirstName);
            }
        }

        internal string LastName
        {
            get
            {
                return m_vCardContent.LastName;
            }
            set
            {
                if (m_vCardContent.LastName == value)
                    return;
                m_vCardContent.LastName = value;
                PropertyChanged.Raise(() => LastName);
            }
        }

        internal string Mobile
        {
            get
            {
                return m_vCardContent.Mobile;
            }
            set
            {
                if (m_vCardContent.Mobile == value)
                    return;
                m_vCardContent.Mobile = value;
                PropertyChanged.Raise(() => Mobile);
            }
        }

        internal string Title
        {
            get
            {
                return m_vCardContent.Title;
            }
            set
            {
                if (m_vCardContent.Title == value)
                    return;
                m_vCardContent.Title = value;
                PropertyChanged.Raise(() => Title);
            }
        }

        internal string Organization
        {
            get
            {
                return m_vCardContent.Organization;
            }
            set
            {
                if (m_vCardContent.Organization == value)
                    return;
                m_vCardContent.Organization = value;
                PropertyChanged.Raise(() => Title);
            }
        }

        internal string PhoneHome
        {
            get
            {
                return m_vCardContent.PhoneHome;
            }
            set
            {
                if (m_vCardContent.PhoneHome == value)
                    return;
                m_vCardContent.PhoneHome = value;
                PropertyChanged.Raise(() => PhoneHome);
            }
        }

        internal string PhoneWork
        {
            get
            {
                return m_vCardContent.PhoneWork;
            }
            set
            {
                if (m_vCardContent.PhoneWork == value)
                    return;
                m_vCardContent.PhoneWork = value;
                PropertyChanged.Raise(() => PhoneWork);
            }
        }

        internal string FaxHome
        {
            get
            {
                return m_vCardContent.FaxHome;
            }
            set
            {
                if (m_vCardContent.FaxHome == value)
                    return;
                m_vCardContent.FaxHome = value;
                PropertyChanged.Raise(() => FaxHome);
            }
        }

        internal string FaxWork
        {
            get
            {
                return m_vCardContent.FaxWork;
            }
            set
            {
                if (m_vCardContent.FaxWork == value)
                    return;
                m_vCardContent.FaxWork = value;
                PropertyChanged.Raise(() => FaxWork);
            }
        }

        internal string Email
        {
            get
            {
                return m_vCardContent.Email;
            }
            set
            {
                if (m_vCardContent.Email == value)
                    return;
                m_vCardContent.Email = value;
                PropertyChanged.Raise(() => Email);
            }
        }

        internal string URL
        {
            get
            {
                return m_vCardContent.URL;
            }
            set
            {
                if (m_vCardContent.URL == value)
                    return;
                m_vCardContent.URL = value;
                PropertyChanged.Raise(() => URL);
            }
        }

        internal string Note
        {
            get
            {
                return m_vCardContent.Note;
            }
            set
            {
                if (m_vCardContent.Note == value)
                    return;
                m_vCardContent.Note = value;
                PropertyChanged.Raise(() => Note);
            }
        }

        internal DateTime? BirthDay
        {
            get
            {
                return m_vCardContent.BirthDay;
            }
            set
            {
                if (m_vCardContent.BirthDay == value)
                    return;
                m_vCardContent.BirthDay = value;
                PropertyChanged.Raise(() => BirthDay);
            }
        }

        internal string Address
        {
            get
            {
                return m_vCardContent.Address;
            }
            set
            {
                if (m_vCardContent.Address == value)
                    return;
                m_vCardContent.Address = value;
                PropertyChanged.Raise(() => Address);
            }
        }

        #endregion

        #region IDataErrorInfo

        string IDataErrorInfo.Error
        {
            get { return (m_vCardContent as IDataErrorInfo).Error; }
        }

        string IDataErrorInfo.this[string propertyName]
        {
            get
            {
                string error = null;

                error = (m_vCardContent as IDataErrorInfo)[propertyName];

                return error;
            }
        }

        #endregion
    }
}
