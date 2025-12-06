using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.WebAPI.Model
{
    public class EmployeeImageModel
    {
        public int id { get; set; }
        public string EmpIdno { get; set; } = null!;
        public string? FilePath { get; set; }

        public EmployeeModel EmpIdnoNavigation { get; set; } = null!;
    }
}
