using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace MyApp.Domain.MyDomain.Model
{
    public class Country
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public string Capital { get; set; }

        public List<Border> Borders { get; set; }

    }

    public class Name 
    {
        public int id { get; set; }
        public string common {  get; set; }
    }
   
}
