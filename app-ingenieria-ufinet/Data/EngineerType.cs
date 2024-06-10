using System;
using System.Collections.Generic;

namespace app_ingenieria_ufinet.Data;

public partial class EngineerType
{
    public int EngineerTypeId { get; set; }

    public string TypeName { get; set; } = null!;

    public virtual ICollection<Engineer> Engineers { get; set; } = new List<Engineer>();
}
