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
using System.Text;

namespace QrCodeGenerator
{
    internal static class StringMethod
    {
        internal static string MeCardStringRevamp(string input)
        {
            StringBuilder builder = new StringBuilder();
            foreach (char c in input)
            {
                if (c == '\\' || c == ':' || c == ';' || c == ',')
                {
                    builder.Append('\\');
                    builder.Append(c);
                }
                else
                    builder.Append(c);
            }
            return builder.ToString();
        }

        internal static string MeCardPhoneRevamp(string input)
        {
            StringBuilder builder = new StringBuilder();
            foreach (char c in input)
            {
                if (!(c == '(' || c == ')' || c == '-' || c == ' '))
                    builder.Append(c);
            }
            return builder.ToString();
        }

    }
}
