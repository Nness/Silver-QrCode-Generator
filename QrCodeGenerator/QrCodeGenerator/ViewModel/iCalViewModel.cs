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
    internal sealed class iCalViewModel : QrViewModelBase, INotifyPropertyChanged, IDataErrorInfo
    {
        private readonly iCalContent m_iCalContent;

        public event PropertyChangedEventHandler PropertyChanged = null;

        internal override string GetContent()
        {
            return m_iCalContent.GetContent;
        }

        #region Construction

        internal iCalViewModel(iCalContent icalContent)
        {
            if (icalContent == null)
                throw new ArgumentNullException("icalContent");

            m_iCalContent = icalContent;
        }

        #endregion

        #region Properties

        internal string Summary
        {
            get
            {
                return m_iCalContent.Summary;
            }
            set
            {
                if (m_iCalContent.Summary == value)
                    return;
                m_iCalContent.Summary = value;
                PropertyChanged.Raise(() => Summary);
            }
        }

        internal DateTime? StartTime
        {
            get
            {
                return m_iCalContent.StartTime;
            }
            set
            {
                if (m_iCalContent.StartTime == value)
                    return;
                m_iCalContent.StartTime = value;
                PropertyChanged.Raise(() => StartTime);
            }
        }

        internal DateTime? EndTime
        {
            get
            {
                return m_iCalContent.EndTime;
            }
            set
            {
                if (m_iCalContent.EndTime == value)
                    return;
                m_iCalContent.EndTime = value;
                PropertyChanged.Raise(() => EndTime);
            }
        }

        #endregion

        #region IDataErrorInfo

        string IDataErrorInfo.Error
        {
            get { return (m_iCalContent as IDataErrorInfo).Error; }
        }

        string IDataErrorInfo.this[string propertyName]
        {
            get
            {
                string error = null;

                error = (m_iCalContent as IDataErrorInfo)[propertyName];

                return error;
            }
        }

        #endregion

    }
}
