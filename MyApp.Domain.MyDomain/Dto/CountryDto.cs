using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace MyApp.Domain.MyDomain.Dto
{
    public class CountryDto
    {

        [JsonProperty("name")]
        public NameDto Name { get; set; }

        [JsonProperty("capital")]
        public List<string> Capital { get; set; }

        [JsonProperty("borders")]
        public List<string> Borders { get; set; }

    }
    public class NameDto
    {
        [JsonProperty("common")]
        public string Common { get; set; }
    }
}
