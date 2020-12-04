namespace DeviceAssessment.Core.Models.Tablet
{
    public class TabletBatteryMonitorResponse : TabletDetails
    {
        public TabletBatteryMonitorResponse(string id, string value)
        {
            this.SerialNumber = id;
            this.AverageUsage = value;
        }

        public string AverageUsage { get; set; }
    }
}
