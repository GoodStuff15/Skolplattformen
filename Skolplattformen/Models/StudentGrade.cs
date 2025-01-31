using System;
using System.Collections.Generic;

namespace Skolplattformen.Models;

public partial class StudentGrade
{
    public int Id { get; set; }

    public DateOnly? SetDate { get; set; }

    public int? StudentId { get; set; }

    public int? StaffId { get; set; }

    public int? GradeScaleId { get; set; }

    public int? CourseId { get; set; }

    public virtual Course? Course { get; set; }

    public virtual GradeScale? GradeScale { get; set; }

    public virtual Staff? Staff { get; set; }

    public virtual Student? Student { get; set; }
}
