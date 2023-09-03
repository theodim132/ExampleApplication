using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ExampleApplication.Models
{
    public class Border
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public int CountryId {  get; set; }

        [ForeignKey("CountryId")]
        [JsonIgnore]

        public virtual Country Country { get; set; }
    }
}
