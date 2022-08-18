using System.Collections.Generic;

namespace AudioScript.Data
{
    public class Mode
    {
        private string m_name;
        private List<int> m_notes;
        
        public Mode(string name, int[] notes)
        {
            m_name = name;
            m_notes = new List<int>(notes);
        }

        public string Name
        {
            get => m_name;
            set { m_name = value; }
        }

        public int Map(int note, int scale, int chord)
        {
            var octave = (note + chord) / m_notes.Count;
            var index = (note + chord) % m_notes.Count;
            var mapped = m_notes[index] + scale + octave * 12;
            return mapped;
        }

        public List<int> Notes => m_notes;

        public override string ToString()
        {
            return $"Mode: {m_name} note count {m_notes.Count}";
        }
    }
}