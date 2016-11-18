using Prism.Commands;
using Prism.Events;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timesheet.Infrastructure;
using Timesheet.Infrastructure.Commands;
using Timesheet.Infrastructure.Events;
using Timesheet.Infrastructure.Interfaces;
using Timesheet.Infrastructure.Interfaces.SharedServices;
using Timesheet.Infrastructure.Models;

namespace MainContent.ViewModels
{
    public class ToolbarViewModel : BindableBase, INavigationAware
    {
        #region Properties

        private string _emailAddress;

        public string EmailAddress
        {
            get { return _emailAddress; }
            set { SetProperty(ref _emailAddress, value); }
        }

        private DateTime _startDate;

        public DateTime StartDate
        {
            get { return _startDate; }
            set { SetProperty(ref _startDate, value); }
        }

        private DateTime _endDate;

        public DateTime EndDate
        {
            get { return _endDate; }
            set { SetProperty(ref _endDate, value); }
        }

        public DelegateCommand RequestData { get; set; }
        public DelegateCommand BackCommand { get; set; }
        public InteractionRequest<INotification> NotificationRequest { get; set; }

        IEventAggregator _eventAggregator;
        ITimesheetService _timesheetService;
        IRegionManager _regionManager;
        ITabControlService _tabControlService;

        private IRegionNavigationJournal _journal;

        private bool _canGoBack = true;

        public bool CanGoBack
        {
            get { return _canGoBack; }
            set { SetProperty(ref _canGoBack, value); }
        }


        #endregion

        #region Constructors

        public ToolbarViewModel(IEventAggregator eventAggregator, ITimesheetService timesheetService, IRegionManager regionManager, ITabControlService tabControlService)
        {
            RequestData = new DelegateCommand(GetTimesheetData, CanExecuteRequest)
                .ObservesProperty(() => EmailAddress)
                .ObservesProperty(() => StartDate)
                .ObservesProperty(() => EndDate);

            _eventAggregator = eventAggregator;
            _regionManager = regionManager;
            _tabControlService = tabControlService;
            _timesheetService = timesheetService;

            NotificationRequest = new InteractionRequest<INotification>();
            BackCommand = new DelegateCommand(GoBack).ObservesCanExecute(p => CanGoBack);

            GlobalCommands.NavigationBackCommand.RegisterCommand(BackCommand);

            StartDate = DateTime.Now;
            EndDate = DateTime.Now;
        }

        #endregion

        #region Private Methods

        private bool CanExecuteRequest()
        {
            if (string.IsNullOrEmpty(EmailAddress))
                return false;

            if (StartDate > EndDate)
                return false;

            return true;
        }

        private void GetTimesheetData()
        {
            if ((_tabControlService.TabItemSelected as TimesheetViewModel) != null)
            {
                var timeSheetData = _timesheetService.GetTimesheetData(EmailAddress, StartDate, EndDate);
                _eventAggregator.GetEvent<TimesheetUpdatedEvent>().Publish(timeSheetData);

                if (timeSheetData.Count() > 0)
                    _eventAggregator.GetEvent<StatusUpdatedEvent>().Publish(string.Format("Timesheet information for {0} updated. Total Records: {1}", EmailAddress, timeSheetData.Count()));
                else
                    NotificationRequest.Raise(new Notification { Title = "Timesheet data empty", Content = "Response from service doesn't have data to show." });
            }
            else if((_tabControlService.TabItemSelected as DailySummaryViewModel) != null)
            {
                var dailySummary = _timesheetService.GetDailySummary(EmailAddress, StartDate, EndDate);
                _eventAggregator.GetEvent<DailySummaryUpdatedEvent>().Publish(dailySummary);

                if (dailySummary.Count() > 0)
                    _eventAggregator.GetEvent<StatusUpdatedEvent>().Publish(string.Format("Daily Summary information for {0} updated. Total Records: {1}", EmailAddress, dailySummary.Count()));
                else
                    NotificationRequest.Raise(new Notification { Title = "Daily Summary data empty", Content = "Response from service doesn't have data to show." });
            }
        }

        private void NagivateToMembers()
        {
            _regionManager.RequestNavigate(RegionNames.ToolbarRegion, new Uri(ViewNames.InitialToolbarView, UriKind.Relative));
            _regionManager.RequestNavigate(RegionNames.ContentRegion, new Uri(ViewNames.MembersView, UriKind.Relative));

            _eventAggregator.GetEvent<StatusUpdatedEvent>().Publish("Initial Status");
        }

        #endregion

        #region Navigation

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            _journal = navigationContext.NavigationService.Journal;
            CanGoBack = _journal.CanGoBack;

            var teamMember = navigationContext.Parameters["TeamMember"] as TeamMember;
            if(teamMember != null)
                EmailAddress = teamMember.Email;            
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            var teamMember = navigationContext.Parameters["TeamMember"] as TeamMember;
            if (teamMember != null && teamMember.Email.Equals(EmailAddress))
                return true;
            return false;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            
        }

        #endregion

        #region Navigation Journal

        private void GoBack()
        {
            _eventAggregator.GetEvent<StatusUpdatedEvent>().Publish("You used \"Go Back\" functionality of Navigation Journal.");
            _journal.GoBack();
        }

        #endregion
    }
}
