using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

// AMD 9950x test results:
// Main Thread Time: 40123 ms
// Async 100% CPU Processing Time: 3541 ms

//Change Test = 0; to what you want to test.
//0 = Async Test
//1 = Main Thread Test
//2 = Main Thread Max Value Test
//3 = Main vs Async Test (will hang, just wait)

namespace CustomBigNumbersLibrary
{
    class Program
    {
        static readonly int Test = 0;
        static async Task Main()
        {
            CustomBigNumbersLibrary.SetDebugMode(false);

            switch(Test)
            {
                case 0:
                    {
                        // Multithread only
                        using CancellationTokenSource cts = new();
                        var displayTask = DisplayIncrementingNumberAsync(cts.Token);

                        await displayTask;
                        break;
                    }
                case 1:
                    // Main Thread only
                    await DisplayIncrementingNumberAsync(false);
                    break;
                case 2:
                    // Main Thread only
                    await DisplayIncrementingNumberAsync(true);
                    break;
                case 3:
                    // Compares Main Thread vs Async 100% CPU Test.
                    await HeavyAsyncTest();
                    break;
            }
        }

        private static CustomBigNumbersLibrary AdditionComputation(CustomBigNumbersLibrary number)
        {
            for (int i = 0; i < 1000; i++)
            {
                number += new CustomBigNumbersLibrary(1, 1, 1);
            }
            return number;
        }

        private static CustomBigNumbersLibrary HeavyComputation(CustomBigNumbersLibrary number)
        {
            // Simulate a computationally expensive operation
            for (int i = 0; i < 1000; i++)
            {
                number *= 1.0001f;
            }
            return number;
        }

        private static async Task DisplayIncrementingNumberAsync(bool MaxValueTest)
        {
            CustomBigNumbersLibrary number = new(1, 0);
            CustomBigNumbersLibrary addition;
            if (MaxValueTest)
                addition = new(1, 1, double.MaxValue / 100);
            else
                addition = new(1, 1, 1);

            while (true)
            {
                Console.Clear();
                Console.WriteLine(number);
                number += addition;
                await Task.Delay(10); // 0.01 seconds delay
            }
        }

        private static async Task DisplayIncrementingNumberAsync(CancellationToken token)
        {
            CustomBigNumbersLibrary number = new(1, 0);
            CustomBigNumbersLibrary[] numbers = [number];

            static void progressCallback(CustomBigNumbersLibrary currentNumber)
            {
                Console.Clear();
                Console.WriteLine(currentNumber);
            }

            while (!token.IsCancellationRequested)
            {
                // Perform a batch of arithmetic operations and display the result
                await CustomBigNumbersLibraryThreading.ProcessArrayInParallelWithProgress(
                    numbers,
                    n => AdditionComputation(n),
                    progressCallback
                );

                number = numbers[0];

                await Task.Delay(10, token); // 0.01 seconds delay
            }
        }

        private static CustomBigNumbersLibrary[] ProcessArrayOnMainThread(CustomBigNumbersLibrary[] numbers, Func<CustomBigNumbersLibrary, CustomBigNumbersLibrary> operation)
        {
            CustomBigNumbersLibrary[] results = new CustomBigNumbersLibrary[numbers.Length];
            for (int i = 0; i < numbers.Length; i++)
            {
                results[i] = operation(numbers[i]);
            }
            return results;
        }

        private static async Task HeavyAsyncTest()
        {
            const int size = 1000000; // Large dataset for testing
            CustomBigNumbersLibrary[] numbers = new CustomBigNumbersLibrary[size];
            for (int i = 0; i < size; i++)
            {
                numbers[i] = new CustomBigNumbersLibrary(i % 10 + 1, i % 100, i % 1000);
            }

            // Measure performance on the main thread
            Stopwatch stopwatch = new();
            stopwatch.Start();
            CustomBigNumbersLibrary[] mainThreadResults = ProcessArrayOnMainThread(numbers, HeavyComputation);
            stopwatch.Stop();
            Console.WriteLine($"Main Thread Time: {stopwatch.ElapsedMilliseconds} ms");

            // Measure performance with async processing
            stopwatch.Restart();
            CustomBigNumbersLibrary[] asyncResults = await CustomBigNumbersLibraryThreading.ProcessArrayInParallel(numbers, HeavyComputation);
            stopwatch.Stop();
            Console.WriteLine($"Async Processing Time: {stopwatch.ElapsedMilliseconds} ms");
        }
    }
}
