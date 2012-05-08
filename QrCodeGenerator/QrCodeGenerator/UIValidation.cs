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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Xceed.Wpf.Toolkit;
using System.Text.RegularExpressions;

namespace QrCodeGenerator
{
    internal static class UIValidation
    {
        internal static bool ValidateURI(WatermarkTextBox wmTextBox)
        {
            string input = wmTextBox.Text.TrimStart(' ').TrimEnd(' ');
            wmTextBox.Text = input;

            if (string.IsNullOrEmpty(input))
            {
                SetUpValidControl(wmTextBox);
                return true;
            }

            
            
            try
            {
                Uri uri = new Uri(input);
                SetUpValidControl(wmTextBox);
                return true;
            }
            catch (UriFormatException e)
            {
                SetUpUnvalideControl(wmTextBox, e.Message);
                return false;
            }
        }

        internal static void SetUpValidControl(Control control)
        {
            control.ToolTip = null;
            control.BorderBrush = null;
        }

        internal static void SetUpUnvalideControl(Control control, string errorMessage)
        {
            ToolTip ttip = new ToolTip();
            ttip.Content = errorMessage;
            ttip.Opacity = 0.9;

            ttip.Background = Brushes.LightPink;
            control.BorderBrush = Brushes.LightPink;
            control.ToolTip = ttip;
        }

        internal static bool ValidateRequiredTextBox(TextBox textBox)
        {
            string input = textBox.Text.TrimStart(' ').TrimEnd(' ');
            textBox.Text = input;

            if (string.IsNullOrEmpty(input))
            {
                SetUpUnvalideControl(textBox, "Must not be empty.");
                return false;
            }
            else
            {
                SetUpValidControl(textBox);
                return true;
            }
        }

        internal const string PhoneReg = @"^[+]?[0-9\(\)\- ]{1,24}$";
        internal const string EmailReg = @"^[a-z0-9._%+-]+@(([a-z0-9.-]+\.[a-z]{2,4})|([0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}))$";

        internal static bool RegexValidate(WatermarkTextBox wmTextbox, string regularStr, string errorMsg)
        {
            string input = wmTextbox.Text.TrimStart(' ').TrimEnd(' ');
            wmTextbox.Text = input;

            if (string.IsNullOrEmpty(input))
            {
                SetUpValidControl(wmTextbox);
                return true;
            }

            Regex re = new Regex(regularStr);
            if(re.IsMatch(input))
            {
                SetUpValidControl(wmTextbox);
                return true;
            }
            else
            {
                SetUpUnvalideControl(wmTextbox, errorMsg);
                return false;
            }
            
        }

        internal static bool ValidateBirthDay(DateTimePicker dtPicker)
        {
            if (!dtPicker.Value.HasValue)
            {
                SetUpValidControl(dtPicker);
                return true;
            }

            DateTime dateTime = dtPicker.Value.GetValueOrDefault();
            if (dateTime < DateTime.Today)
            {
                SetUpValidControl(dtPicker);
                return true;
            }
            else
            {
                SetUpUnvalideControl(dtPicker, "Are you still inside womb?");
                dtPicker.ClearValue(DateTimePicker.ValueProperty);
                return false;
            }
        }

    }
}
