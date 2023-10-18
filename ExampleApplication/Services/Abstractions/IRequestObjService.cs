using Viva;

namespace Example.App.Services.Abstractions
{
    public interface IRequestObjService
    {
        Task<IResult<int>> FindSecondLargestAsync(IEnumerable<int> array);
    }
}
