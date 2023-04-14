using Newtonsoft.Json;

namespace Nucleotidz.Consumer.Json
{
    public class Animal
    {
        [JsonRequired]
        [JsonProperty("category")]
        public string category { get; set; }

        [JsonRequired]
        [JsonProperty("name")]
        public string name { get; set; }
    }

    public class AnimalKey
    {
        [JsonRequired]
        [JsonProperty("tagid")]
        public string tagid { get; set; }

    }

}