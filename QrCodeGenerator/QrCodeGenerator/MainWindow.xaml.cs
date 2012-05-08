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
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Gma.QrCodeNet.Encoding.Windows.WPF;
using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;
using Microsoft.Win32;
using System.IO;

namespace QrCodeGenerator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

       
        /// <summary>
        /// Check input and return validation result
        /// </summary>
        /// <returns>true if validation successful</returns>
        private bool ValidationInput(int index)
        {
            switch (index)
            {
                case 0:
                    if (UIValidation.ValidateURI(wtbURLUri))
                    {
                        qrCodeGeoControl.Text = wtbURLUri.Text;
                        return true;
                    }
                    else
                        return false;
                case 1:
                    if (isMeCardValid())
                    {
                        MeCardGenerate();
                        return true;
                    }
                    else
                        return false;
                case 2:
                    return true;
                case 3:
                    return true;
                default:
                    throw new ArgumentOutOfRangeException("index", "Not support such index");
            }
            throw new NotImplementedException();
        }

        private bool isMeCardValid()
        {
            bool isValid = true;
            if (!UIValidation.ValidateRequiredTextBox(tbMeFirstName))
                isValid = false;
            if(!UIValidation.RegexValidate(wtbMePhone, UIValidation.PhoneReg, "Invalide phone format"))
                isValid = false;
            if (!UIValidation.RegexValidate(wtbMeEmail, UIValidation.EmailReg, "Invalide email format"))
                isValid = false;
            if (!UIValidation.ValidateURI(wtbMeUrl))
                isValid = false;
            if (!UIValidation.ValidateBirthDay(dtpMeBirth))
                isValid = false;
            return isValid;
        }

        private void MeCardGenerate()
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
            qrCodeGeoControl.Text = builder.ToString();
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
            DateTime date = dtpMeBirth.Value.GetValueOrDefault();
            builder.Append("BDAY:");
            builder.Append(date.ToString("yyyyMMdd"));
            builder.Append(';');
            
        }

        private void btnGenerate_Click(object sender, RoutedEventArgs e)
        {
            int index = tcBarcodeContent.SelectedIndex;
            bool valide = ValidationInput(index);
        }

        #region Collapse button
        /// <summary>
        /// Collapse button
        /// </summary>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (columnDMainLeft.ActualWidth == 0)
            {
                RotateArrow(180);
                ShrinkColumnD(false);
            }
            else
            {
                RotateArrow(0);
                ShrinkColumnD(true);
            }
        }


        private void ShrinkColumnD(bool isShrink)
        {
            GridLengthAnimation animation = new GridLengthAnimation();
            if (isShrink)
            {
                animation.From = new GridLength(columnDMainLeft.ActualWidth, GridUnitType.Pixel);
                animation.To = new GridLength(0, GridUnitType.Pixel);
            }
            else
            {
                animation.From = new GridLength(0, GridUnitType.Pixel);
                animation.To = new GridLength(300, GridUnitType.Pixel);
            }
            columnDMainLeft.Width = animation.To;
            animation.Duration = new Duration(TimeSpan.FromSeconds(0.5));
            animation.FillBehavior = FillBehavior.Stop;
            
            columnDMainLeft.BeginAnimation(ColumnDefinition.WidthProperty, animation);
        }

        private void RotateArrow(int initialAngle)
        {
            RotateTransform transform = new RotateTransform();
            transform.Angle = initialAngle;
            transform.CenterX = 0;
            transform.CenterY = 0;
            pathArrow.RenderTransform = transform;

            DoubleAnimation animation = new DoubleAnimation();
            animation.From = transform.Angle;
            animation.To = transform.Angle + 180;
            animation.Duration = new Duration(TimeSpan.FromSeconds(0.5));

            Storyboard story = new Storyboard();
            PropertyPath propertyPath = new PropertyPath("(0).(1)", new DependencyProperty[]{
                UIElement.RenderTransformProperty, RotateTransform.AngleProperty});
            Storyboard.SetTargetProperty(story, propertyPath);
            story.Children.Add(animation);
            story.Begin(pathArrow);
        }

        #endregion

        #region Clear Button

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            int index = cbSelectContent.SelectedIndex;
            ClearField(index);
            ValidationInput(index);
        }

        private void ClearField(int selectedIndex)
        {
            switch (selectedIndex)
            {
                case 0:
                    wtbURLUri.Text = string.Empty;
                    break;
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
                default:
                    throw new ArgumentOutOfRangeException("selectedIndex",
                        string.Format("SelectedIndex {0} not support", selectedIndex.ToString()));
            }
        }

        #endregion

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            About about = new About();
            about.Left = mainWindows.Left + 50;
            about.Top = mainWindows.Top + 50;
            about.Show();
        }

        #region splitter drag event
        private void gridSplitter_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            if (columnDMainLeft.ActualWidth == 0)
            {
                ChangeArrowDirection(180);
            }
            else
            {
                ChangeArrowDirection(0);
            }
        }

        private void ChangeArrowDirection(int targetDegree)
        {
            RotateTransform transform = new RotateTransform();
            transform.Angle = targetDegree;
            transform.CenterX = 0;
            transform.CenterY = 0;
            pathArrow.RenderTransform = transform;
        }

        #endregion

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Save save = new Save();
            save.Left = mainWindows.Left + 10;
            save.Top = mainWindows.Top + 5;
            save.QrMatrix = qrCodeGeoControl.GetQrMatrix();
            save.DarkColor = cpDark.SelectedColor;
            save.LightColor = cpLight.SelectedColor;
            save.ShowDialog();
        }


    }
}
