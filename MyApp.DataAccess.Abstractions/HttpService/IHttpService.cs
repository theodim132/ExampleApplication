
using MyApp.DataAccess.Abstractions.Dto;

namespace MyApp.DataAccess.Abstractions.HttpService
{
    public interface IHttpService
    {
        Task<ResponseDto?> SendAsync(RequestDto requestDto);
    }
}
