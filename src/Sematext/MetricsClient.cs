using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Sematext.Exceptions;
using System;
using System.Collections;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Sematext
{
    public class MetricsClient
    {
        private static readonly HttpClient Client = new HttpClient();
        private static readonly Uri DefaultUrl = new Uri("http://spm-receiver.sematext.com/receiver/custom/receive.json?token=");
        private readonly JsonSerializerSettings _serializerSettings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
        private readonly Uri _url;

        public MetricsClient(string appToken, Uri endpointUrl = null)
        {
            // if the app token is not provided throw an exception
            if (string.IsNullOrWhiteSpace(appToken))
            {
                throw new SematextValidationException("You must provide a valid app token.");
            }

            // if the user does not provide a non-default endpoint url, then use the default
            _url = endpointUrl ?? new Uri($"{DefaultUrl}{appToken}");
        }

        public async Task SendAsync(Metric metric)
        {
            await SendAsync(new[] { metric });
        }

        public async Task SendAsync(Metric[] metrics)
        {
            // we can only send 100 metrics at a time
            var numOfMetrics = metrics.Length;

            if (numOfMetrics > 100)
            {
                var batches = Math.Ceiling(numOfMetrics / 100.0);

                for (var i = 0; i < batches; i++)
                {
                    // pull out 100 metrics
                    var nextBatch = metrics.Skip(100 * i).Take(100);
                    await SendBatch(nextBatch);
                }
            }
            else
            {
                await SendBatch(metrics);
            }
        }

        private async Task SendBatch(IEnumerable metrics)
        {
            var payload = new
            {
                datapoints = metrics
            };

            var result = await Client
                             .PostAsync(_url, new StringContent(JsonConvert.SerializeObject(payload, _serializerSettings), Encoding.UTF8, "application/json"))
                             .ConfigureAwait(false);

            if (!result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStringAsync().ConfigureAwait(false);
                throw new SematextApiException(result.StatusCode, content);
            }
        }
    }
}