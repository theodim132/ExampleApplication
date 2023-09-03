using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ExampleApplication.Models
{
    public class Country
    {
        [Key]
        public int Id { get; set; }
        public Name Name { get; set; }

        public List<string> Capital { get; set; }

        public List<string> Borders { get; set; }

    }

    public class Name 
    {
        public int id { get; set; }
        public string common {  get; set; }
    }
   
}
