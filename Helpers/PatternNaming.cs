using System;

namespace AudioScript.Helpers
{
    public static class PatternNaming
    {
        public static string ToLetterDigitString(int letter, int digit)
        {
            char l = Convert.ToChar(letter + 'A');
            char d = Convert.ToChar(digit + '1');
            return l.ToString() + d.ToString();
        }
    }
}
