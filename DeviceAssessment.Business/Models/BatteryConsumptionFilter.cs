using System;

namespace DeviceAssessment.Business.Models
{
    public class BatteryConsumptionFilter
    {
        public BatteryConsumptionFilter(string time, double battery)
        {
            this.Timestamp = DateTime.Parse(time);
            this.BatteryLevel = battery;
        }

        public DateTime Timestamp { get; set; }
        public double BatteryLevel { get; set; }
    }
}
