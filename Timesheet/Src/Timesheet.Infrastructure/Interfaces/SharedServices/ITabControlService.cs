using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timesheet.Infrastructure.Interfaces.SharedServices
{
    public interface ITabControlService
    {
        BindableBase TabItemSelected { get; set; }
    }
}
