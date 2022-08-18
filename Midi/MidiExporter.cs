using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Midi;

namespace AudioScript.Midi
{
    public class MidiExporter
    {
        public MidiExporter()
        {

        }

        public void ExportToMidi(string filename, MidiNoteEvent[] events, string title, int ppqn, int tempo)
        {
            var collection = new MidiEventCollection(0, ppqn);

            long tick = 0;
            int trackNumber = 0;

            collection.AddEvent(new TextEvent(title, MetaEventType.TextEvent, tick++), trackNumber);
            collection.AddEvent(new TempoEvent(CalculateMicrosecondsPerQuaterNote(tempo), tick), trackNumber);

            foreach(var midiNoteEvent in events)
            {
                collection.AddEvent(new NoteOnEvent(midiNoteEvent.Start, midiNoteEvent.Channel + 1, midiNoteEvent.Note, 
                    midiNoteEvent.Velocity, midiNoteEvent.Duration), trackNumber);
                collection.AddEvent(new NoteEvent(midiNoteEvent.Start + midiNoteEvent.Duration, midiNoteEvent.Channel + 1,
                    MidiCommandCode.NoteOff, midiNoteEvent.Note, 0), trackNumber);
            }

            collection.PrepareForExport();
            MidiFile.Export(filename, collection);
        }

        private static int CalculateMicrosecondsPerQuaterNote(int bpm)
        {
            return 60 * 1000 * 1000 / bpm;
        }
    }
}
