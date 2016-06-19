using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MerchantsGuideToGalaxy
{
    public class Util
    {
        /// <summary>
        /// Splits a sentence on the word "is"
        /// </summary>
        /// <param name="sentence"></param>
        /// <returns>Substrings containing text before and after the word "is"</returns>
        public static string[] SplitByIs(string sentence)
        {
            Regex regex = new Regex(@" is ");
            return regex.Split(sentence);
        }
    }
}
