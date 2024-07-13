using MultaqaTech.Core.Entities.CourseDomainEntities;
using MultaqaTech.Core.Entities.CourseDomainEntities.CurriculumDomainEntities;
using System;

namespace MultaqaTech.Service.CourseDomainServices.CurriculumDomainServices;

public class CurriculumItemService(IUnitOfWork unitOfWork, MultaqaTechContext context) : ICurriculumItemService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly MultaqaTechContext _context = context;

    public async Task<CurriculumItem?> CreateCurriculumItemAsync(CurriculumItem curriculumItem)
    {
        try
        {
            switch (curriculumItem?.CurriculumItemType)
            {
                case CurriculumItemType.Lecture:
                    await _unitOfWork.Repository<Lecture>().AddAsync((Lecture)curriculumItem);
                    break;

                case CurriculumItemType.Quiz:
                    await _unitOfWork.Repository<Quiz>().AddAsync((Quiz)curriculumItem);
                    break;
            }

            
            var result = await _unitOfWork.CompleteAsync();
            if (result <= 0) return null;

            return curriculumItem;
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return null;
        }
    }

    public async Task<bool> DeleteCurriculumItem(CurriculumItem curriculumItem)
    {
            CurriculumItemSpeceficationsParams speceficationsParams = new CurriculumItemSpeceficationsParams
            {
                curriculumSectionId = curriculumItem.CurriculumSectionId
            };

            int changes = 0;

            List<CurriculumItem> remainingItems=new();
            List<Lecture> remainingLecs = null;
            List<Quiz> remainingQuizes = null;
        try
        {
            switch (curriculumItem.CurriculumItemType)
            {
                case CurriculumItemType.Lecture:
                    _unitOfWork.Repository<Lecture>().Delete((Lecture)curriculumItem);
                    break;

                case CurriculumItemType.Quiz:
                    _unitOfWork.Repository<Quiz>().Delete((Quiz)curriculumItem);
                    break;
            }
                    remainingQuizes = (List<Quiz>?)await ReadCurriculumItemsbyTypeAsync(speceficationsParams, CurriculumItemType.Quiz);
                    remainingLecs = (List<Lecture>?)await ReadCurriculumItemsbyTypeAsync(speceficationsParams, CurriculumItemType.Lecture);

            remainingItems.AddRange(remainingQuizes);
            remainingItems.AddRange(remainingLecs);


            foreach (var item in remainingItems.Where(s => s.Order > curriculumItem.Order))
            {
                item.Order--;
                changes++;
            }

            var result = await _unitOfWork.CompleteAsync();

            if (result <= changes)
                return false;

            return true;
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return false;
        }
    }

    public async Task<CurriculumItem?> ReadByIdAsync(int curriculumItemId, CurriculumItemType type)
    {
        try
        {

            switch (type)
            {
                case CurriculumItemType.Lecture:
                    Lecture? lecture = null;
                    var lecSpec = new LectureWithIncludesSpecifications(curriculumItemId);
                    lecture = await _unitOfWork.Repository<Lecture>().GetByIdWithSpecAsync(lecSpec);

                    return lecture;

                case CurriculumItemType.Quiz:
                    Quiz? quiz = null;
                    var quizSpec = new QuizWithIncludesSpecifications(curriculumItemId);
                    quiz = await _unitOfWork.Repository<Quiz>().GetByIdWithSpecAsync(quizSpec);
                    return quiz;
            }
            return null;
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return null;
        }

    }

    public async Task<IReadOnlyList<CurriculumItem>> ReadCurriculumItemsAsync(CurriculumItemSpeceficationsParams speceficationsParams)
    {
        try
        {
            var lecSpec = new LectureWithIncludesSpecifications(speceficationsParams);
            List<Lecture>? lectures = (List<Lecture>?)await _unitOfWork.Repository<Lecture>().GetAllWithSpecAsync(lecSpec);

            var quizSpec = new QuizWithIncludesSpecifications(speceficationsParams);
            List<Quiz>? quizes = (List<Quiz>?)await _unitOfWork.Repository<Quiz>().GetAllWithSpecAsync(quizSpec);

            List<CurriculumItem> items = new List<CurriculumItem>();
            items.AddRange(lectures);
            items.AddRange(quizes);

            return items;
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return null;
        }
    }

    public async Task<IReadOnlyList<CurriculumItem>> ReadCurriculumItemsbyTypeAsync(CurriculumItemSpeceficationsParams speceficationsParams, CurriculumItemType type)
    {
        try
        {

            switch (type)
            {
                case CurriculumItemType.Lecture:
                    List<Lecture>? lectures = null;
                    var lecSpec = new LectureWithIncludesSpecifications(speceficationsParams);
                    lectures = (List<Lecture>?)await _unitOfWork.Repository<Lecture>().GetAllWithSpecAsync(lecSpec);
                    return lectures;

                case CurriculumItemType.Quiz:
                    List<Quiz>? quizes = null;
                    var quizSpec = new QuizWithIncludesSpecifications(speceficationsParams);
                    quizes = (List<Quiz>?)await _unitOfWork.Repository<Quiz>().GetAllWithSpecAsync(quizSpec);
                    return quizes;
            }

            return null;
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return null;
        }
    }

    public async Task<bool> ReorderItems(int sectionId, List<int> newOrder)
    {
        try
        {
            var speceficationsParams = new CurriculumItemSpeceficationsParams
            {
                curriculumSectionId = sectionId
            };

            var lecSpec = new LectureWithIncludesSpecifications(speceficationsParams);
            List<Lecture>? lectures = (List<Lecture>?)await _unitOfWork.Repository<Lecture>().GetAllWithSpecAsync(lecSpec);

            var quizSpec = new QuizWithIncludesSpecifications(speceficationsParams);
            List<Quiz>? quizes = (List<Quiz>?)await _unitOfWork.Repository<Quiz>().GetAllWithSpecAsync(quizSpec);

            List<CurriculumItem> items = new List<CurriculumItem>();
            items.AddRange(lectures);
            items.AddRange(quizes);
            for (int i = 0; i < newOrder.Count; i++)
            {
                var itemOrder = newOrder[i];
                CurriculumItem? item = items.FirstOrDefault(s => s.Order == itemOrder&&!_context.Entry(s).Property(l => l.Order).IsModified);
                if (item != null)
                {
                    item.Order = i + 1; // Update order property

                    // Update order in respective table using context
                    if (item is Lecture)
                    {
                        _context.Lectures.Attach(item as Lecture); // Attach Lecture entity
                        _context.Entry(item).Property(l => l.Order).IsModified = true; // Mark Order property as modified
                    }
                    else if (item is Quiz)
                    {
                        _context.Quizes.Attach(item as Quiz); // Attach Quiz entity
                        _context.Entry(item).Property(q => q.Order).IsModified = true; // Mark Order property as modified
                    }
                }
            }

            var result = await _unitOfWork.CompleteAsync();
            if (result <= newOrder.Count) return false;

            return true;
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return false;
        }
    }


    public async Task<CurriculumItem?> UpdateCurriculumItem(CurriculumItem storedItem, CurriculumItem newItem)
    {
        if (storedItem == null || newItem == null)
            return null;

        storedItem.Title = newItem.Title;
        storedItem.Description= newItem.Description;

        try
        {
            switch (storedItem?.CurriculumItemType)
            {
                case CurriculumItemType.Lecture:
                    _unitOfWork.Repository<Lecture>().Update((Lecture)storedItem);
                    break;

                case CurriculumItemType.Quiz:
                    _unitOfWork.Repository<Quiz>().Update((Quiz)storedItem);
                    break;
            }

            var result = await _unitOfWork.CompleteAsync();
            if (result <= 0) return null;

            return storedItem;
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return null;
        }
    }

    public async Task<bool> UpdateCurriculumItemCompletionStateInStudentProgress(int CurriculumItemId, int studentCourseId, CurriculumItemType type)
    {
        try
        {
            StudentCourseProgress? progress = null;
            switch (type)
            {
                case CurriculumItemType.Lecture:
                    progress = await _unitOfWork.Repository<StudentCourseProgress>().FindAsync(S => S.LectureId == CurriculumItemId &&
                               S.StudentCourseId == studentCourseId);
                    break;

                case CurriculumItemType.Quiz:
                    progress = await _unitOfWork.Repository<StudentCourseProgress>().FindAsync(S => S.QuizId == CurriculumItemId &&
                                S.StudentCourseId == studentCourseId);
                    break;
            }

            if (progress == null)
                return false;

            progress.IsCompleted = true;
            await _unitOfWork.CompleteAsync();

            // Update student course completion percentage
            var studentCourse = await _context.StudentCourses
                .Include(sc => sc.Course)
                    .ThenInclude(c => c.CurriculumSections)
                    .ThenInclude(cs => cs.Lectures)
                .Include(sc => sc.Course)
                    .ThenInclude(c => c.CurriculumSections)
                    .ThenInclude(cs => cs.Quizes)
                .FirstOrDefaultAsync(sc => sc.Id == studentCourseId);

            if (studentCourse == null)
                return false;

            var completedItems = await _context.StudentsProgress
                .CountAsync(p => p.IsCompleted && p.StudentCourseId == studentCourseId);

            var totalItems = studentCourse.Course?.CurriculumSections?
                .SelectMany(section =>
                {
                    var lectures = section.Lectures?.Cast<CurriculumItem>() ?? Enumerable.Empty<CurriculumItem>();
                    var quizes = section.Quizes?.Cast<CurriculumItem>() ?? Enumerable.Empty<CurriculumItem>();
                    return lectures.Concat(quizes);
                })
                .Count();

            studentCourse.CompletionPercentage = (int)((completedItems * 100) / totalItems);

            await _unitOfWork.CompleteAsync();

            return true;
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return false;
        }
    }

}
