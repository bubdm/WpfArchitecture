using MainContent.BaseModels;
using Prism;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timesheet.Infrastructure.Commands;
using Timesheet.Infrastructure.Events;
using Timesheet.Infrastructure.Interfaces;
using Timesheet.Infrastructure.Interfaces.SharedServices;
using Timesheet.Infrastructure.Models;

namespace MainContent.ViewModels
{
    public class DailySummaryViewModel : TabViewModelBase
    {
        #region Properties

        private int _totalRecords;
        public int TotalRecords
        {
            get { return _totalRecords; }
            set { SetProperty(ref _totalRecords, value); }
        }

        public DelegateCommand AddToListCommand { get; set; }
        public DelegateCommand ConfirmCommand { get; set; }
        public DelegateCommand ConfirmListCommand { get; set; }
        public DelegateCommand ClearListCommand { get; set; }

        private TeamMember _selectedMember;
        public TeamMember SelectedMember
        {
            get { return _selectedMember; }
            set { SetProperty(ref _selectedMember, value); _identityService.CurrentMember = _selectedMember; }
        }

        private ObservableCollection<TeamMember> _members;
        public ObservableCollection<TeamMember> Members
        {
            get { return _members; }
            set { SetProperty(ref _members, value); }
        }

        private TimesheetDailySummary _selectedDailySummary;
        public TimesheetDailySummary SelectedDailySummary
        {
            get { return _selectedDailySummary; }
            set { SetProperty(ref _selectedDailySummary, value); }
        }

        private ObservableCollection<TimesheetDailySummary> _dailySummaryData;

        public ObservableCollection<TimesheetDailySummary> DailySummaryData
        {
            get { return _dailySummaryData; }
            set { SetProperty(ref _dailySummaryData, value); }
        }

        private ObservableCollection<TimesheetDailySummary> _listOfSelectedData;

        public ObservableCollection<TimesheetDailySummary> ListOfSelectedData
        {
            get { return _listOfSelectedData; }
            set { SetProperty(ref _listOfSelectedData, value); }
        }

        IEventAggregator _eventAggregator;
        ITimesheetMemberService _timesheetMemberService;
        IIdentityService _identityService;
        ITimesheetService _timesheetService;

        #endregion

        #region Constructors

        public DailySummaryViewModel(IEventAggregator eventAggregator, 
            ITabControlService tabControlService, 
            ITimesheetMemberService timesheetMemberService,
            IIdentityService identityService,
            ITimesheetService timesheetService) 
            : base(tabControlService)
        {
            _timesheetService = timesheetService;
            _identityService = identityService;
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<DailySummaryUpdatedEvent>().Subscribe(DailySummaryUpdated);
            _timesheetMemberService = timesheetMemberService;

            ListOfSelectedData = new ObservableCollection<TimesheetDailySummary>();

            Title = "Daily Summary Data";

            AddToListCommand = new DelegateCommand(AddToList, CanBeConfirmed).ObservesProperty(() => SelectedDailySummary);
            ConfirmCommand = new DelegateCommand(ConfirmRequest, CanBeConfirmed).ObservesProperty(() => SelectedDailySummary);
            ConfirmListCommand = new DelegateCommand(ConfirmListRequest, CanListBeConfirmed)
                .ObservesProperty(() => TotalRecords)
                .ObservesProperty(() => SelectedMember);
            ClearListCommand = new DelegateCommand(ClearListOfSelected, CanClearList).ObservesProperty(() => TotalRecords);

            LoadMembers();            
        }

        #endregion

        #region Private Methods

        private void ClearListOfSelected()
        {
            if (ListOfSelectedData != null)
                ListOfSelectedData.Clear();
            TotalRecords = ListOfSelectedData.Count;
        }

        private void DailySummaryUpdated(IEnumerable<TimesheetDailySummary> dailySummaryData)
        {
            if (IsActive)
            {
                DailySummaryData = new ObservableCollection<TimesheetDailySummary>(dailySummaryData);
                if (dailySummaryData.Count() > 0)
                    Owner = dailySummaryData.First().Email;
            }
        }

        private bool CanClearList()
        {
            return TotalRecords > 0;
        }

        private bool CanListBeConfirmed()
        {
            return TotalRecords > 0 && SelectedMember != null && !string.IsNullOrWhiteSpace(SelectedMember.Email);
        }

        private bool CanBeConfirmed()
        {
            return SelectedDailySummary == null ? false : !SelectedDailySummary.IsConfirmed;
        }

        private void ConfirmListRequest()
        {
            var success = 0;
            var fails = 0;
            var notConfirmedList = new ObservableCollection<TimesheetDailySummary>();
            foreach (var listItem in ListOfSelectedData)
            {
                var result = _timesheetService.ConfirmDate(listItem.Email, listItem.DateTime, _identityService.CurrentMember.Email);

                if (result)
                {
                    listItem.IsConfirmed = true;
                    success++;
                }
                else
                {
                    notConfirmedList.Add(listItem);
                    fails++;
                }
            }

            if (fails > 0)
                ListOfSelectedData = notConfirmedList;
            else
            {
                ListOfSelectedData.Clear();
                TotalRecords = 0;
            }

            _eventAggregator.GetEvent<StatusUpdatedEvent>().Publish(string.Format("DateTimes confirmed: {0} - Datetimes not confirmed: {1}", success, fails));
        }

        private void LoadMembers()
        {
            Members = new ObservableCollection<TeamMember>(_timesheetMemberService.GetActiveMembers());
            if (Members != null && _identityService.CurrentMember != null)
                SelectedMember = Members.First(m => m.Email == _identityService.CurrentMember.Email);
        }

        private void AddToList()
        {
            if (!SelectedDailySummary.IsConfirmed &&
                ListOfSelectedData.FirstOrDefault(ds => ds.DateTime == SelectedDailySummary.DateTime) == null)
            {
                ListOfSelectedData.Add(SelectedDailySummary);
                TotalRecords = _listOfSelectedData.Count;
            }
        }

        private void ConfirmRequest()
        {
            var result = _timesheetService.ConfirmDate(SelectedDailySummary.Email, SelectedDailySummary.DateTime, _identityService.CurrentMember.Email);
            if (result)
            {
                SelectedDailySummary.IsConfirmed = true;
                _eventAggregator.GetEvent<StatusUpdatedEvent>().Publish("DateTimes confirmed successfully");
            }
            else
                _eventAggregator.GetEvent<StatusUpdatedEvent>().Publish(string.Format("DateTime \"{0}\" not confirmed.", SelectedDailySummary.DateTime));
        }

        #endregion

    }
}
