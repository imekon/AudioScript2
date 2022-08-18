using AudioScript.Helpers;

namespace AudioScript.Data
{
    public class Reference
    {
        private int m_letter;
        private int m_digit;
        private int m_bar;

        public Reference(int letter, int digit, int bar)
        {
            m_letter = letter;
            m_digit = digit;
            m_bar = bar;
        }

        public int Letter
        {
            get => m_letter;
            set { m_letter = value; }
        }

        public int Digit
        {
            get => m_digit;
            set { m_digit = value; }
        }

        public int Bar
        {
            get => m_bar;
            set { m_bar = value; }
        }

        public override string ToString()
        {
            return PatternNaming.ToLetterDigitString(m_letter, m_digit);
        }
    }
}
