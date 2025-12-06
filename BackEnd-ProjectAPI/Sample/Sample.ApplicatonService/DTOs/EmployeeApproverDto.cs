using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.ApplicationService.DTOs
{
    public partial class EmployeeApproverDto
    {
        public int id { get; set; }
        public string EmpIdno { get; set; } = null!;

        public EmployeeDto EmpIdnoNavigation { get; set; } = null!;
    }
}
