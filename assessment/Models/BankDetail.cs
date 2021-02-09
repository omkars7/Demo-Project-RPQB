using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrudMVCADO.Models
{
    public class BankDetail
    {

        public int bankId { get; set; }
        public string bankName { get; set; }
        public int accNo { get; set; }

        public int basicSal { get; set; }
        public int hRA { get; set; }
        public int otherAllowances { get; set; }

        public int grossSal { get; set; }

        public int pF { get; set; }
        public int medicalPremium { get; set; }
        public int tDS { get; set; }

        public int netSal{ get; set; }

        public int empId { get; set; }

    }
}