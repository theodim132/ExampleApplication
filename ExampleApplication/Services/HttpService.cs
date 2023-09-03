using ExampleApplication.Models.Dto;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace ExampleApplication.Services
{
    public class HttpService : IHttpService
    {

        private readonly IHttpClientFactory _httpClientFactory;

        public HttpService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ResponseDto?> SendAsync(RequestDto requestDto)
        {
            try
            {
                HttpClient httpClient =  _httpClientFactory.CreateClient();
                HttpRequestMessage message = new();
                message.Headers.Add("Accept", "application/json");
                message.RequestUri = new Uri(requestDto.Url);

                HttpResponseMessage? apiResponse = null;
                message.Method = HttpMethod.Get;

                apiResponse = await httpClient.SendAsync(message);

                switch (apiResponse.StatusCode)
                {
                    case HttpStatusCode.NotFound:
                        return new() { IsSuccess = false, Message = "Not Found" };
                    case HttpStatusCode.Forbidden:
                        return new() { IsSuccess = false, Message = "Access Denied" };
                    case HttpStatusCode.Unauthorized:
                        return new() { IsSuccess = false, Message = "Unauthorized" };
                    case HttpStatusCode.InternalServerError:
                        return new() { IsSuccess = false, Message = "Internal Server Error" };
                    case HttpStatusCode.BadRequest:
                        return new() { IsSuccess = false, Message = "Bad Request" };
                    case HttpStatusCode.OK:
                        var apiContent = await apiResponse.Content.ReadAsStringAsync();
                        //var apiResponseDto = JsonConvert.DeserializeObject<ResponseDto>(apiContent);
                        JToken apiContentToken = JToken.Parse(apiContent);
                        var apiResponseDto = new ResponseDto {IsSuccess = true, Result = apiContentToken };

                        return apiResponseDto;
                    default:
                        return new() { IsSuccess = false, Message = "Something went wrong" };
                }

            }
            catch (Exception ex) 
            {
                var dto = new ResponseDto
                {
                    Message = ex.Message.ToString(),
                    IsSuccess = false,
                };
                return dto;
            }
        }
    }
}
