using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timesheet.Application.Notifications;
using Timesheet.Infrastructure.Interfaces;
using Timesheet.Infrastructure.Models;

namespace Timesheet.Application.ViewModels
{
    public class EmailModalViewModel : BindableBase, IInteractionRequestAware
    {
        private EmailNotification _notification;
        public DelegateCommand FinishCommand { get; set; }
        public Action FinishInteraction { get; set; }

        private ObservableCollection<TeamMember> _members;
        public ObservableCollection<TeamMember> Members
        {
            get { return _members; }
            set { SetProperty(ref _members, value); }
        }

        ITimesheetMemberService _timesheetMemberService;

        public INotification Notification
        {
            get
            {
                return _notification;
            }

            set
            {
                if(value is EmailNotification)
                {
                    _notification = value as EmailNotification;
                    OnPropertyChanged(() => Notification);
                }
            }
        }

        public EmailModalViewModel(ITimesheetMemberService timesheetMemberService)
        {
            _timesheetMemberService = timesheetMemberService;
            Members = new ObservableCollection<TeamMember>(_timesheetMemberService.GetActiveMembers());
            FinishCommand = new DelegateCommand(RequestFinishInteraction);            
        }

        private void RequestFinishInteraction()
        {
            _notification.Confirmed = true;           
            FinishInteraction();
        }
    }
}
