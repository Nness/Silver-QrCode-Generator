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
using QrCodeGenerator.Properties;
using System.Diagnostics;

namespace QrCodeGenerator.ViewModel
{
    internal sealed class TabFormViewModel : INotifyPropertyChanged, IDataErrorInfo
    {
        private QrViewModelBase[] m_QrViewModelBase =
        {
            new UrlViewModel(),
            new MeCardViewModel(),
            new vCardViewModel(),
            new PhoneViewModel(),
            new SMSViewModel(),
            new EmailViewModel(),
            new iCalViewModel(),
            new TextViewModel()
        };

        public event PropertyChangedEventHandler PropertyChanged = null;


        #region properties

        private int m_SelectedIndex = 0;

        public int SelectedIndex
        {
            get
            {
                return m_SelectedIndex;
            }
            set
            {
                if (m_SelectedIndex == value)
                    return;
                m_SelectedIndex = value;
                InputString = string.Empty;     //Reset inputstring whenever selectedIndex changes. 
                PropertyChanged.Raise(() => SelectedIndex);
            }
        }

        public UrlViewModel UrlView
        { get { return (UrlViewModel)m_QrViewModelBase[0]; } }

        public MeCardViewModel MeCardView
        { get { return (MeCardViewModel)m_QrViewModelBase[1]; } }

        public vCardViewModel vCardView
        { get { return (vCardViewModel)m_QrViewModelBase[2];} }

        public PhoneViewModel PhoneView
        { get { return (PhoneViewModel)m_QrViewModelBase[3]; } }

        public SMSViewModel SMSView
        { get { return (SMSViewModel)m_QrViewModelBase[4]; } }

        public EmailViewModel EmailView
        { get { return (EmailViewModel)m_QrViewModelBase[5];} }

        public iCalViewModel iCalView
        { get { return (iCalViewModel)m_QrViewModelBase[6]; } }

        public TextViewModel TextView
        { get { return (TextViewModel)m_QrViewModelBase[7]; } }

        private string m_Input = string.Empty;

        public string InputString
        {
            get
            {
                return m_Input;
            }
            private set
            {
                if (m_Input == value)
                    return;
                m_Input = value;
                PropertyChanged.Raise(() => InputString);
            }
        }

        #endregion

        #region Method

        public void GenerateInputString()
        {
            this.InputString = m_QrViewModelBase[SelectedIndex].GetContent();
        }

        public void Clear()
        {
            m_QrViewModelBase[SelectedIndex].Clear();
            this.InputString = string.Empty;
        }

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

        #region Validation

        private static readonly string[] ValidatedProperties =
        {
            "SelectedIndex"
        };

        internal bool IsValid
        {
            get
            {
                foreach (string property in ValidatedProperties)
                    if (GetValidationError(property) != null)
                        return false;
                return true;
            }
        }

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
                case "SelectedIndex" :
                    if (SelectedIndex >= ValidatedProperties.Length && SelectedIndex < 0)
                        error = Resources.Error_SelectedIndex_OutOfRange;
                    break;
                default:
                    Debug.Fail("Unexpected property being validated on TabFormViewModel: " + propertyName);
                    break;
            }

            return error;
        }

        #endregion

    }
}
