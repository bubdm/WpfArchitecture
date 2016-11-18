using Prism.Regions;

namespace Timesheet.Infrastructure.Prism.CustomCode
{
    public interface IRegionManagerAware
    {
        IRegionManager RegionManager { get; set; }
    }
}