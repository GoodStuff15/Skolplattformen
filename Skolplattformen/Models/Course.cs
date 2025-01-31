using System;
using System.Collections.Generic;

namespace Skolplattformen.Models;

public partial class Course
{
    public int Id { get; set; }

    public string? CourseName { get; set; }

    public int? CourseHours { get; set; }

    public int? CurrentClassId { get; set; }

    public int? TeacherId { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public int? SubjectId { get; set; }

    public virtual Class? CurrentClass { get; set; }

    public virtual ICollection<StudentGrade> StudentGrades { get; set; } = new List<StudentGrade>();

    public virtual SchoolSubject? Subject { get; set; }

    public virtual Staff? Teacher { get; set; }
}
