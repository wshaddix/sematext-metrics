using Sematext;
using Sematext.Exceptions;
using Xunit;

namespace Tests
{
    public class NegativeTestCases : TestCaseBase
    {
        [Fact]
        public void Cannot_Create_A_Metrics_Client_With_A_Blank_App_Key()
        {
            var ex = Assert.Throws<SematextValidationException>(() =>
                                                                {
                                                                    var client = new MetricsClient("  ");
                                                                });

            Assert.Equal("You must provide a valid app token.", ex.Message);
        }

        [Fact]
        public void Cannot_Create_A_Metrics_Client_With_A_Null_App_Key()
        {
            var ex = Assert.Throws<SematextValidationException>(() =>
                                                                {
                                                                    var client = new MetricsClient(null);
                                                                });

            Assert.Equal("You must provide a valid app token.", ex.Message);
        }

        [Fact]
        public void Cannot_Send_A_Metric_Whose_Filter1_Name_Is_Longer_Than_255_Characters()
        {
            var invalidFilterName = new string('x', 256);
            var metric = GenerateTestMetric();

            var ex = Assert.Throws<SematextValidationException>(() =>
                {
                    metric.SetFilter1(invalidFilterName);
                });

            Assert.Equal("filter1 must be between 1 and 255 characters.", ex.Message);
        }

        [Fact]
        public void Cannot_Send_A_Metric_Whose_Filter2_Name_Is_Longer_Than_255_Characters()
        {
            var invalidFilterName = new string('x', 256);
            var metric = GenerateTestMetric();

            var ex = Assert.Throws<SematextValidationException>(() =>
                                                                {
                                                                    metric.SetFilter2(invalidFilterName);
                                                                });

            Assert.Equal("filter2 must be between 1 and 255 characters.", ex.Message);
        }

        [Fact]
        public void Cannot_Send_A_Metric_Whose_Name_Is_Blank()
        {
            var metric = GenerateTestMetric();

            var ex = Assert.Throws<SematextValidationException>(() =>
                {
                    metric.SetName(string.Empty);
                });

            Assert.Equal("name must be between 1 and 255 characters.", ex.Message);
        }

        [Fact]
        public void Cannot_Send_A_Metric_Whose_Name_Is_Longer_Than_255_Characters()
        {
            var invalidMetricName = new string('x', 256);
            var metric = GenerateTestMetric();

            var ex = Assert.Throws<SematextValidationException>(() =>
                {
                    metric.SetName(invalidMetricName);
                });

            Assert.Equal("name must be between 1 and 255 characters.", ex.Message);
        }

        [Fact]
        public void Cannot_Send_A_Metric_Whose_Name_Is_Null()
        {
            var metric = GenerateTestMetric();

            var ex = Assert.Throws<SematextValidationException>(() =>
                {
                    metric.SetName(null);
                });

            Assert.Equal("name must be between 1 and 255 characters.", ex.Message);
        }

        [Fact]
        public void Cannot_Send_A_Metric_Whose_Name_Is_White_Space()
        {
            var metric = GenerateTestMetric();

            var ex = Assert.Throws<SematextValidationException>(() =>
                {
                    metric.SetName("    ");
                });

            Assert.Equal("name must be between 1 and 255 characters.", ex.Message);
        }
    }
}