function CreateRandom()
	SetMode("dorian")
	SetScale("D")

	track = FindTrack("lead")
	if track == nil then
		ShowMessage('Cant find track', 0)
		return
	end
	pattern = FindPattern(track, 0, 0)
	if pattern == nil then
		ShowMessage('Cant find paatern', 0)
		return
	end
	ClearAllNotes(pattern)
	notes = CreateNotes(pattern, { 0, 3, 6, 8, 11, 14 }, 35, 24)

	ShowMessage("Number of notes "..#notes, 0)

	index = 0
	for i, note in ipairs(notes) do
		note.NoteIndex = 42 + index % 6
		if index % 3 != 0 then
			note.Velocity = 75
		end
		index = index + 1
		if index > 5 then
			index = 0
		end
	end

	print('a simple message')
end

CreateRandom()
