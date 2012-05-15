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

namespace QrCodeGenerator.Model
{
    internal sealed class TextContent : QrContent, IDataErrorInfo
    {

        #region Creation

        internal static TextContent CreateNewContent()
        {
            return new TextContent();
        }

        internal TextContent()
        { }

        #endregion

        #region Properties

        /// <summary>
        /// Get/sets the Text
        /// </summary>
        internal string Text { get; set; }

        #endregion

        #region IDataErrorInfo Members

        string IDataErrorInfo.Error { get { return null; } }

        string IDataErrorInfo.this[string PropertyName]
        {
            get
            {
                return null;
            }
        }

        #endregion

        #region Inheritance QrContent

        internal override string GetContent
        {
            get { return this.Text; }
        }

        #endregion

        #region Validation

        internal override bool IsValid
        {
            get
            {
                return true;
            }
        }

        #endregion

    }
}
