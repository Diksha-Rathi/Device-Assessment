using System.Collections.Generic;
using DeviceAssessment.Core.Models.Tablet;

namespace DeviceAssessment.Business.Interfaces
{
    public interface IBatteryService
    {
        public IEnumerable<TabletBatteryMonitorResponse> GetAverageTabletBatteryUsage(List<TabletBatteryMonitorRequest> metrics);
    }
}
