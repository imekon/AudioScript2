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

        public static (int letter, int digit) FromLetterDigit(string name)
        {
            char l = name[0];
            char d = name[1];

            int letter = l - 'A';
            int digit = d - '1';

            return (letter, digit);
        }
    }
}
