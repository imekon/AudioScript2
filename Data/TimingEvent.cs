using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioScript.Data
{
    public class TimingEvent
    {
        protected int m_start;
        protected int m_duration;

        public TimingEvent(int start, int duration)
        {
            m_start = start;
            m_duration = duration;
        }

        public int Start
        {
            get => m_start;
            set { m_start = value; }
        }

        public int Duration
        {
            get => m_duration;
            set { m_duration = value; }
        }
    }
}
