using System;
using System.Collections.Generic;
using System.Linq;
using DeviceAssessment.Business.Models;
using DeviceAssessment.Core.Models.Tablet;

namespace DeviceAssessment.Business.Helpers
{
    public class BatteryMonitor
    {
        public Dictionary<string, List<BatteryConsumptionFilter>> Filter(List<TabletBatteryMonitorRequest> metrics)
        {
            var result = new Dictionary<string, List<BatteryConsumptionFilter>>();

            foreach (var metric in metrics)
            {
                if (result.TryGetValue(metric.SerialNumber, out var reading))
                {
                    continue;
                }

                result[metric.SerialNumber] = metrics.Where(x => x.SerialNumber == metric.SerialNumber).Select(x => new BatteryConsumptionFilter(x.TimeStamp, x.BatteryLevel)).ToList();
            }

            return result;
        }

        public string GetAverageForDevice(List<BatteryConsumptionFilter> item)
        {
            int i = 0, hours_difference = 0, days = 0;
            double average = 0, last_updated = 0, battery_level_difference = 0;
            var previous = item[i];

            while (i < item.Count)
            {
                var curr = item[i];
                var matchedItemsCount = item.Count(x => x.Timestamp.Date == curr.Timestamp.Date);

                if (matchedItemsCount > 1)
                {
                    if ((i + 1) < item.Count)
                    {
                        if (item[i + 1].BatteryLevel < curr.BatteryLevel)
                        {
                            // Move ahead, battery has not been recharged
                            previous = curr;
                        }
                        else
                        {
                            // Set previous to new updated recharged battery level
                            previous = item[i + 1];
                        }
                    }

                    // Find the last value before battery recharges for the current selected date 
                    // if more than 2 data points are given for current date
                    while (i < item.Count
                        && item[i].Timestamp.Date == curr.Timestamp.Date
                        && item[i].BatteryLevel <= curr.BatteryLevel)
                    {
                        i++;
                    }

                    // Get last matched value of i
                    i--;

                    hours_difference = GetHoursDifference(curr.Timestamp, item[i].Timestamp, matchedItemsCount);
                    battery_level_difference = GetBatteryLevelDifference(curr.BatteryLevel, item[i].BatteryLevel);
                    last_updated = battery_level_difference / hours_difference * 24;
                    average += last_updated;
                }

                if (matchedItemsCount == 1)
                {
                    if (i > 0 && curr.BatteryLevel < item[i - 1].BatteryLevel)
                    {
                        if (curr.Timestamp.Day - previous.Timestamp.Day > days)
                        {
                            days = curr.Timestamp.Day - previous.Timestamp.Day;
                        }

                        average -= last_updated;
                        hours_difference = GetHoursDifference(previous.Timestamp, curr.Timestamp, matchedItemsCount);
                        battery_level_difference = GetBatteryLevelDifference(previous.BatteryLevel, curr.BatteryLevel);  
                        last_updated = battery_level_difference / hours_difference * (((curr.Timestamp - previous.Timestamp).Days + 1) * 24);
                        average += last_updated;
                    }
                    else
                    {
                        if (days > 0) days--;
                        previous = curr;                        
                    }
                }

                days = battery_level_difference > 0 ? days + 1 : days;
                i++;
            }

            return GetAdjustedAverage(average, days);
        }

        private int GetHoursDifference(DateTime previous, DateTime current, int count)
        {
            int difference = (current - previous).Hours;

            if (count == 1)
            {
                difference += 24 * (current - previous).Days;
            }

            return difference == 0 ? 1 : difference;
        }

        private double GetBatteryLevelDifference(double previous, double current)
        {
            return previous - current;
        }

        private string GetAdjustedAverage(double average, int days)
        {
            return Math.Round(average / days * 100, 2).ToString();
        }
    }
}
