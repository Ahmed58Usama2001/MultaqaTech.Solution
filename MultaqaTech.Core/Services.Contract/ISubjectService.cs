namespace MultaqaTech.Core.Services.Contract;

public interface ISubjectService
{
    public Task<Subject?> Create(Subject subject);
}