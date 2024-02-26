namespace MultaqaTech.Core.Services.Contract;

public interface ISubjectService
{
    public Task<Subject?> Create(Subject subject);
    public Task<IEnumerable<Subject>> ReadAllAsync();
    public Task<Subject?> ReadById(int id);
}