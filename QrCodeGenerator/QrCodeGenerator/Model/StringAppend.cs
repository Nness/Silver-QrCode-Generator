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

namespace QrCodeGenerator.Model
{
    internal static class StringAppend
    {
        /// <summary>
        /// StringBuilder append with new line at end.
        /// </summary>
        /// <param name="input">input content</param>
        /// <param name="header">header of content</param>
        internal static void BuilderAppendNewLine(ref StringBuilder builder, string input, string header)
        {
            if (string.IsNullOrWhiteSpace(input))
                return;
            builder.Append(header);
            builder.Append(input.TrimStart(' ').TrimEnd(' '));
            builder.Append('\n');
        }

        internal static void BuilderAppendMeCardName(ref StringBuilder builder, string lastname, string firstname)
        {
            string tempStr = lastname;
            if (!string.IsNullOrWhiteSpace(tempStr))
                builder.Append(StringMethod.MeCardStringRevamp(tempStr));
            builder.Append(',');
            builder.Append(StringMethod.MeCardStringRevamp(firstname));
            builder.Append(';');
        }

        internal static void BuilderAppendvCardName(ref StringBuilder builder, string lastName, string firstName)
        {
            builder.Append("N:");
            if (!string.IsNullOrWhiteSpace(lastName))
                builder.Append(lastName.TrimStart(' ').TrimEnd(' '));
            builder.Append(';');
            builder.Append(firstName.TrimStart(' ').TrimEnd(' '));
            builder.Append('\n');
        }

        internal static void BuilderAppendSemicolon(ref StringBuilder builder, string input, string header)
        {
            if (string.IsNullOrWhiteSpace(input))
                return;
            builder.Append(header);
            builder.Append(input.TrimStart(' ').TrimEnd(' '));
            builder.Append(';');
        }
    }
}
