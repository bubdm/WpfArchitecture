using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timesheet.Infrastructure;
using Timesheet.Infrastructure.Events;
using Timesheet.Infrastructure.Models;

namespace MainContent.ViewModels
{
    public class InitialToolbarViewModel : BindableBase, INavigationAware
    {
        #region Properties

        private string _requestInfo = "Request Info";

        IEventAggregator _eventAggregator;
        IRegionManager _regionManager;

        public string RequestInfo
        {
            get { return _requestInfo; }
            set { SetProperty(ref _requestInfo, value); }
        }

        public TeamMember SelectedTeamMember { get; set; }

        public DelegateCommand RequestInfoCommand { get; set; }

        #endregion

        #region Constructors

        public InitialToolbarViewModel(IEventAggregator eventAggregator, IRegionManager regionManager)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<TeamMemberUpdatedEvent>().Subscribe(TeamMemberSelected);
            _regionManager = regionManager;
            RequestInfoCommand = new DelegateCommand(RequestInformation);
        }

        #endregion

        #region Private Methods

        private void TeamMemberSelected(TeamMember teamMember)
        {
            SelectedTeamMember = teamMember;
        }

        private void RequestInformation()
        {
            var parameters = new NavigationParameters();
            parameters.Add("TeamMember", SelectedTeamMember);

            _regionManager.RequestNavigate(RegionNames.ContentRegion, new Uri(ViewNames.GridContentView, UriKind.Relative));
            _regionManager.RequestNavigate(RegionNames.ToolbarRegion, new Uri(ViewNames.ToolbarView, UriKind.Relative), parameters);

            _regionManager.RequestNavigate(RegionNames.GridTabsRegion, new Uri(ViewNames.TimesheetView, UriKind.Relative));
            _regionManager.RequestNavigate(RegionNames.GridTabsRegion, new Uri(ViewNames.DailySummaryView, UriKind.Relative));
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
