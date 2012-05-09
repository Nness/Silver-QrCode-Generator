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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Xceed.Wpf.Toolkit;

namespace QrCodeGenerator.TabPage
{
    /// <summary>
    /// Interaction logic for iCalPage.xaml
    /// </summary>
    public partial class iCalPage : UserControl
    {
        public iCalPage()
        {
            InitializeComponent();
        }

        internal bool isCalValid(out string calStr)
        {
            bool isValid = true;
            calStr = string.Empty;
            if (!UIValidation.ValidateRequiredTextBox(tbiCalSummary))
                isValid = false;
            if (!ValidateCalTime())
                isValid = false;
            if (isValid)
                calStr = CalGenerate();
            return isValid;
        }

        private bool ValidateCalTime()
        {
            if (!ValidateTime(dtpiCalStart, "Please input start time."))
                return false;
            if (!ValidateTime(dtpiCalEnd, "Please input end time"))
                return false;
            if (dtpiCalEnd.Value.GetValueOrDefault() < dtpiCalStart.Value.GetValueOrDefault())
            {
                UIValidation.SetUpUnvalideControl(dtpiCalEnd, "End time must be later than start time.");
                return false;
            }
            else
            {
                UIValidation.SetUpValidControl(dtpiCalEnd);
                return true;
            }
        }

        private bool ValidateTime(DateTimePicker dtp, string message)
        {
            if (!dtp.Value.HasValue)
            {
                UIValidation.SetUpUnvalideControl(dtp, message);
                return false;
            }
            else
                UIValidation.SetUpValidControl(dtp);
            return true;
        }

        private string CalGenerate()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("BEGIN:VEVENT\n");
            BuilderAppend(ref builder, tbiCalSummary.Text, "SUMMARY:");
            BuilderAppend(ref builder, StringMethod.iCalDateTimeRevamp(dtpiCalStart), "DTSTART:");
            BuilderAppend(ref builder, StringMethod.iCalDateTimeRevamp(dtpiCalEnd), "DTEND:");
            builder.Append("END:VEVENT");
            return builder.ToString();
        }

        private void BuilderAppend(ref StringBuilder builder, string input, string header)
        {
            if (string.IsNullOrWhiteSpace(input))
                return;
            builder.Append(header);
            builder.Append(input.TrimStart(' ').TrimEnd(' '));
            builder.Append('\n');
        }

        internal void Clear()
        {
            tbiCalSummary.Text = string.Empty;
            UIValidation.SetUpValidControl(tbiCalSummary);
            dtpiCalStart.Value = null;
            UIValidation.SetUpValidControl(dtpiCalStart);
            dtpiCalEnd.Value = null;
            UIValidation.SetUpValidControl(dtpiCalEnd);
        }
    }
}
