using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MeuDesenho.Models
{
    public class OnLineResult
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("project")]
        public string Project { get; set; }

        [JsonProperty("iteration")]
        public string Iteration { get; set; }

        [JsonProperty("created")]
        public DateTime Created { get; set; }

        [JsonProperty("predictions")]
        public OnLinePrediction[] Predictions { get; set; }
    }
}