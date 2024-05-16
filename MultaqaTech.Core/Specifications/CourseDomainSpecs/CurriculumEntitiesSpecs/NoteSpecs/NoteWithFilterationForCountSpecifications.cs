namespace MultaqaTech.Core.Specifications.CourseDomainSpecs.CurriculumEntitiesSpecs.CurriculumSectionSpecs;

public class NoteWithFilterationForCountSpecifications : BaseSpecifications<Note>
{
    public NoteWithFilterationForCountSpecifications(NoteSpeceficationsParams speceficationsParams) :
         base(e =>
           (
                 (speceficationsParams.writerStudentId == null || e.WriterStudentId == speceficationsParams.writerStudentId) &&                
                 (speceficationsParams.lectureId == null || e.LectureId == speceficationsParams.lectureId)
         &&
            (string.IsNullOrEmpty(speceficationsParams.Search)
              || e.Content.ToLower().Contains(speceficationsParams.Search)
            
           )))
    {

    }
}

