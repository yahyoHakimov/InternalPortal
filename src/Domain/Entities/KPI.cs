using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class KPI
    {
        [Key]
        public int KpiId { get; set; }
        public string MetricName { get; set; }
        public decimal TargetValue { get; set; }
        public decimal ActualValue { get; set; }
        public DateTime ReviewDate { get; set; }

        // Foreign Key
        public int EmployeeId { get; set; }
        public EmployeeModel Employee { get; set; }
    }
}
