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
    /// Interaction logic for vCardPage.xaml
    /// </summary>
    public partial class vCardPage : UserControl
    {
        public vCardPage()
        {
            InitializeComponent();
        }

        internal bool isVCardValide(out string vCardStr)
        {
            bool isValid = true;
            vCardStr = string.Empty;
            if (!UIValidation.ValidateRequiredTextBox(tbVFirstName))
                isValid = false;
            if (!UIValidation.RegexValidate(wtbVMobile, UIValidation.PhoneReg, "Invalide mobile format", false))
                isValid = false;
            if (!UIValidation.RegexValidate(wtbVPhoneHome, UIValidation.PhoneReg, "Invalide mobile format", false))
                isValid = false;
            if (!UIValidation.RegexValidate(wtbVPhoneWork, UIValidation.PhoneReg, "Invalide mobile format", false))
                isValid = false;
            if (!UIValidation.RegexValidate(wtbVFax, UIValidation.PhoneReg, "Invalide mobile format", false))
                isValid = false;
            if (!UIValidation.RegexValidate(wtbVFaxWork, UIValidation.PhoneReg, "Invalide mobile format", false))
                isValid = false;
            if (!UIValidation.RegexValidate(wtbVEmail, UIValidation.EmailReg, "Invalide mobile format", false))
                isValid = false;
            if (!UIValidation.ValidateURI(wtbVURL, false))
                isValid = false;
            if (!UIValidation.ValidateBirthDay(dtpVBirth))
                isValid = false;

            if (isValid)
                vCardStr = VCardGenerate();
            return isValid;
        }

        private string VCardGenerate()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("BEGIN:VCARD\n");
            //Name
            VCardNameAppend(ref builder, tbVLastName.Text, tbVFirstName.Text);
            //Mobile
            VPhoneAppend(ref builder, wtbVMobile.Text, "TEL;CELL:");
            //Title
            VBuilderAppend(ref builder, tbVTitle.Text, "TITLE:");
            //Organisation
            VBuilderAppend(ref builder, tbVOrganization.Text, "ORG:");
            //Phone (Home)
            VPhoneAppend(ref builder, wtbVPhoneHome.Text, "TEL;HOME:");
            //Phone (Work)
            VPhoneAppend(ref builder, wtbVPhoneWork.Text, "TEL;WORK");
            //Fax
            VPhoneAppend(ref builder, wtbVFax.Text, "TEL;HOME;FAX:");
            //Fax (Work)
            VPhoneAppend(ref builder, wtbVFaxWork.Text, "TEL;WORK;FAX:");
            //Email
            VBuilderAppend(ref builder, wtbVEmail.Text, "EMAIL:");
            //URL
            VBuilderAppend(ref builder, wtbVURL.Text, "URL:");
            //Note
            VBuilderAppend(ref builder, tbVNote.Text, "NOTE:");
            //BirthDay
            VBirthdayAppend(ref builder);
            //Address
            VBuilderAppend(ref builder, tbVADR.Text, "ADR;WORK:");
            builder.Append("END:VCARD");
            return builder.ToString();
        }

        private void VCardNameAppend(ref StringBuilder builder, string lastName, string firstName)
        {
            builder.Append("N:");
            if (!string.IsNullOrWhiteSpace(lastName))
                builder.Append(lastName.TrimStart(' ').TrimEnd(' '));
            builder.Append(';');
            builder.Append(firstName.TrimStart(' ').TrimEnd(' '));
            builder.Append('\n');
        }

        private void VPhoneAppend(ref StringBuilder builder, string input, string header)
        {
            if (string.IsNullOrWhiteSpace(input))
                return;
            builder.Append(header);
            builder.Append(StringMethod.MeCardPhoneRevamp(input));
            builder.Append('\n');
        }

        private void VBuilderAppend(ref StringBuilder builder, string input, string header)
        {
            if (string.IsNullOrWhiteSpace(input))
                return;
            builder.Append(header);
            builder.Append(input.TrimStart(' ').TrimEnd(' '));
            builder.Append('\n');
        }

        private void VBirthdayAppend(ref StringBuilder builder)
        {
            if (!dtpVBirth.Value.HasValue)
                return;
            builder.Append("BDAY:");
            builder.Append(StringMethod.MeCardBirthRevamp(dtpVBirth.Value));
            builder.Append('\n');
        }

        internal void Clear()
        {
            tbVFirstName.Text = string.Empty;
            UIValidation.SetUpValidControl(tbVFirstName);
            tbVLastName.Text = string.Empty;
            tbVNote.Text = string.Empty;
            tbVOrganization.Text = string.Empty;
            tbVTitle.Text = string.Empty;
            tbVADR.Text = string.Empty;
            wtbVEmail.Text = string.Empty;
            UIValidation.SetUpValidControl(wtbVEmail);
            wtbVFax.Text = string.Empty;
            UIValidation.SetUpValidControl(wtbVFax);
            wtbVFaxWork.Text = string.Empty;
            UIValidation.SetUpValidControl(wtbVFaxWork);
            wtbVMobile.Text = string.Empty;
            UIValidation.SetUpValidControl(wtbVMobile);
            wtbVPhoneHome.Text = string.Empty;
            UIValidation.SetUpValidControl(wtbVPhoneHome);
            wtbVPhoneWork.Text = string.Empty;
            UIValidation.SetUpValidControl(wtbVPhoneWork);
            wtbVURL.Text = string.Empty;
            UIValidation.SetUpValidControl(wtbVURL);
            dtpVBirth.Value = null;
            UIValidation.SetUpValidControl(dtpVBirth);
        }

    }
}
