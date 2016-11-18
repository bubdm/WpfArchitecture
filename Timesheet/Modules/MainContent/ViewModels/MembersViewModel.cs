using Prism.Commands;
using Prism.Events;
using Prism.Interactivity;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Timesheet.Infrastructure.Events;
using Timesheet.Infrastructure.Interfaces;
using Timesheet.Infrastructure.Models;

namespace MainContent.ViewModels
{
    public class MembersViewModel : BindableBase, INavigationAware
    {
        #region Properties

        private List<TeamMember> _members;

        public List<TeamMember> Members
        {
            get { return _members; }
            set { SetProperty(ref _members, value); }
        }

        public ICommand SelectedCommand { get; set; }

        private TeamMember _selectedMember;

        public TeamMember SelectedMember
        {
            get { return _selectedMember; }
            set { SetProperty(ref _selectedMember, value); }
        }

        ITimesheetMemberService _timesheetMemberService;
        IEventAggregator _eventAggregator;

        #endregion

        #region Constructors

        public MembersViewModel(IEventAggregator eventAggregator, ITimesheetMemberService timesheetMemberService)
        {
            _timesheetMemberService = timesheetMemberService;
            _eventAggregator = eventAggregator;
            GetActiveMembers();

            SelectedCommand = new DelegateCommand<object[]>(SelectedItem);
        }

        #endregion

        #region Private Methods

        private void SelectedItem(object[] members)
        {
            if (members != null && members.Count() > 0)
                _eventAggregator.GetEvent<TeamMemberUpdatedEvent>().Publish(members.First() as TeamMember);
        }

        private void GetActiveMembers()
        {
            var request = _timesheetMemberService.GetActiveMembers();
            Members = new List<TeamMember>(request);
        }

        #endregion

        #region Navigation

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        #endregion
    }
}
