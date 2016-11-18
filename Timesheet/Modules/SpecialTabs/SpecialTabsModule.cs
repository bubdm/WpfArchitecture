using Microsoft.Practices.Unity;
using Prism.Modularity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timesheet.Infrastructure;
using Timesheet.Infrastructure.Roles;

namespace SpecialTabs
{
    [Roles("User")]
    public class SpecialTabsModule : IModule
    {
        IUnityContainer _container;

        public SpecialTabsModule(IUnityContainer container)
        {
            _container = container;
        }

        public void Initialize()
        {
            _container.RegisterType<object, Views.SpecialTabAView>(ViewNames.SpecialTabAView);
            _container.RegisterType<object, Views.SpecialTabBView>(ViewNames.SpecialTabBView);
        }
    }
}
