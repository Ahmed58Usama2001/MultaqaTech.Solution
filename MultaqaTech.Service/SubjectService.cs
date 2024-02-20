using MultaqaTech.Core.Entities;
using MultaqaTech.Core.Repositories.Contract;
using Serilog;

namespace MultaqaTech.Service;

public class SubjectService(IUnitOfWork unitOfWork) : ISubjectService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Subject?> Create(Subject subject)
    {
        IGenericRepository<Subject>? repo = _unitOfWork.Repository<Subject>();

        try
        {
            await repo.AddAsync(subject);
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return null;
        }

        return subject;
    }
}