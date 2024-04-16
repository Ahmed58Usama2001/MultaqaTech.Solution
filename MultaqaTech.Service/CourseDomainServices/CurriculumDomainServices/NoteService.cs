namespace MultaqaTech.Service.CourseDomainServices.CurriculumDomainServices;

public class NoteSectionService(IUnitOfWork unitOfWork) : INoteService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Note?> CreateNoteAsync(Note note)
    {
        try
        {
            await _unitOfWork.Repository<Note>().AddAsync(note);
            var result = await _unitOfWork.CompleteAsync();
            if (result <= 0) return null;

            return note;
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return null;
        }
    }

    public async Task<bool> DeleteNote(int noteId)
    {
        var note = await _unitOfWork.Repository<Note>().GetByIdAsync(noteId);

        if (note == null)
            return false;

        try
        {
            _unitOfWork.Repository<Note>().Delete(note);

            var result = await _unitOfWork.CompleteAsync();

            if (result <= 0)
                return false;

            return true;
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return false;
        }
    }

    public async Task<Note?> ReadByIdAsync(int noteId)
    {
        var spec = new NoteWithIncludesSpecifications(noteId);

        var note = await _unitOfWork.Repository<Note>().GetByIdWithSpecAsync(spec);

        return note;
    }

    public async Task<IReadOnlyList<Note>> ReadNotesAsync(NoteSpeceficationsParams speceficationsParams)
    {
        var spec = new NoteWithIncludesSpecifications(speceficationsParams);

        var notes = await _unitOfWork.Repository<Note>().GetAllWithSpecAsync(spec);

        return notes;
    }

    public async Task<Note?> UpdateNote(int noteId, Note updatedNote)
    {
        var note = await _unitOfWork.Repository<Note>().GetByIdAsync(noteId);

        if (note == null || updatedNote == null || string.IsNullOrWhiteSpace(updatedNote.Content))
            return null;

        note = updatedNote;

        try
        {
            _unitOfWork.Repository<Note>().Update(note);
            var result = await _unitOfWork.CompleteAsync();
            if (result <= 0) return null;

            return note;
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return null;
        }
    }
}
