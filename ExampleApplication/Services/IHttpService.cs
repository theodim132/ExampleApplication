using ExampleApplication.Models.Dto;

namespace ExampleApplication.Services
{
    public interface IHttpService
    {
        Task<ResponseDto?> SendAsync(RequestDto requestDto);
    }
}
