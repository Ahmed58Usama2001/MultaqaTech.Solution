namespace MultaqaTech.Core.Services.Contract.CourseDomainContracts.CurriculumDomainContracts;

public interface INoteService
{
    Task<Note?> CreateNoteAsync(Note note);

    Task<IReadOnlyList<Note>> ReadNotesAsync(NoteSpeceficationsParams speceficationsParams);

    Task<Note?> ReadByIdAsync(int noteId);

    public Task<Note?> UpdateNote(Note storedNote, Note newNote);


    public Task<bool> DeleteNote(int noteId);

    //Task<IReadOnlyList<Note>> ReadNotesByIds(List<int> curriculumItemIds);
}
