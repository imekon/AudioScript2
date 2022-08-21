using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using AudioScript.Logging;
using AudioScript.Midi;

namespace AudioScript.Data
{
    public class Song
    {
        private string m_title;
        private string m_copyright;
        private int m_ppqn;
        private int m_tempo;
        private Mode? m_mode;
        private int m_scale;
        private int[] m_chords = new int[128];
        //private List<ChordProgression> m_chords;
        private List<Track> m_tracks;

        private static Song? m_instance = null;

        private static Song GetInstance()
        {
            if (m_instance == null)
            {
                m_instance = new Song();
            }

            return m_instance;
        }

        public static Song Instance => GetInstance();

        public Song(string title, int ppqn, int tempo)
        {
            m_title = title;
            m_copyright = "Copyright (c) 2022";
            m_ppqn = ppqn;
            m_tempo = tempo;
            m_tracks = new List<Track>();
            //m_chords = new List<ChordProgression>();
            m_tempo = tempo;
            m_mode = null;
            m_scale = 0;

            for(int bar = 0; bar < 128; bar++)
            {
                m_chords[bar] = -1;
            }
        }

        public Song() : this("untitled", 96, 120)
        {

        }

        public string Title
        {
            get => m_title;
            set { m_title = value; }
        }

        public string Copyright
        {
            get => m_copyright;
            set { m_copyright = value; }
        }

        public int PPQN
        {
            get => m_ppqn;
            set { m_ppqn = value; }
        }

        public int Tempo
        {
            get => m_tempo;
            set { m_tempo = value; }
        }

        public int Scale
        {
            get => m_scale;
            set { m_scale = value; }
        }

        [JsonIgnore]
        public Mode Mode
        {
            get => m_mode!;
            set { m_mode = value; }
        }

        [JsonPropertyName("Mode")]
        public string ModeRef
        {
            get
            {
                if (m_mode == null)
                    return "none";

                return m_mode.Name;
            }

            set
            {
                m_mode = ModeManager.Instance.FindMode(value);
            }
        }

        //public List<ChordProgression> Chords => m_chords;
        public int[] Chords => m_chords;

        public List<Track> Tracks => m_tracks;

        public static void Create(string title)
        {
            var song = new Song(title, 96, 120);
            m_instance = song;
        }

        public void SetChord(int bar, int chord)
        {
            m_chords[bar] = chord;
        }

        //public ChordProgression AddChord(int chord, int start, int duration)
        //{
        //    var c = new ChordProgression(chord, start, duration);
        //    m_chords.Add(c);
        //    return c;
        //}

        //public void RemoveChord(ChordProgression chord)
        //{
        //    m_chords.Remove(chord);
        //}

        public Track AddTrack(string name, string device, int channel)
        {
            var track = new Track(name, device, channel, -1);
            m_tracks.Add(track);
            return track;
        }

        public void RemoveTrack(string name)
        {
            var track = FindTrack(name);
            if (track != null)
                m_tracks.Remove(track);
        }

        public Track? FindTrack(string name)
        {
            foreach (var track in m_tracks)
            {
                if (track.Name == name)
                {
                    return track;
                }
            }

            return null;
        }

        public void Load(string filename)
        {
            var jsonText = File.ReadAllText(filename);
            var song = JsonSerializer.Deserialize<Song>(jsonText);
            m_instance = song;
        }

        public void Save(string filename)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            var jsonText = JsonSerializer.Serialize<Song>(this, options);
            File.WriteAllText(filename, jsonText);
        }

        public void Generate(string filename)
        {
            Logger.Debug($"Generate {filename}");
            var events = new List<MidiNoteEvent>();

            foreach(var track in m_tracks)
            {
                // TODO: pass chords here
                track.Generate(m_scale, m_mode!, m_chords, events);
            }

            var exporter = new MidiExporter();
            exporter.ExportToMidi(filename, events.ToArray(), m_title, m_ppqn, m_tempo);
        }

        public override string ToString()
        {
            return $"Song '{m_title}' copyright '{m_copyright}' tempo {m_tempo} ppqn {m_ppqn}";
        }
    }
}
