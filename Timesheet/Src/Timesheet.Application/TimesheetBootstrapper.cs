using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Unity;
using System.Windows;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Timesheet.Infrastructure.Interfaces;
using Timesheet.Data;
using System.Security.Principal;
using System.Threading;
using Timesheet.Infrastructure.Roles;
using Timesheet.Infrastructure.Interfaces.SharedServices;
using Prism.Regions;
using Timesheet.Infrastructure.Prism.CustomCode;

namespace Timesheet.Application
{
    public class TimesheetBootstrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<Views.Shell>();
        }

        protected override void InitializeShell()
        {
            var identity = WindowsIdentity.GetCurrent();
            GenericPrincipal gprincipal;
            gprincipal = new GenericPrincipal(identity, new string[] { "User", "Admin" });

            //var question = MessageBox.Show("Do you want to use the Application as Administrator?", "User Role Selection", MessageBoxButton.YesNo, MessageBoxImage.Question);
            //if (question == MessageBoxResult.Yes)
            //    gprincipal = new GenericPrincipal(identity, new string[] { "User", "Admin" });
            //else
            //    gprincipal = new GenericPrincipal(identity, new string[] { "User" });

            Thread.CurrentPrincipal = gprincipal;

            base.InitializeShell();
            System.Windows.Application.Current.MainWindow = (Window)Shell;
            System.Windows.Application.Current.MainWindow.Show();
        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            Container.RegisterType<ITimesheetService, TimesheetService>();
            Container.RegisterType<ITimesheetMemberService, TimesheetMemberService>();
            Container.RegisterType<ITabControlService, TabControlService>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IRegionNavigationContentLoader, ScopedRegionNavigationContentLoader>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IIdentityService, IdentityService>(new ContainerControlledLifetimeManager());

            Container.RegisterType<IModuleInitializer, RolesBasedModuleInitializer>(new ContainerControlledLifetimeManager());
        }

        protected override IRegionBehaviorFactory ConfigureDefaultRegionBehaviors()
        {
            var behaviors = base.ConfigureDefaultRegionBehaviors();

            behaviors.AddIfMissing(RegionManagerAwareBehavior.BehaviorKey, typeof(RegionManagerAwareBehavior));

            return behaviors;
        }

        #region Load Modules

        // To load a module from code.
        //protected override void ConfigureModuleCatalog()
        //{
        //    Type moduleAType = typeof(ModuleAModule);
        //    ModuleCatalog.AddModule(new ModuleInfo()
        //    {
        //        ModuleName = moduleAType.Name,
        //        ModuleType = moduleAType.AssemblyQualifiedName,
        //        InitializationMode = InitializationMode.WhenAvailable
        //    });
        //}

        // To load module from directory. ModuleA.ddl shoul be copied to the Modules folder inside bin folder.
        //protected override IModuleCatalog CreateModuleCatalog()
        //{
        //    return new DirectoryModuleCatalog() { ModulePath = @".\Modules" };
        //}

        // To load module from xaml file. Uri has to point to Xaml Catalog defined as a resource.
        //protected override IModuleCatalog CreateModuleCatalog()
        //{
        //    return Prism.Modularity.ModuleCatalog.CreateFromXaml(new Uri("", UriKind.Relative));
        //}

        // To load module from config file. Add references into the config file.
        protected override IModuleCatalog CreateModuleCatalog()
        {
            return new ConfigurationModuleCatalog();
        }

        #endregion
    }
}
