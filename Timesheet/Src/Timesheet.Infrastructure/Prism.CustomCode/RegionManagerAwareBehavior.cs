using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Timesheet.Infrastructure.Prism.CustomCode
{
    public class RegionManagerAwareBehavior : RegionBehavior
    {
        public const string BehaviorKey = "RegionManagerAwareBehavior";
        protected override void OnAttach()
        {
            Region.ActiveViews.CollectionChanged += ActiveViews_CollectionChanged;
        }
        private void ActiveViews_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                foreach (var item in e.NewItems)
                {
                    IRegionManager regionManager = Region.RegionManager;
                    FrameworkElement element = item as FrameworkElement;
                    if (element != null)
                    {
                        IRegionManager scopedRegionManager = element.GetValue(RegionManager.RegionManagerProperty) as IRegionManager;
                        if (scopedRegionManager != null)
                            regionManager = scopedRegionManager;
                    }
                    InvokeOnRegionManagerAwareElement(item, x => x.RegionManager = regionManager);
                }
            }
            else if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
            {
                foreach (var item in e.OldItems)
                {
                    InvokeOnRegionManagerAwareElement(item, x => x.RegionManager = null);
                }
            }
        }
        static void InvokeOnRegionManagerAwareElement(object item, Action<IRegionManagerAware> invocation)
        {
            var rmAwareItem = item as IRegionManagerAware;
            if (rmAwareItem != null)
                invocation(rmAwareItem);
            var fwElement = item as FrameworkElement;
            if (fwElement != null)
            {
                var rmAwareDataContext = fwElement.DataContext as IRegionManagerAware;
                if (rmAwareDataContext != null)
                {
                    var fwElementParent = fwElement.Parent as FrameworkElement;
                    if (fwElementParent != null)
                    {
                        var rmAwareDataContextParent = fwElementParent.DataContext as IRegionManager;
                        if (rmAwareDataContextParent != null)
                        {
                            if (rmAwareDataContext == rmAwareDataContextParent)
                            {
                                return;
                            }
                        }
                    }

                    invocation(rmAwareDataContext);
                }
            }
        }
    }
}
