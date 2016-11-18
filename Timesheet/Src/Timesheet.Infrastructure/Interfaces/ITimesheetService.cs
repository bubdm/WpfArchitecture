using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timesheet.Infrastructure.Models;

namespace Timesheet.Infrastructure.Interfaces
{
    public interface ITimesheetService
    {
        IEnumerable<TimesheetData> GetTimesheetData(string email, DateTime startDate, DateTime endDate);

        IEnumerable<TimesheetDailySummary> GetDailySummary(string email, DateTime startDate, DateTime endDate);

        bool ConfirmDate(string userEmail, DateTime timeEntryDate, string confirmingEmail);
    }
}
