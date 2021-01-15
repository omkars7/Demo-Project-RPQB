using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrudMVCADO.Models
{
    [Table("Department")]
    public class Department
    {
        public int deptId { get; set; }
        public string deptName { get; set; }

        public List<Employee> Employees { get; set; }
    }
}
