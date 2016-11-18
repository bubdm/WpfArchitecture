using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timesheet.Infrastructure.Models
{
    public class TimesheetData
    {
        public int PmpId { get; set; }

        public string Email { get; set; }

        public DateTime DateTime { get; set; }

        public DateTime? LastUpdateTime { get; set; }

        public DateTime? ConfirmDateTime { get; set; }

        public string Company { get; set; }

        public string Project { get; set; }

        public string TicketTitle { get; set; }

        public string TimeEntryComment { get; set; }

        public double Hours { get; set; }

        public bool IsConfirmed { get; set; }

        public bool IsBillable { get; set; }

        public string ConfirmingEmail { get; set; }
    }
}
