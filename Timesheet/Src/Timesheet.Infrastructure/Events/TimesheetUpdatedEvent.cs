using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timesheet.Infrastructure.Models;

namespace Timesheet.Infrastructure.Events
{
    public class TimesheetUpdatedEvent : PubSubEvent<IEnumerable<TimesheetData>>
    {
    }
}
