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
