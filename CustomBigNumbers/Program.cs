using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

// AMD 9950x test results:
// Main Thread Time: 40123 ms
// Async 100% CPU Processing Time: 3541 ms

namespace CustomBigNumbersLibrary
{
    class Program
    {
        static async Task Main()
        {
            CustomBigNumbersLibrary.SetDebugMode(false);
            // The 100% CPU TRUE Multithread test
            /*using CancellationTokenSource cts = new();
            var arithmeticTasks = CreateArithmeticTasks(cts.Token);
            var displayTask = DisplayIncrementingNumberAsync(cts.Token);

            await Task.WhenAll(arithmeticTasks);
            await displayTask;*/

            // Main Thread only
            await DisplayIncrementingNumberAsync();

            // Compares Main Thread vs Async 100% CPU Test.
            // await HeavyAsyncTest();
        }

        private static Task[] CreateArithmeticTasks(CancellationToken token)
        {
            int processorCount = Environment.ProcessorCount;
            Task[] tasks = new Task[processorCount];

            for (int i = 0; i < processorCount; i++)
            {
                tasks[i] = Task.Run(() => PerformArithmeticOperations(token), token);
            }

            return tasks;
        }

        private static async Task PerformArithmeticOperations(CancellationToken token)
        {
            CustomBigNumbersLibrary number = new(1, 0);
            CustomBigNumbersLibrary[] numbers = { number };

            while (!token.IsCancellationRequested)
            {
                await CustomBigNumbersLibraryThreading.ProcessArrayInParallelWithProgress(
                    numbers,
                    n => HeavyComputation(n),
                    _ => { } // No display callback for arithmetic tasks
                );

                number = numbers[0];
            }
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

        private static async Task DisplayIncrementingNumberAsync()
        {
            CustomBigNumbersLibrary number = new(1, 0);
            CustomBigNumbersLibrary addition = new(1, 1, double.MaxValue/100);

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
            CustomBigNumbersLibrary[] numbers = { number };

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
                    n => HeavyComputation(n),
                    progressCallback
                );

                number = numbers[0];
                Console.WriteLine($"Processed number: {number}");

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
