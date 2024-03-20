using MultaqaTech.Core.Entities.SettingsEntities;

namespace MultaqaTech.Service;

public class SubjectService(IUnitOfWork unitOfWork) : ISubjectService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Subject?> CreateSubjectAsync(Subject subject)
    {
        try
        {
            await _unitOfWork.Repository<Subject>().AddAsync(subject);
            var result = await _unitOfWork.CompleteAsync();
            if (result <= 0) return null;

            return subject;
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return null;
        }
    }

    public async Task<Subject?> ReadByIdAsync(int subjectId)
    {
        try
        {
            var subject = await _unitOfWork.Repository<Subject>().GetByIdAsync(subjectId);

            return subject;
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return null;
        }

    }

    public async Task<IReadOnlyList<Subject>> ReadAllAsync()
    {
        try
        {
            var subjects = await _unitOfWork.Repository<Subject>().GetAllAsync();

            return subjects;
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return null;
        }
    }

    public async Task<IReadOnlyList<Subject>> ReadSubjectsByIds(List<int> subjectsIds)
    {
        try
        {
            var subjects = await _unitOfWork.Repository<Subject>().GetAllByIdsAsync(subjectsIds);

            return subjects;
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return null;
        }
    }

    public async Task<Subject?> UpdateSubject(int subjectId, Subject updatedSubject)
    {
        var subject = await _unitOfWork.Repository<Subject>().GetByIdAsync(subjectId);

        if (subject == null) return null;

        if (updatedSubject == null || string.IsNullOrWhiteSpace(updatedSubject.Name))
            return null;

        subject.Name = updatedSubject.Name;

        try
        {
            _unitOfWork.Repository<Subject>().Update(subject);
            var result = await _unitOfWork.CompleteAsync();
            if (result <= 0) return null;

            return subject;
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return null;
        }
    }

    public async Task<bool> DeleteSubject(int subjectId)
    {
        var subject = await _unitOfWork.Repository<Subject>().GetByIdAsync(subjectId);

        if (subject == null)
            return false;

        try
        {
            _unitOfWork.Repository<Subject>().Delete(subject);

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
}