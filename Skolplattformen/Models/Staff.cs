using System;
using System.Collections.Generic;

namespace Skolplattformen.Models;

public partial class Staff
{
    public int Id { get; set; }

    public string? StaffName { get; set; }

    public bool? IsMentor { get; set; }

    public int? JobTitleId { get; set; }

    public DateOnly? DateOfHire { get; set; }

    public decimal? Salary { get; set; }

    public int? UnitId { get; set; }

    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();

    public virtual JobTitle? JobTitle { get; set; }

    public virtual ICollection<StudentGrade> StudentGrades { get; set; } = new List<StudentGrade>();

    public virtual Unit? Unit { get; set; }

    public virtual ICollection<SchoolSubject> Subjects { get; set; } = new List<SchoolSubject>();
}
