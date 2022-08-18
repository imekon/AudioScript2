namespace AudioScript.Data
{
    public class ChordProgression : TimingEvent
    {
        private int m_chord;

        public ChordProgression(int chord, int start, int duration) : base(start, duration)
        {
            m_chord = chord;
        }

        public int Chord
        {
            get => m_chord;
            set { m_chord = value; }
        }
    }
}
