
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrudMVCADO.Models
{
    public class Employee
    {
        
        public int empId { get; set; }
        public string empName { get; set; }

        public int deptId { get; set; }
    }
}