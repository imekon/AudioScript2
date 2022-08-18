using System.Collections.Generic;
using AudioScript.Logging;

namespace AudioScript.Data
{
    public class Note : TimingEvent
    {
        private int m_note;
        private int m_velocity;
        private Dictionary<string, int> m_properties;

        public Note(int note, int start, int duration, int velocity) : base(start, duration)
        {
            m_note = note;
            m_velocity = velocity;
            m_properties = new Dictionary<string, int>();
        }

        public int NoteIndex
        {
            get => m_note;
            set 
            {
                Logger.Debug($"note {value}");
                m_note = value; 
            }
        }

        public int Velocity
        {
            get => m_velocity;
            set 
            {
                Logger.Debug($"velocity {value}");
                m_velocity = value; 
            }
        }
    }
}
