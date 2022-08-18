using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using AudioScript.Data;
using AudioScript.Logging;
using Microsoft.Win32;
using MoonSharp.Interpreter;

namespace AudioScript
{
    public class MainWindowViewModel
    {
        private Script m_script;
        private Song? m_song;
        private ModeManager? m_modes;

        private static string[] m_scales = { "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B" };

        public MainWindowViewModel()
        {
            Logger.Info("Audio Script V2.0");

            m_script = new Script();

            m_modes = ModeManager.Instance;
            m_song = Song.Instance;

            UserData.RegisterType<Mode>();
            UserData.RegisterType<Song>();
            UserData.RegisterType<ChordProgression>();
            UserData.RegisterType<Track>();
            UserData.RegisterType<Pattern>();
            UserData.RegisterType<Reference>();
            UserData.RegisterType<Note>();

            m_script.Globals["CreateMode"] = (Func<string, List<int>, Mode>)CreateMode;
            m_script.Globals["CreateInstruments"] = (Action<List<string>>)CreateInstruments;
            m_script.Globals["CreateSong"] = (Func<string, int, Song>)CreateSong;
            m_script.Globals["SetTitle"] = (Action<string>)SetTitle;
            m_script.Globals["SetCopyright"] = (Action<string>)SetCopyright;
            m_script.Globals["SetChord"] = (Action<int, int>)SetChord;
            m_script.Globals["CreateTrack"] = (Func<string, int, Track>)CreateTrack;
            m_script.Globals["CreatePattern"] = (Func<Track, int, int, int, Pattern>)CreatePattern;
            m_script.Globals["SetPatternLength"] = (Action<Pattern, int>)SetPatternLength;
            m_script.Globals["CreateReference"] = (Func<Track, int, int, int, bool>)CreateReference;
            m_script.Globals["ClearReference"] = (Func<Track, int, bool>)ClearReference;
            m_script.Globals["ClearAllReferences"] = (Func<Track, bool>)ClearAllReferences;
            m_script.Globals["CreateNote"] = (Func<Pattern, int, int, int, int, Note>)CreateNote;
            m_script.Globals["CreateNotes"] = (Func<Pattern, List<int>, int, int, List<Note>>)CreateNotes;
            m_script.Globals["ClearAllNotes"] = (Func<Pattern, bool>)ClearAllNotes;

            m_script.Globals["FindScale"] = (Func<string, int>)FindScale;
            m_script.Globals["FindMode"] = (Func<string, Mode>)FindMode;
            m_script.Globals["FindTrack"] = (Func<string, Track>)FindTrack;
            m_script.Globals["FindPattern"] = (Func<Track, int, int, Pattern>)FindPattern;

            m_script.Globals["GetSong"] = (Func<Song>)GetSong;

            m_script.Globals["ShowMessage"] = (Func<string, int, int>)ShowMessage;

            Startup();
        }

        private void Startup()
        {
            try
            {
                m_script.DoFile("Scripts/startup.lua");
            }
            catch (Exception e)
            {
                Logger.Error($"Error loading script: {e.Message}");
            }
        }

        private static void CreateInstruments(List<string> names)
        {
            Logger.Debug("Create instruments");
        }

        private static Mode CreateMode(string name, List<int> notes)
        {
            Logger.Debug($"Create mode {name}");
            var mode = ModeManager.Instance.AddMode(name, notes.ToArray());
            return mode;
        }

        private static Song CreateSong(string title, int ppqn = 96)
        {
            Logger.Debug($"Create song {title} ppqn {ppqn}");
            var song = Song.Instance;
            song.Title = title;
            song.PPQN = ppqn;
            return song;
        }

        private static Song GetSong()
        {
            return Song.Instance;
        }

        private static void SetTitle(string title)
        {
            Song.Instance.Title = title;
        }

        private static void SetCopyright(string copyright)
        {
            Song.Instance.Copyright = copyright;
        }

        //private static ChordProgression CreateChordProgression(int chord, int start, int duration)
        //{
        //    Logger.Debug("Create Chord Progression");

        //    var c = Song.Instance.AddChord(chord, start, duration);
        //    return c;
        //}

        private void SetChord(int bar, int chord)
        {
            Song.Instance.SetChord(bar, chord);
        }

        private static Track CreateTrack(string name, int channel)
        {
            Logger.Debug($"Create track {name}");
            var track = Song.Instance.AddTrack(name, "default", channel);
            return track;
        }

        private static Track FindTrack(string name)
        {
            var track = Song.Instance.FindTrack(name);
            return track!;
        }

        private static Mode FindMode(string name)
        {
            var mode = ModeManager.Instance.FindMode(name);
            return mode!;
        }

