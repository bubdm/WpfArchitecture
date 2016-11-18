using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timesheet.Infrastructure.Models;

namespace DetailedUserData.ViewModels
{
    public class DetailsViewModel : BindableBase
    {
        private TeamMember _member;

        public TeamMember Member
        {
            get { return _member; }
            set { SetProperty(ref _member, value); }
        }

        public DetailsViewModel()
        {

        }
    }
}
