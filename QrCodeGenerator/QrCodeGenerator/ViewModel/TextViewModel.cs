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
    internal sealed class TextViewModel : QrViewModelBase, INotifyPropertyChanged, IDataErrorInfo
    {

        private readonly TextContent m_TextContent;

        public event PropertyChangedEventHandler PropertyChanged = null;

        internal override string GetContent()
        {
            return m_TextContent.GetContent;
        }

        #region Construction

        internal TextViewModel()
        {
            m_TextContent = new TextContent();
        }

        internal TextViewModel(TextContent textContent)
        {
            if (textContent == null)
                throw new ArgumentNullException("textContent");

            m_TextContent = textContent;
        }

        #endregion

        #region Properties

        public string Text
        {
            get
            {
                return m_TextContent.Text;
            }
            set
            {
                if (m_TextContent.Text == value)
                    return;
                m_TextContent.Text = value;
                PropertyChanged.Raise(() => Text);
            }
        }

        #endregion

        #region IDataErrorInfo

        string IDataErrorInfo.Error
        {
            get { return (m_TextContent as IDataErrorInfo).Error; }
        }

        string IDataErrorInfo.this[string propertyName]
        {
            get
            {
                string error = null;

                error = (m_TextContent as IDataErrorInfo)[propertyName];

                return error;
            }
        }

        #endregion

        internal override void Clear()
        {
            this.Text = string.Empty;
        }
    }
}
