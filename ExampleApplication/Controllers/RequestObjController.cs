using ExampleApplication.Utility;
using Microsoft.AspNetCore.Mvc;
using MyApp.DataAccess.Abstractions.Dto;
using MyApp.Domain.MyDomain.Model;

namespace ExampleApplication.Controllers
{

    [ApiController]
    [Route("api/requestObj")]
    public class RequestObjController : ControllerBase
    {
        private readonly ResponseDto _response;

        public RequestObjController()
        {
            _response = new ResponseDto();
        }

        [HttpPost("FindSecondLargest")]
        public ResponseDto? FindSecondLargest([FromBody] RequestObj request)
        {
            try
            {
                if (request is null || !request.RequestArrayObj.Any())
                {
                    _response.Message = "array was empty";
                }
                else 
                {
                        var secondLargestNumber = Helper.FindSecondLargest(request.RequestArrayObj);
                        _response.Result = secondLargestNumber;
                        _response.IsSuccess = true;
                };
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSuccess = false;
            }
            return _response;
        }
    }
}
