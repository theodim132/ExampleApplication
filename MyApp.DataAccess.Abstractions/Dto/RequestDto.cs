using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.DataAccess.Abstractions.Dto
{
    public class RequestDto
    {
        public string ApiType { get; set; } = "GET";

        public string Url { get; set; }

        public string Data { get; set; }
    }
}
