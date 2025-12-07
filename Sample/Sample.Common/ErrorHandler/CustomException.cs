using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Common.ErrorHandler
{
    public class CustomException
    {
        public bool ExceptionHandled { get; set; }
        public string? Message { get; set; }
        public bool Default { get; set; }
    }
}
