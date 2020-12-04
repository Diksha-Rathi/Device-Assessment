using System;
using System.Collections.Generic;
using System.Linq;
using DeviceAssessment.Business.Helpers;
using DeviceAssessment.Business.Interfaces;
using DeviceAssessment.Core.Models.Tablet;

namespace DeviceAssessment.Business.Classes
{
    public class BatteryService : IBatteryService
    {
        public IEnumerable<TabletBatteryMonitorResponse> GetAverageTabletBatteryUsage(List<TabletBatteryMonitorRequest> metrics)
        {
            var result = new List<TabletBatteryMonitorResponse>();
            if (metrics != null && metrics.Any())
            {
                var batteryMonitor = new BatteryMonitor();
                var filtered_metrics = batteryMonitor.Filter(metrics);

                foreach (var item in filtered_metrics)
                {
                    if (item.Value.Count == 1)
                    {
                        result.Add(new TabletBatteryMonitorResponse(item.Key, "unknown"));
                        continue;
                    }

                    item.Value.Sort((x, y) => DateTime.Compare(x.Timestamp, y.Timestamp));
                    result.Add(new TabletBatteryMonitorResponse(item.Key, batteryMonitor.GetAverageForDevice(item.Value)));
                }
            }

            return result;
        }        
    }
}
