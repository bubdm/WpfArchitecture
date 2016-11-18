using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timesheet.Infrastructure.Models
{
    public class TimesheetDailySummary
    {
        public string Email { get; set; }
        public DateTime DateTime { get; set; }
        public decimal Hours { get; set; }
        public decimal BillableHours { get; set; }
        public bool IsConfirmed { get; set; }
        public bool IsException { get; set; }
        public int EntiresCount { get; set; }
        public int TotalLatency { get; set; }
        public decimal AverageLatency { get; set; }
    }
}
