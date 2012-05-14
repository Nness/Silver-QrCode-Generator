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

namespace QrCodeGenerator.TabPage
{
    /// <summary>
    /// Interaction logic for MeCardPage.xaml
    /// </summary>
    public partial class MeCardPage : UserControl
    {
        public MeCardPage()
        {
            InitializeComponent();
        }

        internal bool isMeCardValid(out string mecardStr)
        {
            bool isValid = true;
            mecardStr = string.Empty;
            if (!UIValidation.ValidateRequiredTextBox(tbMeFirstName))
                isValid = false;
            if (!UIValidation.RegexValidate(wtbMePhone, UIValidation.PhoneReg, "Invalide phone format", false))
                isValid = false;
            if (!UIValidation.RegexValidate(wtbMeEmail, UIValidation.EmailReg, "Invalide email format", false))
                isValid = false;
            if (!UIValidation.ValidateURI(wtbMeUrl, false))
                isValid = false;
            if (!UIValidation.ValidateBirthDay(dtpMeBirth))
                isValid = false;
            if (isValid)
                mecardStr = MeCardGenerate();
            return isValid;
        }

        private string MeCardGenerate()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("MECARD:");
            //Name
            builder.Append("N:");
            string tempStr = tbMeLastName.Text;
            if (!string.IsNullOrWhiteSpace(tempStr))
                builder.Append(StringMethod.MeCardStringRevamp(tempStr));
            builder.Append(',');
            builder.Append(StringMethod.MeCardStringRevamp(tbMeFirstName.Text));
            builder.Append(';');
            //Phone
            MePhoneAppend(ref builder, wtbMePhone.Text);
            //Email
            MeBuilderAppend(ref builder, wtbMeEmail.Text, "EMAIL:");
            //URL
            MeBuilderAppend(ref builder, wtbMeUrl.Text, "URL:");
            //Birthday
            MeBirthdayAppend(ref builder);
            //Memo
            MeBuilderAppend(ref builder, tbMeMemo.Text, "NOTE:");
            //Address
            MeBuilderAppend(ref builder, tbMeAddress.Text, "ADR:");
            builder.Append(';');
            return builder.ToString();
        }

        private void MePhoneAppend(ref StringBuilder builder, string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return;
            builder.Append("TEL:");
            builder.Append(StringMethod.MeCardPhoneRevamp(input));
            builder.Append(';');
        }

        private void MeBuilderAppend(ref StringBuilder builder, string input, string header)
        {
            if (string.IsNullOrWhiteSpace(input))
                return;
            builder.Append(header);
            builder.Append(StringMethod.MeCardStringRevamp(input));
            builder.Append(';');
        }

        private void MeBirthdayAppend(ref StringBuilder builder)
        {
            if (!dtpMeBirth.Value.HasValue)
                return;
            builder.Append("BDAY:");
            builder.Append(StringMethod.MeCardBirthRevamp(dtpMeBirth.Value));
            builder.Append(';');
        }

        internal void Clear()
        {
            tbMeFirstName.Text = string.Empty;
            UIValidation.SetUpValidControl(tbMeFirstName);
            tbMeLastName.Text = string.Empty;
            tbMeMemo.Text = string.Empty;
            tbMeAddress.Text = string.Empty;
            wtbMeEmail.Text = string.Empty;
            UIValidation.SetUpValidControl(wtbMeEmail);
            wtbMePhone.Text = string.Empty;
            UIValidation.SetUpValidControl(wtbMePhone);
            wtbMeUrl.Text = string.Empty;
            UIValidation.SetUpValidControl(wtbMeUrl);
            dtpMeBirth.Value = null;
            UIValidation.SetUpValidControl(dtpMeBirth);
        }
    }
}
