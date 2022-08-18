using AudioScript.Helpers;
using AudioScript.Midi;
using System.Collections.Generic;

namespace AudioScript.Data
{
    public class Pattern
    {
        private int m_letter;
        private int m_digit;
        private int m_length;
        private List<Note> m_notes;

        public Pattern(int letter, int digit, int length)
        {
            m_letter = letter;
            m_digit = digit;
            m_length = length;
            m_notes = new List<Note>();
        }

        public int Letter => m_letter;
        public int Digit => m_digit;
        
        public int Length
        {
            get => m_length;
            set { m_length = value; }
        }

        public List<Note> Notes => m_notes;

        public void AddNote(Note note)
        {
            m_notes.Add(note);
        }

        public void RemoveNote(Note note)
        {
            m_notes.Remove(note);
        }

        public void RemoveAllNotes()
        {
            m_notes.Clear();
        }

        public void Generate(int scale, Mode mode, int channel, int bar, int chord, List<MidiNoteEvent> events)
        {
            foreach(var note in m_notes)
            {
                var midiNoteEvent = new MidiNoteEvent(channel, mode.Map(note.NoteIndex, scale, chord), 
                    bar * 96 * 4 + note.Start, note.Duration, note.Velocity);
                events.Add(midiNoteEvent);
            }
        }

        public override string ToString()
        {
            return PatternNaming.ToLetterDigitString(m_letter, m_digit);
        }
    }
}
