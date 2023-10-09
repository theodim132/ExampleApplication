using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MyApp.DataAccess.Abstractions.MyDomain.Entities
{
    public class Border
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public int CountryId { get; set; }

        [ForeignKey("CountryId")]
        [JsonIgnore]

        public virtual Country Country { get; set; }
    }
}
