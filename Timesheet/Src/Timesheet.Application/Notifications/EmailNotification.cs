using Prism.Interactivity.InteractionRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timesheet.Infrastructure.Models;

namespace Timesheet.Application.Notifications
{
    public class EmailNotification : Confirmation
    {
        public TeamMember TeamMember { get; set; }

        public EmailNotification()
        {

        }
    }
}
