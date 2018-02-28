using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;

namespace ApplicationInsightsClientIp
{
    class Program
    {
        static void Main(string[] args)
        {
            var appInsightsSender = new AppInsightsSender("InstrumentationKey");
            appInsightsSender.SendTelemetry();
            Console.ReadKey();
        }
    }

    class AppInsightsSender
    {
        private TelemetryClient _tc;

        public AppInsightsSender(string instrumentationKey)
        {
            SetTelemetryClient(instrumentationKey);
        }

        void SetTelemetryClient(string instrumentationKey)
        {
            TelemetryConfiguration.Active.InstrumentationKey = instrumentationKey;
            _tc = new TelemetryClient {InstrumentationKey = instrumentationKey};
            _tc.Context.User.Id = "userId";
            _tc.Context.Location.Ip = "192.168.1.1";
            _tc.Context.Device.OperatingSystem = Environment.OSVersion.ToString();
        }

        public void SendTelemetry()
        {
            _tc.TrackRequest("Hi!", DateTimeOffset.UtcNow, TimeSpan.FromHours(1), "200", true);
            _tc.Flush();
        }
    }
}
