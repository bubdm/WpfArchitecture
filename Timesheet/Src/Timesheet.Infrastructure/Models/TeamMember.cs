using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timesheet.Infrastructure.Models
{
    public class TeamMember
    {
        public int PmpId { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Company { get; set; }
        public Guid PublicId { get; set; }
    }
}
