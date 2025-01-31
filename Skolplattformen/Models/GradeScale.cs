using System;
using System.Collections.Generic;

namespace Skolplattformen.Models;

public partial class GradeScale
{
    public int Id { get; set; }

    public string? GradeName { get; set; }

    public int? GradeValue { get; set; }

    public virtual ICollection<StudentGrade> StudentGrades { get; set; } = new List<StudentGrade>();
}
