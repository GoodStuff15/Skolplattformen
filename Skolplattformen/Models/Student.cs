using System;
using System.Collections.Generic;

namespace Skolplattformen.Models;

public partial class Student
{
    public int Id { get; set; }

    public string? StudentName { get; set; }

    public int? ClassId { get; set; }

    public string? StudentLname { get; set; }

    public string? Gender { get; set; }

    public DateOnly? PersonNumber { get; set; }

    public virtual Class? Class { get; set; }

    public virtual ICollection<StudentGrade> StudentGrades { get; set; } = new List<StudentGrade>();
}
