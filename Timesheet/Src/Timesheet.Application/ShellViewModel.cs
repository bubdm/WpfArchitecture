using Prism.Commands;
using Prism.Events;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timesheet.Application.Notifications;
using Timesheet.Infrastructure.Events;
using Timesheet.Infrastructure.Interfaces.SharedServices;

namespace Timesheet.Application.ViewModels
{
    public class ShellViewModel : BindableBase
    {
        IEventAggregator _eventAggregator;
        IIdentityService _identityService;
        public DelegateCommand ShowEmailRequestCommand { get; set; }
        public InteractionRequest<EmailNotification> EmailRequest { get; set; }

        public ShellViewModel(IEventAggregator eventAggregator, IIdentityService identityService)
        {
            _eventAggregator = eventAggregator;
            _identityService = identityService;

            ShowEmailRequestCommand = new DelegateCommand(ShowEmailRequest);
            EmailRequest = new InteractionRequest<EmailNotification>();
        }

        private void ShowEmailRequest()
        {
            var notification = new EmailNotification() { Title = "Email Required" };

            EmailRequest.Raise(notification, returned => 
            {
                if(returned != null && returned.Confirmed )
                {
                    _identityService.CurrentMember = returned.TeamMember;
                }
            });
        }
    }
}
