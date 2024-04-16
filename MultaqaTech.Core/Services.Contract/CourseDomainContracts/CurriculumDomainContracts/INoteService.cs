namespace MultaqaTech.Core.Services.Contract.CourseDomainContracts.CurriculumDomainContracts;

public interface INoteService
{
    Task<Note?> CreateNoteAsync(Note note);

    Task<IReadOnlyList<Note>> ReadNotesAsync(NoteSpeceficationsParams speceficationsParams);

    Task<Note?> ReadByIdAsync(int noteId);

    Task<Note?> UpdateNote(int noteId, Note updatedNote);

    Task<bool> DeleteNote(int noteId);

    //Task<IReadOnlyList<CurriculumItem>> ReadCurriculumItemsByIds(List<int> curriculumItemIds);
}
