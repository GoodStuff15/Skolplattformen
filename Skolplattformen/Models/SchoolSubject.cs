using System;
using System.Collections.Generic;

namespace Skolplattformen.Models;

public partial class SchoolSubject
{
    public int Id { get; set; }

    public string? SubjectName { get; set; }

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();

    public virtual ICollection<Staff> Teachers { get; set; } = new List<Staff>();
}
