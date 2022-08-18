function InitSong()
    song = CreateSong("Song")

	song.Mode = FindMode("major")

	for i=0,31 do
		SetChord(i * 4, 0)
		SetChord(i * 4 + 1, 3)
		SetChord(i * 4 + 2, 4)
		SetChord(i * 4 + 3, 0)
	end
    
	track = CreateTrack("lead", 0)
	pattern = CreatePattern(track, 0, 0)
	for i=0,3 do
		CreateNote(pattern, 21, i * 96, 24, 96)
	end

	for bar=0,15 do
		CreateReference(track, bar, 0, 0)
	end

    track = CreateTrack("pad", 3)
	pattern = CreatePattern(track, 0, 0)
	CreateNote(pattern, 21, 0, 4 * 96, 96)
	for bar=0,15 do
		CreateReference(track, bar, 0, 0)
	end

    track = CreateTrack("bass", 6)
	pattern = CreatePattern(track, 0, 0)
	for i=0,3 do
		CreateNote(pattern, 21, i * 96 + 48, 48, 96)
	end
	for bar=0,15 do
		CreateReference(track, bar, 0, 0)
	end

    track = CreateTrack("drums", 9)
	CreatePattern(track, 0, 0)
	for bar=0,15 do
		CreateReference(track, bar, 0, 0)
	end
end

function CreateModes()
    CreateMode('major',			{ 0, 2, 4, 5, 7, 9, 11 })
    CreateMode('aeolian',		{ 0, 2, 3, 5, 7, 8, 10 })
	CreateMode('natural minor', { 0, 2, 3, 5, 7, 8, 10 })

    CreateMode('harmonic minor', {0, 2, 3, 5, 7, 8, 11})
    CreateMode('melodic minor', {0, 2, 3, 5, 7, 9, 11})
    CreateMode('dorian',        {0, 2, 3, 5, 7, 9, 10})
    CreateMode('phrygian',      {0, 1, 3, 5, 7, 8, 10})
    CreateMode('lydian',        {0, 2, 4, 6, 7, 9, 11})
    CreateMode('mixolydian',    {0, 2, 4, 5, 7, 9, 10})
    CreateMode('locrian',       {0, 1, 3, 5, 6, 8, 10})

    CreateMode('major blues',   {0, 2, 3, 4, 5, 7})
    CreateMode('minor blues',   {0, 3, 5, 6, 7, 10})
    CreateMode('diminished',    {0, 2, 3, 6, 8, 9, 11})
    CreateMode('combination diminished', {0, 1, 3, 4, 6, 7, 9, 10 })
    CreateMode('major pentatonic', {0, 2, 4, 7, 9})
    CreateMode('minor pentatonic', {0, 3, 5, 7, 10})
    CreateMode('raga bhairav',  {0, 1, 4, 5, 7, 8, 11})
    CreateMode('raga gamanasrama', {0, 1, 4, 6, 7, 8, 11})
    CreateMode('raga todi',     {0, 1, 3, 6, 7, 8, 11})
    CreateMode('arabian',       {0, 2, 4, 5, 6, 8, 10})
    CreateMode('spanish',       {0, 1, 3, 4, 6, 7, 8, 11})
    CreateMode('gypsy',         {0, 2, 3, 6, 7, 8, 11})
    CreateMode('egyptian',      {0, 2, 5, 7, 10})
    CreateMode('Hawaiian',      {0, 2, 3, 7, 9})
    CreateMode('bali island pelog', {0, 1, 3, 7, 8})
    CreateMode('japanese miyakobushi', {0, 1, 5, 7, 8})
    CreateMode('ryukyu',        {0, 4, 5, 7, 11})
    CreateMode('chinese',       {0, 4, 6, 7, 11})
    CreateMode('bass line',     {0, 7, 10})
    CreateMode('whole tone',    {0, 2, 4, 6, 8, 10})
    CreateMode('minor 3rd interval', {0, 3, 6, 9})
    CreateMode('major 3rd interval', {0, 4, 8})
    CreateMode('4th interval',  {0, 5, 10})
    CreateMode('5th interval',  {0, 7})
    CreateMode('octave',        {0})
