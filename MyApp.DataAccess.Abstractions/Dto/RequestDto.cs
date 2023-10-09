namespace MyApp.DataAccess.Abstractions.Dto
{
    public class RequestDto
    {
        public string ApiType { get; set; } = "GET";

        public string Url { get; set; }

        public string Data { get; set; }
    }
}
