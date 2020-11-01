namespace api.layer
{
    using Newtonsoft.Json;

    public partial class PullRequestSonarDetails
    {
        [JsonProperty("component")]
        public Component Component { get; set; }
    }

    public partial class Component
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("qualifier")]
        public string Qualifier { get; set; }

        [JsonProperty("measures")]
        public Measure[] Measures { get; set; }
    }

    public partial class Measure
    {
        [JsonProperty("metric")]
        public string Metric { get; set; }

        [JsonProperty("periods")]
        public Period[] Periods { get; set; }
    }

    public partial class Period
    {
        [JsonProperty("index")]
        public long Index { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("bestValue")]
        public bool BestValue { get; set; }
    }
}
