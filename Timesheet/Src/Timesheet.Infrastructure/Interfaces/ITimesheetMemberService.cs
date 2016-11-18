using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timesheet.Infrastructure.Models;

namespace Timesheet.Infrastructure.Interfaces
{
    public interface ITimesheetMemberService
    {
        TeamMember GetMemberByEmail();
        IEnumerable<TeamMember> GetActiveMembers();
    }
}
