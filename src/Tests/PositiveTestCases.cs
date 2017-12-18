using Sematext;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Tests
{
    public class PositiveTestCases : TestCaseBase
    {
        [Fact]
        public async Task Can_Handle_A_429_Http_Throttle_Response()
        {
        }

        [Fact]
        public async Task Can_Send_A_Group_Of_Metrics()
        {
            var metrics = new[]
                          {
                              GenerateTestMetric(),
                              GenerateTestMetric(),
                              GenerateTestMetric()
                          };

            await MetricsClient.SendAsync(metrics).ConfigureAwait(false);
        }

        [Fact]
        public async Task Can_Send_A_Metric_With_No_Filter1()
        {
            var metric = GenerateTestMetric();
            metric.SetFilter1(null);
            await MetricsClient.SendAsync(metric).ConfigureAwait(false);
        }

        [Fact]
        public async Task Can_Send_A_Metric_With_No_Filter2()
        {
            var metric = GenerateTestMetric();
            metric.SetFilter2(null);
            await MetricsClient.SendAsync(metric).ConfigureAwait(false);
        }

        [Fact]
        public async Task Can_Send_A_Metric_With_No_Filters()
        {
            var metric = GenerateTestMetric();
            metric.SetFilter1(null);
            metric.SetFilter2(null);
            await MetricsClient.SendAsync(metric).ConfigureAwait(false);
        }

        [Fact]
        public async Task Can_Send_A_Single_Metric()
        {
            await MetricsClient.SendAsync(GenerateTestMetric()).ConfigureAwait(false);
        }

        [Fact]
        public async Task Can_Send_More_Than_100_Metrics_At_A_Time()
        {
            var metrics = new List<Metric>();

            for (var i = 0; i < 150; i++)
            {
                metrics.Add(GenerateTestMetric());
            }

            await MetricsClient.SendAsync(metrics.ToArray()).ConfigureAwait(false);
        }
    }
}