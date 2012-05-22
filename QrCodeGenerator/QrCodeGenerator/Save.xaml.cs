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
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;
using Microsoft.Win32;

namespace QrCodeGenerator
{
    /// <summary>
    /// Interaction logic for Save.xaml
    /// </summary>
    public partial class Save : Window
    {
        public Save()
        {
            InitializeComponent();
            wtbSize.Text = Properties.Settings.Default.SaveSize.ToString();
        }

        internal BitMatrix QrMatrix { get; set; }
        internal Color DarkColor { get; set; }
        internal Color LightColor { get; set; }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            double inch;

            if (!double.TryParse(wtbSize.Text, out inch))
            {
                SetUpUnvalideControl(wtbSize, "Please input numbers for image size.");
                return;
            }
            else
            {
                SetUpValidControl(wtbSize);
            }

            if (inch <= 0)
            {
                SetUpUnvalideControl(wtbSize, "Size can not be negative or zero"); 
            }

            this.SaveImageFile(inch);
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


        private void SaveImageFile(double imageSize)
        {
            SaveFileDialog saveFileDIalog = new SaveFileDialog();
            saveFileDIalog.Filter =
                @"PNG (.png)|*.png|Bitmap (.bmp)|*.bmp|JEPG (.jpg)|*.jpg|GIF (.gif)|*.gif|TIFF (.tiff)|*.tiff|WDP (.wdp)|*.wdp|EPS (.eps)|*.eps";
            saveFileDIalog.DefaultExt = ".png";
            saveFileDIalog.FileName = "QrCode";
            saveFileDIalog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

            if (saveFileDIalog.ShowDialog() == false)
                return;

            string fileName = saveFileDIalog.FileName;
            ImageFormatEnum imageFormat = ImageFormatEnum.PNG;
            string fileNameCheck = saveFileDIalog.FileName.ToLower();

            if (fileNameCheck.EndsWith("png"))
                imageFormat = ImageFormatEnum.PNG;
            else if (fileNameCheck.EndsWith("bmp"))
                imageFormat = ImageFormatEnum.BMP;
            else if (fileNameCheck.EndsWith("jpg"))
                imageFormat = ImageFormatEnum.JPEG;
            else if (fileNameCheck.EndsWith("gif"))
                imageFormat = ImageFormatEnum.GIF;
            else if (fileNameCheck.EndsWith("tiff"))
                imageFormat = ImageFormatEnum.TIFF;
            else if (fileNameCheck.EndsWith("wdp"))
                imageFormat = ImageFormatEnum.WDP;
            else if (fileNameCheck.EndsWith("eps"))
            {
                Properties.Settings.Default.SaveSize = imageSize;
                Properties.Settings.Default.Save();
                CreateEPSImage(imageSize, fileName);
                return;
            }
            else
                return;

            Properties.Settings.Default.SaveSize = imageSize;
            Properties.Settings.Default.Save();
            int pixelSize = (int)(imageSize * 96);

            DrawingBrushRenderer dRenderer = new DrawingBrushRenderer(new FixedCodeSize(pixelSize, QuietZoneModules.Two),
                new SolidColorBrush(DarkColor), new SolidColorBrush(LightColor));

            using (FileStream stream = new FileStream(fileName, FileMode.Create))
            {

                dRenderer.WriteToStream(QrMatrix, imageFormat, stream);
            }
        }

        private void CreateEPSImage(double imageSize, string fileName)
        {
            int pixelSize = (int)(imageSize * 72);

            EncapsulatedPostScriptRenderer epsRenderer = new EncapsulatedPostScriptRenderer(new FixedCodeSize(pixelSize, QuietZoneModules.Two),
                new EPSMediaColor(DarkColor), new EPSMediaColor(LightColor));

            using (FileStream stream = new FileStream(fileName, FileMode.Create))
            {
                epsRenderer.WriteToStream(QrMatrix, stream);
            }
        }

    }
}
