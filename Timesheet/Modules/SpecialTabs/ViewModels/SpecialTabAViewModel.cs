using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timesheet.Infrastructure;
using Timesheet.Infrastructure.Prism.CustomCode;

namespace SpecialTabs.ViewModels
{
    public class SpecialTabAViewModel : BindableBase, INavigationAware, IRegionManagerAware
    {
        #region Properties

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public DelegateCommand AddToTabControlRegionCommand { get; set; }

        public IRegionManager RegionManager { get; set; }


        #endregion

        #region Constructor

        public SpecialTabAViewModel()
        {
            AddToTabControlRegionCommand = new DelegateCommand(AddToTabControl);

            Title = "Special Tab";
        }

        #endregion

        #region Private Methods

        private void AddToTabControl()
        {
            RegionManager.RequestNavigate(RegionNames.TabControlRegion, ViewNames.SpecialTabBView);
        }

        #endregion

        #region Navigation

        public void OnNavigatedTo(NavigationContext navigationContext)
        {

        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return false;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        #endregion

    }
}
