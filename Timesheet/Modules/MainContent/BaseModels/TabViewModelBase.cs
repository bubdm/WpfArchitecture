using Prism;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timesheet.Infrastructure.Interfaces.SharedServices;

namespace MainContent.BaseModels
{
    public abstract class TabViewModelBase : BindableBase, INavigationAware, IActiveAware
    {
        #region Properties

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private string _owner;
        public string Owner
        {
            get { return _owner; }
            set { SetProperty(ref _owner, value); }
        }

        private bool _isActive;
        public bool IsActive
        {
            get { return _isActive; }
            set
            {
                SetProperty(ref _isActive, value);
                SetTabItemSelectedToService(this);
            }
        }

        private ITabControlService _tabControlService;

        public event EventHandler IsActiveChanged;

        #endregion

        #region Constructor

        public TabViewModelBase(ITabControlService tabControlService)
        {
            _tabControlService = tabControlService;
        }

        #endregion

        #region Private Methods

        private void SetTabItemSelectedToService(BindableBase selectedTabItem)
        {
            _tabControlService.TabItemSelected = selectedTabItem;
        }

        #endregion

        #region Navigation

        public void OnNavigatedTo(NavigationContext navigationContext)
        {

        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return string.IsNullOrWhiteSpace(Owner);
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        #endregion
    }
}
