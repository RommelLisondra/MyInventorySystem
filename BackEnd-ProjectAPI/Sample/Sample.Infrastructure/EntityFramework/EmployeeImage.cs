using System;
using System.Collections.Generic;

namespace Sample.Infrastructure.EntityFramework
{
    public partial class EmployeeImage
    {
        public int Id { get; set; }
        public string EmpIdno { get; set; } = null!;
        public string? ImagePath { get; set; }

        public virtual Employee EmpIdnoNavigation { get; set; } = null!;
    }
}
