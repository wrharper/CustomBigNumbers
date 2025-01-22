using System;
using System.Threading.Tasks;

namespace CustomBigNumbersLibrary
{
    public static class CustomBigNumbersLibraryThreading
    {
        public static Task<CustomBigNumbersLibrary[]> ProcessArrayInParallel(CustomBigNumbersLibrary[] numbers, Func<CustomBigNumbersLibrary, CustomBigNumbersLibrary> operation)
        {
            return Task.Run(() =>
            {
                CustomBigNumbersLibrary[] results = new CustomBigNumbersLibrary[numbers.Length];

                Parallel.For(0, numbers.Length, i =>
                {
                    results[i] = operation(numbers[i]);
                });

                return results;
            });
        }

        public static Task ProcessArrayInParallelWithProgress(CustomBigNumbersLibrary[] numbers, Func<CustomBigNumbersLibrary, CustomBigNumbersLibrary> operation, Action<CustomBigNumbersLibrary> progressCallback)
        {
            return Task.Run(() =>
            {
                Parallel.For(0, numbers.Length, i =>
                {
                    numbers[i] = operation(numbers[i]);
                    progressCallback(numbers[i]);
                });
            });
        }
    }
}
