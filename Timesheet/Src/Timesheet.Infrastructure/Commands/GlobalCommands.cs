using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timesheet.Infrastructure.Commands
{
    public static class GlobalCommands
    {
        public static CompositeCommand NavigationBackCommand = new CompositeCommand();
    }
}
