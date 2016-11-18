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

namespace MainContent
{
    [Roles("User")]
    public class MainContentModule : IModule
    {
        IUnityContainer _container;
        IRegionManager _regionManager;

        public MainContentModule(IUnityContainer container, IRegionManager regionManager)
        {
            _container = container;
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            _container.RegisterType<object, Views.ToolbarView>(ViewNames.ToolbarView);
            _container.RegisterType<object, Views.GridContentView>(ViewNames.GridContentView);
            _container.RegisterType<object, Views.InitialToolbarView>(ViewNames.InitialToolbarView);
            _container.RegisterType<object, Views.MembersView>(ViewNames.MembersView);
            _container.RegisterType<object, Views.TimesheetView>(ViewNames.TimesheetView);
            _container.RegisterType<object, Views.DailySummaryView>(ViewNames.DailySummaryView);

            _regionManager.RequestNavigate(RegionNames.ToolbarRegion, ViewNames.InitialToolbarView);
            _regionManager.RequestNavigate(RegionNames.ContentRegion, ViewNames.MembersView);
        }
    }
}
