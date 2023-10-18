namespace ExampleApplication.Utility
{
    public static class Helper
    {

        public static int FindSecondLargest(IEnumerable<int> array)
        {
            var sortedArray = array?.Distinct().OrderByDescending(u => u).ToList();

            return sortedArray?.Count >= 2 ? sortedArray[1] : -1;
        }
    }
}
