using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.WebAPI.Model
{
    public class EmployeeSalesRefModel
    {
        public int id { get; set; }
        public string EmpIdno { get; set; } = null!;

        public EmployeeModel EmpIdnoNavigation { get; set; } = null!;
    }
}
