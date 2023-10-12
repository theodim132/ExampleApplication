using System.ComponentModel.DataAnnotations;

namespace MyApp.DataAccess.Abstractions.MyDomain.Entities
{
    public class Country
    {
        [Key]
        public int Id { get; set; }
        public string CommonName { get; set; } = string.Empty;
        public string OfficialName { get; set; } = string.Empty;
        public string? NativeNameSpaCommon { get; set; }
        public string? NativeNameSpaOfficial { get; set; }
        public string Capital { get; set; }  = string.Empty;
        public List<Border> Borders { get; set; }
    }

}