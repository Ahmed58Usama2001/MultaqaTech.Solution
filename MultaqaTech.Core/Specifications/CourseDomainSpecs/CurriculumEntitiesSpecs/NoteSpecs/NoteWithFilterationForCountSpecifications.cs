namespace MultaqaTech.Core.Specifications.CourseDomainSpecs.CurriculumEntitiesSpecs.CurriculumSectionSpecs;

public class NoteWithFilterationForCountSpecifications : BaseSpecifications<Note>
{
    public NoteWithFilterationForCountSpecifications(NoteSpeceficationsParams speceficationsParams) :
         base(e =>
           (
                 (string.IsNullOrEmpty(speceficationsParams.writerStudentId) || e.WriterStudentId == speceficationsParams.writerStudentId) &&                
                 (speceficationsParams.lectureId == null || e.LectureId == speceficationsParams.lectureId)                 
           ))
    {

    }
}