end

instruments = {
	'Acoustic Grand Piano',
	'Bright Acoustic Piano',
	'Electric Grand Piano',
	'Honky-tonk Piano',
	'Electric Piano 1',
	'Electric Piano 2',
	'Harpsichord',
	'Clavinet',
	'Celesta',
	'Glockenspiel',
	'Music Box',
	'Vibraphone',
	'Marimba',
	'Xylophone',
	'Tubular Bells',
	'Dulcimer',
	'Drawbar Organ',
	'Percussive Organ',
	'Rock Organ',
	'Church Organ',
	'Reed Organ',
	'Accordion',
	'Harmonica',
	'Tango Accordion',
	'Acoustic Guitar (nylon)',
	'Acoustic Guitar (steel)',
	'Electric Guitar (jazz)',
	'Electric Guitar (clean)',
	'Electric Guitar (muted)',
	'Overdriven Guitar',
	'Distortion Guitar',
	'Guitar harmonics',
	'Acoustic Bass',
	'Electric Bass (finger)',
	'Electric Bass (pick)',
	'Fretless Bass',
	'Slap Bass 1',
	'Slap Bass 2',
	'Synth Bass 1',
	'Synth Bass 2',
	'Violin',
	'Viola',
	'Cello',
	'Contrabass',
	'Tremolo Strings',
	'Pizzicato Strings',
	'Orchestral Harp',
	'Timpani',
	'String Ensemble 1',
	'String Ensemble 2',
	'Synth Strings 1',
	'Synth Strings 2',
	'Choir Aahs',
	'Voice Oohs',
	'Synth Voice',
	'Orchestra Hit',
	'Trumpet',
	'Trombone',
	'Tuba',
	'Muted Trumpet',
	'French Horn',
	'Brass Section',
	'Synth Brass 1',
	'Synth Brass 2',
	'Soprano Sax',
	'Alto Sax',
	'Tenor Sax',
	'Baritone Sax',
	'Oboe',
	'English Horn',
	'Bassoon',
	'Clarinet',
	'Piccolo',
	'Flute',
	'Recorder',
	'Pan Flute',
	'Blown Bottle',
	'Shakuhachi',
	'Whistle',
	'Ocarina',
	'Lead 1 (square)',
	'Lead 2 (sawtooth)',
	'Lead 3 (calliope)',
	'Lead 4 (chiff)',
	'Lead 5 (charang)',
	'Lead 6 (voice)',
	'Lead 7 (fifths)',
	'Lead 8 (bass + lead)',
	'Pad 1 (new age)',
	'Pad 2 (warm)',
	'Pad 3 (polysynth)',
	'Pad 4 (choir)',
	'Pad 5 (bowed)',
	'Pad 6 (metallic)',
	'Pad 7 (halo)',
	'Pad 8 (sweep)',
	'FX 1 (rain)',
	'FX 2 (soundtrack)',
	'FX 3 (crystal)',
	'FX 4 (atmosphere)',
	'FX 5 (brightness)',
	'FX 6 (goblins)',
	'FX 7 (echoes)',
	'FX 8 (sci-fi)',
	'Sitar',
	'Banjo',
	'Shamisen',
	'Koto',
	'Kalimba',
	'Bag pipe',
	'Fiddle',
	'Shanai',
	'Tinkle Bell',
	'Agogo',
	'Steel Drums',
	'Woodblock',
	'Taiko Drum',
	'Melodic Tom',
	'Synth Drum',
	'Reverse Cymbal',
	'Guitar Fret Noise',
	'Breath Noise',
	'Seashore',
	'Bird Tweet',
	'Telephone Ring',
	'Helicopter',
	'Applause',
	'Gunshot' }

CreateInstruments(instruments)
CreateModes()
InitSong()
