using Azure.Core;
using Example.App.Services.Abstractions;
using ExampleApplication.Utility;
using Microsoft.IdentityModel.Tokens;
using MyApp.DataAccess.Abstractions.CountryApi;
using Viva;

namespace Example.App.Services
{
    public class RequestObjService : IRequestObjService
    {
        public async Task<IResult<int>> FindSecondLargestAsync(IEnumerable<int> array)
        {
            if (array.IsNullOrEmpty()) 
            {
                return Result<int>.CreateFailed(ResultCode.NotFound,"Array was empty or null");
            }

            if(array.Count() == 1) 
            {
                return Result<int>.CreateFailed(ResultCode.BadRequest, "The array must have more than one integer");
            }
            int value = await Task.FromResult(Helper.FindSecondLargest(array));
            return Result<int>.CreateSuccessful(value);
        }
    }
}
