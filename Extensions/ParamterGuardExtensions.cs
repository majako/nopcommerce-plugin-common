using System;
using System.Collections.Generic;
using System.Text;

namespace Majako.Plugin.Common.Extensions
{
    public static class ParamterGuardExtensions
    {
        public static void NotNull<T>(this T source, string parameterName) where T : class
        {
            parameterName.NotEmpty(nameof(parameterName));
            if (source == null)
                throw new ArgumentNullException(parameterName);
        }

        public static void NotEmpty(this string source, string parameterName)
        {
            if (string.IsNullOrWhiteSpace(parameterName))
                throw new ArgumentException(nameof(parameterName));
            
            if (string.IsNullOrWhiteSpace(source))
                throw new ArgumentException(parameterName);
        }
        public static void NotEmpty(this string source)
        {
            if (string.IsNullOrWhiteSpace(source))
                throw new ArgumentException($"Input is empty!");
        }
    }
}
