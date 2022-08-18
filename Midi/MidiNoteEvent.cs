using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioScript.Midi
{
    public class MidiNoteEvent
    {
        private int m_channel;
        private int m_note;
        private int m_start;
        private int m_duration;
        private int m_velocity;

        public MidiNoteEvent(int channel, int note, int start, int duration, int velocity)
        {
            m_channel = channel;
            m_note = note;
            m_start = start;
            m_duration = duration;
            m_velocity = velocity;
        }

        public int Channel => m_channel;
        public int Note => m_note;
        public int Start => m_start;
        public int Duration => m_duration;
        public int Velocity => m_velocity;
    }
}
