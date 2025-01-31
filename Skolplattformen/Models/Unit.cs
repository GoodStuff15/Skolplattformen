using System;
using System.Collections.Generic;

namespace Skolplattformen.Models;

public partial class Unit
{
    public int Id { get; set; }

    public string? UnitName { get; set; }

    public virtual ICollection<Staff> Staff { get; set; } = new List<Staff>();
}
