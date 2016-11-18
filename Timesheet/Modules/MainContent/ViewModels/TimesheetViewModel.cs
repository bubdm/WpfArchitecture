using MainContent.BaseModels;
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
using Timesheet.Infrastructure.Events;
using Timesheet.Infrastructure.Interfaces;
using Timesheet.Infrastructure.Interfaces.SharedServices;
using Timesheet.Infrastructure.Models;

namespace MainContent.ViewModels
{
    public class TimesheetViewModel : TabViewModelBase, INavigationAware
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

        private TimesheetData _selectedTimesheetData;
        public TimesheetData SelectedTimesheetData
        {
            get { return _selectedTimesheetData; }
            set { SetProperty(ref _selectedTimesheetData, value); }
        }

        private ObservableCollection<TimesheetData> _timeSheetData;

        public ObservableCollection<TimesheetData> TimesheetData
        {
            get { return _timeSheetData; }
            set { SetProperty(ref _timeSheetData, value); }
        }

        private ObservableCollection<TimesheetData> _listOfSelectedData;

        public ObservableCollection<TimesheetData> ListOfSelectedData
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

        public TimesheetViewModel(IEventAggregator eventAggregator,
            ITabControlService tabControlService, 
            ITimesheetMemberService timesheetMemberService,
            IIdentityService identityService, 
            ITimesheetService timesheetService) : base(tabControlService)
        {
            _timesheetService = timesheetService;
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<TimesheetUpdatedEvent>().Subscribe(TimesheetUpdated);
            _identityService = identityService;
            _timesheetMemberService = timesheetMemberService;

            ListOfSelectedData = new ObservableCollection<TimesheetData>();

            Title = "Timesheet Data";

            AddToListCommand = new DelegateCommand(AddToList, CanBeConfirmed).ObservesProperty(() => SelectedTimesheetData);
            ConfirmCommand = new DelegateCommand(ConfirmRequest, CanBeConfirmed).ObservesProperty(() => SelectedTimesheetData);
            ConfirmListCommand = new DelegateCommand(ConfirmListRequest, CanListBeConfirmed).ObservesProperty(() => TotalRecords);
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

        private void TimesheetUpdated(IEnumerable<TimesheetData> timesheetData)
        {
            if (IsActive)
            {
                TimesheetData = new ObservableCollection<TimesheetData>(timesheetData);
                if (timesheetData.Count() > 0)
                    Owner = timesheetData.First().Email;
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
            return SelectedTimesheetData == null ? false : !SelectedTimesheetData.IsConfirmed;
        }

        private void ConfirmListRequest()
        {
            var success = 0;
            var fails = 0;
            var notConfirmedList = new ObservableCollection<TimesheetData>();
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
                ListOfSelectedData.Clear();
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
            if (!SelectedTimesheetData.IsConfirmed &&
                ListOfSelectedData.FirstOrDefault(ds => ds.DateTime == SelectedTimesheetData.DateTime) == null)
            {
                ListOfSelectedData.Add(SelectedTimesheetData);
                TotalRecords = _listOfSelectedData.Count;
            }
        }

        private void ConfirmRequest()
        {
            var result = _timesheetService.ConfirmDate(SelectedTimesheetData.Email, SelectedTimesheetData.DateTime, _identityService.CurrentMember.Email);
            if (result)
            {
                SelectedTimesheetData.IsConfirmed = true;
                _eventAggregator.GetEvent<StatusUpdatedEvent>().Publish("DateTimes confirmed successfully");
            }
            else
                _eventAggregator.GetEvent<StatusUpdatedEvent>().Publish(string.Format("DateTime \"{0}\" not confirmed.", SelectedTimesheetData.DateTime));
        }

        #endregion

    }
}
