using System.Collections.Generic;
using System.Linq;
using Xunit;
using DeviceAssessment.Core.Models.Tablet;
using DeviceAssessment.Business.Classes;

namespace DeviceAssessment.Business.UnitTests
{
    public class BatteryServiceTests
    {
        private readonly BatteryService batteryService;
        private readonly TestsHelper helper;

        public BatteryServiceTests()
        {
            this.batteryService = new BatteryService();
            this.helper = new TestsHelper();
        }

        [Fact]
        public void GetAverageTabletBatteryUsage_ForEmptyList_ReturnsEmptyResponse()
        {
            var metrics = new List<TabletBatteryMonitorRequest>();
            var result = this.batteryService.GetAverageTabletBatteryUsage(metrics);
            Assert.False(result.Any());
        }

        [Fact]
        public void GetAverageTabletBatteryUsage_ForNullObject_ReturnsEmptyResponse()
        {
            List<TabletBatteryMonitorRequest> metrics = null;
            var result = this.batteryService.GetAverageTabletBatteryUsage(metrics);
            Assert.False(result.Any());
        }

        [Fact]
        public void GetAverageTabletBatteryUsage_ForSampleTestCase_ReturnsAverage()
        {
            var metrics = new List<TabletBatteryMonitorRequest>
                {
                    this.helper.GenerateBatteryMonitorRequestObject(0.90, "2019-05-17T21:04:25.902"),
                    this.helper.GenerateBatteryMonitorRequestObject(1.0, "2019-05-17T09:04:25.902"),
                    this.helper.GenerateBatteryMonitorRequestObject(0.80, "2019-05-18T21:04:25.902"),
                };

            var result = this.batteryService.GetAverageTabletBatteryUsage(metrics);

            Assert.Single(result);
            Assert.Equal("13.33", result.First().AverageUsage);
        }

        [Fact]
        public void GetAverageTabletBatteryUsage_ForSingleDataPointsDailyWithoutRecharge_ReturnsAverage()
        {
            var metrics = new List<TabletBatteryMonitorRequest>
                {
                    this.helper.GenerateBatteryMonitorRequestObject(1.0, "2019-05-17T07:09:08.902"),
                    this.helper.GenerateBatteryMonitorRequestObject(0.80, "2019-05-19T23:09:08.902"),
                    this.helper.GenerateBatteryMonitorRequestObject(0.60, "2019-05-21T21:09:08.902")
                };

            var result = this.batteryService.GetAverageTabletBatteryUsage(metrics);

            Assert.Single(result);
            Assert.Equal("8.73", result.First().AverageUsage);
        }

        [Fact]
        public void GetAverageTabletBatteryUsage_ForSingleDataPointsDailyWithRecharge_ReturnsAverage()
        {
            var metrics = new List<TabletBatteryMonitorRequest>
                {
                    this.helper.GenerateBatteryMonitorRequestObject(1.0, "2019-05-17T09:09:08.902"),
                    this.helper.GenerateBatteryMonitorRequestObject(0.80, "2019-05-19T21:09:08.902"),
                    this.helper.GenerateBatteryMonitorRequestObject(0.90, "2019-05-21T20:09:08.902")
                };

            var result = this.batteryService.GetAverageTabletBatteryUsage(metrics);

            Assert.Single(result);
            Assert.Equal("8", result.First().AverageUsage);
        }

        [Fact]
        public void GetAverageTabletBatteryUsage_ForMultipleDataPointsDailyWithoutRecharge_ReturnsAverage()
        {
            var metrics = new List<TabletBatteryMonitorRequest>
                {
                    this.helper.GenerateBatteryMonitorRequestObject(0.95, "2019-05-17T21:00:08.902"),
                    this.helper.GenerateBatteryMonitorRequestObject(1.0, "2019-05-17T09:09:00.902"),
                    this.helper.GenerateBatteryMonitorRequestObject(0.60, "2019-05-18T19:04:25.902"),
                    this.helper.GenerateBatteryMonitorRequestObject(0.70, "2019-05-18T07:04:25.902"),
                    this.helper.GenerateBatteryMonitorRequestObject(0.50, "2019-05-20T07:04:25.902"),
                    this.helper.GenerateBatteryMonitorRequestObject(0.30, "2019-05-20T19:04:25.902")
                };

            var result = this.batteryService.GetAverageTabletBatteryUsage(metrics);

            Assert.Single(result);
            Assert.Equal("23.64", result.First().AverageUsage);
        }

        [Fact]
        public void GetAverageTabletBatteryUsage_ForMultipleDataPointsDailyWithRecharge_ReturnsAverage()
        {
            var metrics = new List<TabletBatteryMonitorRequest>
                {
                    this.helper.GenerateBatteryMonitorRequestObject(0.95, "2019-05-17T21:00:08.902"),
                    this.helper.GenerateBatteryMonitorRequestObject(1.0, "2019-05-17T09:09:00.902"),
                    this.helper.GenerateBatteryMonitorRequestObject(0.60, "2019-05-18T19:04:25.902"),
                    this.helper.GenerateBatteryMonitorRequestObject(0.70, "2019-05-18T07:04:25.902"),
                    this.helper.GenerateBatteryMonitorRequestObject(0.30, "2019-05-20T07:04:25.902"),
                    this.helper.GenerateBatteryMonitorRequestObject(0.50, "2019-05-20T19:04:25.902")
                };

            var result = this.batteryService.GetAverageTabletBatteryUsage(metrics);

            Assert.Single(result);
            Assert.Equal("15.45", result.First().AverageUsage);
        }

        [Fact]
        public void GetAverageTabletBatteryUsage_ForSingleAndMultipleDataPointsWithoutRecharge_ReturnsAverage()
        {
            var metrics = new List<TabletBatteryMonitorRequest>
                {
                    this.helper.GenerateBatteryMonitorRequestObject(0.90, "2019-05-17T21:04:25.902"),
                    this.helper.GenerateBatteryMonitorRequestObject(1.0, "2019-05-17T09:04:25.902"),
                    this.helper.GenerateBatteryMonitorRequestObject(0.80, "2019-05-18T21:04:25.902"),
                    this.helper.GenerateBatteryMonitorRequestObject(0.60, "2019-05-19T21:04:25.902"),
                    this.helper.GenerateBatteryMonitorRequestObject(0.30, "2019-05-21T21:04:25.902")
                };

            var result = this.batteryService.GetAverageTabletBatteryUsage(metrics);

            Assert.Single(result);
            Assert.Equal("15.56", result.First().AverageUsage);
        }

        [Fact]
        public void GetAverageTabletBatteryUsage_ForSingleAndMultipleDataPointsWithRecharge_ReturnsAverage()
        {
            var metrics = new List<TabletBatteryMonitorRequest>
                {
                    this.helper.GenerateBatteryMonitorRequestObject(0.90, "2019-05-17T21:04:25.902"),
                    this.helper.GenerateBatteryMonitorRequestObject(1.0, "2019-05-17T09:04:25.902"),
                    this.helper.GenerateBatteryMonitorRequestObject(0.80, "2019-05-18T23:04:25.902"),
                    this.helper.GenerateBatteryMonitorRequestObject(0.60, "2019-05-19T21:04:25.902"),
                    this.helper.GenerateBatteryMonitorRequestObject(0.60, "2019-05-21T21:04:25.902")
                };

            var result = this.batteryService.GetAverageTabletBatteryUsage(metrics);

            Assert.Single(result);
            Assert.Equal("16", result.First().AverageUsage);
        }
    }
}
