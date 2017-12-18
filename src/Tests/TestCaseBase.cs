using Sematext;
using System;
using Sematext.Helpers;

namespace Tests
{
    public abstract class TestCaseBase
    {
        protected static readonly string AppToken = Environment.GetEnvironmentVariable("SEMATEXT_APP_TOKEN");
        protected readonly MetricsClient MetricsClient = new MetricsClient(AppToken);
        private readonly string[] _filter1Names = new[] { "floor=1", "floor=2", "floor=3", "floor=4" };
        private readonly string[] _filter2Names = new[] { "machine=A", "machine=B", "machine=C", "machine=D" };
        private readonly string[] _metricNames = new[] { "coffee-consumed", "tea-consumed", "juice-consumed", "soda-consumed" };
        private readonly Random _random = new Random();

        protected Metric GenerateTestMetric()
        {
            return new Metric(timestamp: TimeHelper.EpochFromUtc,
                              name: RandomMetricName(),
                              value: RandomMetricValue(),
                              aggregateType: RandomAggregateType(),
                              filter1: RandomFilter1(),
                              filter2: RandomFilter2());
        }

        private AggregateTypes RandomAggregateType()
        {
            var index = RandomNumber(1, 4);
            return (AggregateTypes)index;
        }

        private string RandomFilter1()
        {
            var index = RandomNumber(0, 4);
            return _filter1Names[index];
        }

        private string RandomFilter2()
        {
            var index = RandomNumber(0, 4);
            return _filter2Names[index];
        }

        private string RandomMetricName()
        {
            var index = RandomNumber(0, 4);
            return _metricNames[index];
        }

        private int RandomMetricValue()
        {
            return RandomNumber(1, int.MaxValue);
        }

        private int RandomNumber(int min, int max)
        {
            return _random.Next(min, max);
        }
    }
}