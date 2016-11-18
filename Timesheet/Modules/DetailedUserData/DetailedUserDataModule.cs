using DetailedUserData.ViewModels;
using DetailedUserData.Views;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timesheet.Infrastructure;
using Timesheet.Infrastructure.Roles;

namespace DetailedUserData
{
    [Roles("Admin")]
    public class DetailedUserDataModule : IModule
    {
        IRegionManager _regionManager;
        IUnityContainer _container;

        public DetailedUserDataModule(IUnityContainer container, IRegionManager regionManager)
        {
            _container = container;
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            _regionManager.RegisterViewWithRegion(RegionNames.DetailsRegion, typeof(Views.DetailsView));
        }
    }
}
