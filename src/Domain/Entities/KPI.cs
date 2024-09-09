using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class KPI
    {
        public int KpiId { get; set; }
        public int EmployeeId { get; set; }
        public string MetricName { get; set; }
        public decimal TargetValue { get; set; }
        public decimal ActualValue { get; set; }
        public DateTime ReviewDate { get; set; }
        public Employee Employee { get; set; }
    }

}
