using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timesheet.Infrastructure;
using Timesheet.Infrastructure.Commands;
using Timesheet.Infrastructure.Interfaces.SharedServices;

namespace MainContent.ViewModels
{
    public class GridContentViewModel : BindableBase, INavigationAware, IRegionMemberLifetime
    {
        #region Properties

        public DelegateCommand BackCommand { get; set; }
        public DelegateCommand AddDailyInstanceCommand { get; set; }
        public DelegateCommand AddTimesheetInstanceCommand { get; set; }
        public DelegateCommand AddSpecialTabCommand { get; set; }

        private IRegionManager _regionManager;
        private IRegionNavigationJournal _journal;

        private bool _canGoBack;

        public bool CanGoBack
        {
            get { return _canGoBack; }
            set { SetProperty(ref _canGoBack, value); }
        }

        #endregion

        #region Constructors

        public GridContentViewModel(IRegionManager regionManager)
        {
            BackCommand = new DelegateCommand(GoBack).ObservesCanExecute(p => CanGoBack);
            AddDailyInstanceCommand = new DelegateCommand(AddDailyInstance);
            AddTimesheetInstanceCommand = new DelegateCommand(AddTimesheeInstance);
            AddSpecialTabCommand = new DelegateCommand(AddSpecialTab);

            GlobalCommands.NavigationBackCommand.RegisterCommand(BackCommand);

            _regionManager = regionManager;
        }

        #endregion

        #region Private Methods

        private void AddTimesheeInstance()
        {
            _regionManager.RequestNavigate(RegionNames.GridTabsRegion, ViewNames.TimesheetView);
        }

        private void AddDailyInstance()
        {
            _regionManager.RequestNavigate(RegionNames.GridTabsRegion, ViewNames.DailySummaryView);
        }

        private void AddSpecialTab()
        {
            _regionManager.RequestNavigate(RegionNames.GridTabsRegion, ViewNames.SpecialTabAView);
        }

        #endregion

        #region LifeTime

        public bool KeepAlive
        {
            get
            {
                return true;
            }
        }

        #endregion

        #region Navigation

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            _journal = navigationContext.NavigationService.Journal;
            CanGoBack = _journal.CanGoBack;
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        #endregion

        #region Navigation Journal

        private void GoBack()
        {
            _journal.GoBack();
        }

        #endregion
    }
}
