using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrudMVCADO.Models
{
    public class Department
    {
        public int deptId { get; set; }

        public string deptName { get; set; }

        public int empId
        {
            get; set;
        }
        public List<Department> dep { get; set; }
    }
}