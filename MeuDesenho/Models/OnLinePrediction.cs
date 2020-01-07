using Newtonsoft.Json;

namespace MeuDesenho.Models
{
    public class OnLinePrediction
    {
        [JsonProperty("probability")]
        public float Probability { get; set; }

        [JsonProperty("tagId")]
        public string TagId { get; set; }

        [JsonProperty("tagName")]
        public string TagName { get; set; }
    }
}
