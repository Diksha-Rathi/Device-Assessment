using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using DeviceAssessment.Core.Models.Tablet;
using DeviceAssessment.Business.Interfaces;

namespace DeviceAssessment.API.Controllers
{
    [Route("api/battery/")]
    public class BatteryMonitorController : ControllerBase
    {
        private readonly IBatteryService batteryService;
        public BatteryMonitorController(IBatteryService batteryService)
        {
            this.batteryService = batteryService;
        }

        [HttpPost("monitor")]
        public ActionResult<IEnumerable<TabletBatteryMonitorResponse>> Get([FromBody] List<TabletBatteryMonitorRequest> metrics)
        {
            return Ok(this.batteryService.GetAverageTabletBatteryUsage(metrics));
        }
    }
}
