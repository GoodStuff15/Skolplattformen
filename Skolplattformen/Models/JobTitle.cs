using System;
using System.Collections.Generic;

namespace Skolplattformen.Models;

public partial class JobTitle
{
    public int Id { get; set; }

    public string? TitleName { get; set; }

    public string? JobRole { get; set; }

    public virtual ICollection<Staff> Staff { get; set; } = new List<Staff>();
}
