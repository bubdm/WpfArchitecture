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

namespace StatusBar
{
    [Roles("User")]
    public class StatusBarModule : IModule
    {
        IUnityContainer _container;
        IRegionManager _regionManager;

        public StatusBarModule(IUnityContainer container, IRegionManager regionManager)
        {
            _container = container;
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            _regionManager.RegisterViewWithRegion(RegionNames.StatusBarRegion, typeof(Views.StatusBarView));
        }
    }
}
