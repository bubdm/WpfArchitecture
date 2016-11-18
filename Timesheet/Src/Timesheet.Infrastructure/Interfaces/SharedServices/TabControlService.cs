using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;

namespace Timesheet.Infrastructure.Interfaces.SharedServices
{
    public class TabControlService : ITabControlService
    {
        public BindableBase TabItemSelected { get; set; }
    }
}
