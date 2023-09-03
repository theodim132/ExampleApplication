using ExampleApplication.Models;
using ExampleApplication.Utility;
using Microsoft.AspNetCore.Mvc;

namespace ExampleApplication.Controllers
{
    [ApiController]
    [Route("api/requestObj")]
    public class RequestObjController : ControllerBase
    {

        [HttpPost("FindSecondLargest")]
        public  ActionResult<int> FindSecondLargest([FromBody] RequestObj request)
        {
            if(request is null) return BadRequest(string.Empty);

            var secondLargestNumber = Helper.FindSecondLargest(request.RequestArrayObj);

            return Ok(secondLargestNumber);
        }
    }
}
