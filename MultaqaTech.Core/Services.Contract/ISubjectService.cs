
namespace MultaqaTech.Core.Services.Contract;

public interface ISubjectService
{
    Task<Subject?> CreateSubjectAsync(Subject subject);

    Task<IReadOnlyList<Subject>> ReadAllAsync();

    Task<Subject?> ReadByIdAsync(int subjectId);

    Task<Subject?> UpdateSubject(int subjectId, Subject subject);

    Task<bool> DeleteSubject(int subjectId);

    Task<IReadOnlyList<Subject>> ReadSubjectsByIds(List<int> subjectsIds);
    
}