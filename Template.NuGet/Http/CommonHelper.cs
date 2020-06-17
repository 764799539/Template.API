using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Template.NuGet
{
    public static class CommonHelper
    {
        public static string StringFilter(this string input) => input.StringFilter(false);

        public static string StringFilter(this string input, bool filterBadWords)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }
            StringBuilder builder1 = new StringBuilder(input);
            builder1.Replace("'", "").Replace(";", "").Replace("/", "").Replace(@"\", "").Replace("*", "").Replace("\"", "").Replace("<", "").Replace(">", "").Replace("|", "");
            bool flag1 = filterBadWords;
            return builder1.ToString();
        }

        public static string SubString(this string str, int count)
        {
            if (string.IsNullOrEmpty(str))
            {
                return "";
            }
            if (str.Length <= count)
            {
                return str;
            }
            if (count <= 3)
            {
                return str.Substring(0, count);
            }
            return (str.Substring(0, count - 3) + "...");
        }

        public static IServiceProvider ServiceProvider { get; set; }

        public static IServiceCollection ServiceCollection { get; set; }
        public static string RootPath { get; set; }
    }

}
