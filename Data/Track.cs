using System.Collections.Generic;
using System.Security.RightsManagement;
using AudioScript.Midi;

namespace AudioScript.Data
{
    public class Track
    {
        private string m_name;
        private string m_device;
        private int m_channel;
        private int m_instrument;
        private List<Reference> m_references;
        private List<Pattern> m_patterns;

        public Track(string name, string device, int channel, int instrument)
        {
            m_name = name;
            m_device = device;
            m_channel = channel;
            m_instrument = instrument;
            m_references = new List<Reference>();
            m_patterns = new List<Pattern>();
        }

        public Track() : this("untitled", "unknown", -1, -1)
        {

        }

        public string Name
        {
            get { return m_name; }
            set { m_name = value; }
        }

        public string Device
        {
            get { return m_device; }
            set { m_device = value; }
        }

        public int Channel
        {
            get { return m_channel; }
            set { m_channel = value; }
        }

        public int Instrument
        {
            get { return m_instrument; }
            set { m_instrument = value; }
        }

        public List<Pattern> Patterns => m_patterns;

        public List<Reference> References => m_references;

        public void CreatePattern(int letter, int digit, int length)
        {
            var pattern = new Pattern(letter, digit, length);
            m_patterns.Add(pattern);
        }

        public void AddPattern(Pattern pattern)
        {
            m_patterns.Add(pattern);
        }

        public Pattern? FindPattern(int letter, int digit)
        {
            foreach(var pattern in m_patterns)
            {
                if (pattern.Letter == letter && pattern.Digit == digit)
                    return pattern;
            }

            return null;
        }

        public Reference? FindReference(int bar)
        {
            foreach(var reference in m_references)
            {
                if (reference.Bar == bar)
                    return reference;
            }

            return null;
        }

        public void RemovePattern(int letter, int digit)
        {
            var pattern = FindPattern(letter, digit);
            if (pattern == null)
                return;

            m_patterns.Remove(pattern);
        }

        public void AddReference(int bar, int letter, int digit)
        {
            var reference = FindReference(bar);
            if (reference != null)
            {
                reference.Letter = letter;
                reference.Digit = digit;
            }
            else
            {
                reference = new Reference(letter, digit, bar);
                m_references.Add(reference);
            }
        }

        public void RemoveReference(int bar)
        {
            var reference = FindReference(bar);
            if (reference == null)
                return;

            m_references.Remove(reference);
        }

        public void RemoveAllReferences()
        {
            m_references.Clear();
        }

        public void Generate(int scale, Mode mode, int[] chords, List<MidiNoteEvent> events)
        {
            for (int bar = 0; bar < 128; bar++)
            {
                var reference = FindReference(bar);
                if (reference == null)
                    continue;

                var pattern = FindPattern(reference.Letter, reference.Digit);
                if (pattern == null)
                    continue;

                pattern.Generate(scale, mode, m_channel, bar, chords[bar], events);
            }
        }

        public override string ToString()
        {
            return $"Track: {m_name}";
        }
    }
}
