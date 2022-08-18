using System.Collections.Generic;

namespace AudioScript.Data
{
    public class ModeManager
    {
        private List<Mode> m_modes;
        private static ModeManager? m_instance = null;
        public static ModeManager Instance => GetInstance();
        private ModeManager()
        {
            m_modes = new List<Mode>();
        }

        private static ModeManager GetInstance()
        {
            if (m_instance == null)
                m_instance = new ModeManager();

            return m_instance;
        }

        public List<Mode> Modes => m_modes;

        public Mode? FindMode(string name)
        {
            foreach(var mode in m_modes)
            {
                if (mode.Name == name)
                    return mode;
            }

            return null;
        }

        public Mode AddMode(string name, int[] notes)
        {
            var mode = new Mode(name, notes);
            m_modes.Add(mode);
            return mode;
        }

        public void RemoveMode(string name)
        {
            var mode = FindMode(name);

            if (mode == null)
                return;

            m_modes.Remove(mode);
        }
    }
}
