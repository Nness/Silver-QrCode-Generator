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
using QrCodeGenerator.ViewModel;
using Microsoft.Win32;
using System.IO;

namespace QrCodeGenerator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TabFormViewModel m_TabFormViewModel = new TabFormViewModel();

        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = m_TabFormViewModel;
        }

       
        /// <summary>
        /// Check input and return validation result
        /// </summary>
        /// <returns>true if validation successful</returns>
        //private void ValidationInput(int index)
        //{
        //    switch (index)
        //    {
        //        case 0:
        //            if (UIValidation.ValidateURI(wtbURLUri, true))
        //                qrCodeGeoControl.Text = wtbURLUri.Text;
        //            break;
        //        case 1:
        //            string mecardStr;
        //            if (pageMeCard.isMeCardValid(out mecardStr))
        //                qrCodeGeoControl.Text = mecardStr;
        //            break;
        //        case 2:
        //            string vcardStr;
        //            if (pageVCard.isVCardValide(out vcardStr))
        //                qrCodeGeoControl.Text = vcardStr;
        //            break;
        //        case 3:
        //            string phoneStr;
        //            if (pagePhone.isPhoneValid(out phoneStr))
        //                qrCodeGeoControl.Text = phoneStr;
        //            break;
        //        case 4:
        //            string smsStr;
        //            if (pageSMS.isSMSValid(out smsStr))
        //                qrCodeGeoControl.Text = smsStr;
        //            break;
        //        case 5:
        //            string emailStr;
        //            if (pageEmail.isEmailValid(out emailStr))
        //                qrCodeGeoControl.Text = emailStr;
        //            break;
        //        case 6:
        //            string iCalStr;
        //            if (pageiCal.isCalValid(out iCalStr))
        //                qrCodeGeoControl.Text = iCalStr;
        //            break;
        //        case 7:
        //            if (UIValidation.ValidateRequiredTextBox(tbText))
        //                qrCodeGeoControl.Text = tbText.Text;
        //            break;
        //        default:
        //            throw new ArgumentOutOfRangeException("index", "Not support such index");
        //    }
        //}

        

        //private void btnGenerate_Click(object sender, RoutedEventArgs e)
        //{
        //    int index = tcBarcodeContent.SelectedIndex;
        //    ValidationInput(index);
        //}

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

        //private void btnClear_Click(object sender, RoutedEventArgs e)
        //{
        //    int index = cbSelectContent.SelectedIndex;
        //    ClearField(index);
        //}

        //private void ClearField(int selectedIndex)
        //{
        //    switch (selectedIndex)
        //    {
        //        case 0:
        //            wtbURLUri.Text = string.Empty;
        //            UIValidation.SetUpValidControl(wtbURLUri);
        //            break;
        //        case 1:
        //            pageMeCard.Clear();
        //            break;
        //        case 2:
        //            pageVCard.Clear();
        //            break;
        //        case 3:
        //            pagePhone.Clear();
        //            break;
        //        case 4:
        //            pageSMS.Clear();
        //            break;
        //        case 5:
        //            pageEmail.Clear();
        //            break;
        //        case 6:
        //            pageiCal.Clear();
        //            break;
        //        case 7:
        //            tbText.Text = string.Empty;
        //            UIValidation.SetUpValidControl(tbText);
        //            break;
        //        default:
        //            throw new ArgumentOutOfRangeException("selectedIndex",
        //                string.Format("SelectedIndex {0} not support", selectedIndex.ToString()));
        //    }
        //    qrCodeGeoControl.Text = string.Empty;
        //}

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