        private static int FindScale(string name)
        {
            int index = 0;

            foreach (var scale in m_scales)
            {
                if (name == scale)
                    return index;

                index++;
            }

            return -1;
        }

        private static Pattern CreatePattern(Track track, int letter, int digit, int length = 1)
        {
            Logger.Debug("Create pattern");

            var pattern = new Pattern(letter, digit, length);
            track.AddPattern(pattern);
            return pattern;
        }

        private static Pattern FindPattern(Track track, int letter, int digit)
        {
            var pattern = track.FindPattern(letter, digit);
            return pattern!;
        }

        private static void SetPatternLength(Pattern pattern, int length)
        {
            pattern.Length = length;
        }

        private static bool CreateReference(Track track, int bar, int letter, int digit)
        {
            Logger.Debug("Create reference");

            track.AddReference(bar, letter, digit);
            return true;
        }

        private static bool ClearReference(Track track, int bar)
        {
            Logger.Debug("Clear reference");
            track.RemoveReference(bar);
            return true;
        }

        private static bool ClearAllReferences(Track track)
        {
            Logger.Debug("Clear All References");
            track.RemoveAllReferences();
            return true;
        }

        private static Note CreateNote(Pattern pattern, int note, int start, int duration = 24, int velocity = 96)
        {
            var n = new Note(note, start, duration, velocity);
            pattern.AddNote(n);
            return n;
        }

        private static List<Note> CreateNotes(Pattern pattern, List<int> ns, int baseNote, int duration)
        {
            Logger.Debug("Create notes");

            var n16th = Song.Instance.PPQN / 4;

            var notes = new List<Note>();

            foreach (var n in ns)
            {
                var note = new Note(baseNote, n * n16th, duration, 96);
                pattern.AddNote(note);
                notes.Add(note);
            }

            return notes;
        }

        private static bool ClearAllNotes(Pattern pattern)
        {
            pattern.RemoveAllNotes();
            return true;
        }

        private static int ShowMessage(string message, int type)
        {
            Logger.Debug($"ShowMessage {message} {type}");
            switch (type)
            {
                case 0:
                    MessageBox.Show(message, "Audio Script", MessageBoxButton.OK, MessageBoxImage.Information);
                    break;

                case 1:
                    if (MessageBox.Show(message, "Audio Script", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK)
                        return 1;
                    break;

                case 2:
                    if (MessageBox.Show(message, "Audio Script", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        return 1;
                    break;
            }

            return 0;
        }

        public ICommand NewCommand
        {
            get
            {
                return new DelegateCommand((o) =>
                {
                    Song.Create("Untitled");
                });
            }
        }

        public ICommand OpenCommand
        {
            get
            {
                return new DelegateCommand((o) =>
                {
                    var dialog = new OpenFileDialog
                    {
                        FileName = "AudioScript",
                        DefaultExt = ".asc",
                        Filter = "Audio Script projects (*.asc)|*.asc"
                    };

                    if (dialog.ShowDialog() == true)
                    {
                        Song.Instance.Load(dialog.FileName);
                    }
                });
            }
        }

        public ICommand SaveCommand
        {
            get
            {
                return new DelegateCommand((o) =>
                {
                    var dialog = new SaveFileDialog
                    {
                        FileName = "AudioScript.asc",
                        DefaultExt = ".asc",
                        Filter = "Audio Script projects (*.asc)|*.asc"
                    };

                    if (dialog.ShowDialog() == true)
                    {
                        Song.Instance.Save(dialog.FileName);
                    }
                });
            }
        }

        public ICommand ExportCommand
        {
            get
            {
                return new DelegateCommand((o) =>
                {
                    var dialog = new SaveFileDialog
                    {
                        FileName = "Export.mid",
                        DefaultExt = ".mid",
                        Filter = "MIDI files (*.mid)|*.mid"
                    };

                    if (dialog.ShowDialog() == true)
                    {
                        Song.Instance.Generate(dialog.FileName);
                    }
                });
            }
        }

        public ICommand ExitCommand
        {
            get
            {
                return new DelegateCommand((o) =>
                {
                    Application.Current.Shutdown();
                });
            }
        }

        public ICommand RunScriptCommand
        {
            get
            {
                return new DelegateCommand((o) =>
                {
                    var dialog = new OpenFileDialog
                    {
                        FileName = "Script.lua",
                        DefaultExt = ".lua",
                        InitialDirectory = Helpers.Folders.GetScriptFolder(),
                        Filter = "Scripts (*.lua)|*.lua"
                    };

                    if (dialog.ShowDialog() == true)
                    {
                        m_script.DoFile(dialog.FileName);
                    }
                });
            }
        }


    }
}
