namespace MyApp.DataAccess.Abstractions.CountryApi
{
    public class CountryContract
    {
        public CountryName Name { get; set; }
        public List<string> Borders { get; set; }
        public List<string> Capital { get; set; }

        public class CountryName
        {
            public string Common { get; set; }
            public string Official { get; set; }
            public NativeName NativeName { get; set; }
           
        }
        public class NativeName
        {
            public NativeNameSpa Spa { get; set; }

            public class NativeNameSpa
            {
                public string Common { get; set; }
                public string Official { get; set; }
            }
        }
    }
}
