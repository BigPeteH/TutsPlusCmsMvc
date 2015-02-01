using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace TutsPlusCmsMvc.Utility
{
    public static class StringExtensions
    {
        public static string MakeUrlFriendly(this string input)
        {
            input = input.ToLowerInvariant().Replace(" ", "-");
            input = Regex.Replace(input, @"[^0-9a-z-]", string.Empty);

            return input;
        }
    }
}