using Newtonsoft.Json;
using System;
using System.Linq;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using MeuDesenho.Models;
using System.Collections.Generic;
using Windows.Storage.Streams;
using MeuDesenho.Infra;

namespace MeuDesenho.Services
{
    internal class CustomVisionOnLine
    {
        internal async Task<IEnumerable<Tag>> Predict(IRandomAccessStream randomAccessStream)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var image = randomAccessStream.AsStream();
                    var content = new StreamContent(image);
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                    var multiPartContent = new MultipartFormDataContent
                {
                    { content, "imageData" }
                };
                    client.DefaultRequestHeaders.TryAddWithoutValidation("Prediction-Key", Parameters.CustomVisionOnlinePredictionKey);
                    client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/octet-stream");
                    var response = await client.PostAsync(Parameters.CustomVisionOnlineEndpoint, multiPartContent).ConfigureAwait(false);
                    var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var result = JsonConvert.DeserializeObject<OnLineResult>(json);
                    return this.ConvertToResult(result);
                }
            }
            catch
            {
                return null;
            }
        }

        public IEnumerable<Tag> ConvertToResult(OnLineResult result)
            => result.Predictions
                     .OrderByDescending(p => p.Probability)
                     .Select(t => new Tag(t.TagName, t.Probability));
    }
}
