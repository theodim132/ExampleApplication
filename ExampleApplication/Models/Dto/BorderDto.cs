using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExampleApplication.Models.Dto
{
    public class BorderDto
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public int CountryId {  get; set; }

        [ForeignKey("CountryId")]
        public virtual Country Country { get; set; }
    }
}
