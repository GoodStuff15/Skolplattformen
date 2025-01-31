using System;
using System.Collections.Generic;

namespace Skolplattformen.Models;

public partial class Class
{
    public int Id { get; set; }

    public string? ClassName { get; set; }

    public int? StaffId { get; set; }

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();

    public virtual Staff? Staff { get; set; }

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
