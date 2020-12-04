using DeviceAssessment.Core.Models.Tablet;

namespace DeviceAssessment.Business.UnitTests
{
    public class TestsHelper
    {
        private const string SerialNumber = "1805C67HD02259";
        private const string EmployeeId = "T1007384";        
        private const int AcademyId = 30006;

        public TabletBatteryMonitorRequest GenerateBatteryMonitorRequestObject(double batteryLevel, string timestamp, string serialNumber = SerialNumber)
        {
            return new TabletBatteryMonitorRequest
            {
                AcademyId = AcademyId,
                BatteryLevel = batteryLevel,
                SerialNumber = serialNumber,
                EmployeeId = EmployeeId,
                TimeStamp = timestamp
            };
        }
    }
}
