using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timesheet.Infrastructure.Models;

namespace Timesheet.Infrastructure.Interfaces.SharedServices
{
    public interface IIdentityService
    {
        TeamMember CurrentMember { get; set; }
    }
}
