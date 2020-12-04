namespace DeviceAssessment.Core.Models.Tablet
{
    public class TabletBatteryMonitorRequest : TabletDetails
    {
        public int AcademyId { get; set; }

        public string EmployeeId { get; set; }

        public string TimeStamp { get; set; } 

        public double BatteryLevel { get; set; }
    }
}
