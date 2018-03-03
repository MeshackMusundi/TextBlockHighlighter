using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBlockHighlighterCS
{
    public static class Extensions
    {
        public static Boolean Contains(this string str, string txt, StringComparison c)
        {
            if (txt != null) return str.IndexOf(txt, c) >= 0;
            return false;
        }
    }
}
