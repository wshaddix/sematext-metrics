using Sematext.Exceptions;
using Sematext.Helpers;

namespace Sematext
{
    public class Metric
    {
        public string Aggregation { get; private set; }

        public string Filter1 { get; private set; }

        public string Filter2 { get; private set; }

        public string Name { get; private set; }

        public long Timestamp { get; private set; }

        public double Value { get; private set; }

        /// <summary>
        /// Creates a new Metric
        /// </summary>
        /// <param name="timestamp">Time in milliseconds since Epoch when the Metric was recorded</param>
        /// <param name="name">The name of the Metric</param>
        /// <param name="value">The value of the Metric</param>
        /// <param name="aggregateType">The aggregation type to use for the metric</param>
        /// <param name="filter1">The value for filter1</param>
        /// <param name="filter2">The value for filter2</param>
        public Metric(long timestamp, string name, double value, AggregateTypes aggregateType, string filter1 = null, string filter2 = null)
        {
            this.SetTimestamp(timestamp)
                .SetName(name)
                .SetValue(value)
                .SetAggregation(aggregateType)
                .SetFilter1(filter1)
                .SetFilter2(filter2);
        }

        /// <summary>
        /// Creates a new Metric using the current UTC time as the timestamp
        /// </summary>
        /// <param name="name">The name of the Metric</param>
        /// <param name="value">The value of the Metric</param>
        /// <param name="aggregateType">The aggregation type to use for the metric</param>
        /// <param name="filter1">The value for filter1</param>
        /// <param name="filter2">The value for filter2</param>
        public Metric(string name, double value, AggregateTypes aggregateType, string filter1 = null, string filter2 = null)
        {
            this.SetTimestamp(TimeHelper.EpochFromUtc)
                .SetName(name)
                .SetValue(value)
                .SetAggregation(aggregateType)
                .SetFilter1(filter1)
                .SetFilter2(filter2);
        }

        public Metric SetAggregation(AggregateTypes aggregateType)
        {
            // convert the enum to it's lowercase string representation
            Aggregation = aggregateType.ToString().ToLowerInvariant();
            return this;
        }

        public Metric SetFilter1(string filter1)
        {
            if (string.IsNullOrWhiteSpace(filter1))
            {
                // filter1 is not required, so we'll clear it out
                Filter1 = null;
            }
            else if (filter1.Length > 255)
            {
                throw new SematextValidationException($"{nameof(filter1)} must be between 1 and 255 characters.");
            }
            else
            {
                Filter1 = filter1;
            }

            return this;
        }

        public Metric SetFilter2(string filter2)
        {
            if (string.IsNullOrWhiteSpace(filter2))
            {
                // filter2 is not required, so we'll clear it out
                Filter1 = null;
            }
            else if (filter2.Length > 255)
            {
                throw new SematextValidationException($"{nameof(filter2)} must be between 1 and 255 characters.");
            }
            else
            {
                Filter2 = filter2;
            }

            return this;
        }

        public Metric SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name) || name.Length > 255)
            {
                throw new SematextValidationException($"{nameof(name)} must be between 1 and 255 characters.");
            }

            this.Name = name;
            return this;
        }

        public Metric SetTimestamp(long timestamp)
        {
            this.Timestamp = timestamp;
            return this;
        }

        public Metric SetValue(double value)
        {
            this.Value = value;
            return this;
        }
    }
}