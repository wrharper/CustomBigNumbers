using System;

namespace CustomBigNumbersLibrary
{
    public partial struct CustomBigNumbersLibrary
    {
        private static bool debug = false;

        public static void SetDebugMode(bool enable)
        {
            debug = enable;
        }

        // Overloaded operator for addition with debugging
        public static CustomBigNumbersLibrary operator +(CustomBigNumbersLibrary a, CustomBigNumbersLibrary b)
        {
            if (debug) Console.WriteLine($"Adding: {a} + {b}");

            float newBase = a.Base + b.Base;
            int newExponent = a.Exponent + b.Exponent;
            int newSecondExponent = a.SecondExponent + b.SecondExponent;

            CustomBigNumbersLibrary result = new(newBase, newExponent, newSecondExponent);
            if (debug) Console.WriteLine($"Before Normalize: {result}");
            result.Normalize();
            if (debug) Console.WriteLine($"After Normalize: {result}");
            return result;
        }

        // Overloaded operator for subtraction with debugging
        public static CustomBigNumbersLibrary operator -(CustomBigNumbersLibrary a, CustomBigNumbersLibrary b)
        {
            if (debug) Console.WriteLine($"Subtracting: {a} - {b}");

            float newBase = a.Base - b.Base;
            int newExponent = a.Exponent - b.Exponent;
            int newSecondExponent = a.SecondExponent - b.SecondExponent;

            if (newBase < 0)
            {
                return new CustomBigNumbersLibrary(0, 0);
            }

            CustomBigNumbersLibrary result = new(newBase, newExponent, newSecondExponent);
            if (debug) Console.WriteLine($"Before Normalize: {result}");
            result.Normalize();
            if (debug) Console.WriteLine($"After Normalize: {result}");
            return result;
        }

        // Overloaded operator for multiplication with debugging
        public static CustomBigNumbersLibrary operator *(CustomBigNumbersLibrary a, CustomBigNumbersLibrary b)
        {
            if (debug) Console.WriteLine($"Multiplying: {a} * {b}");

            float newBase = a.Base * b.Base;
            int newExponent = a.Exponent + b.Exponent;
            int newSecondExponent = a.SecondExponent + b.SecondExponent;

            CustomBigNumbersLibrary result = new(newBase, newExponent, newSecondExponent);
            if (debug) Console.WriteLine($"Before Normalize: {result}");
            result.Normalize();
            if (debug) Console.WriteLine($"After Normalize: {result}");
            return result;
        }

        // Overloaded operator for scalar multiplication with debugging
        public static CustomBigNumbersLibrary operator *(CustomBigNumbersLibrary a, float scalar)
        {
            if (debug) Console.WriteLine($"Multiplying: {a} * {scalar}");

            float newBase = a.Base * scalar;
            int newExponent = a.Exponent;
            int newSecondExponent = a.SecondExponent;

            CustomBigNumbersLibrary result = new(newBase, newExponent, newSecondExponent);
            if (debug) Console.WriteLine($"Before Normalize: {result}");
            result.Normalize();
            if (debug) Console.WriteLine($"After Normalize: {result}");
            return result;
        }

        // Overloaded operator for division with debugging
        public static CustomBigNumbersLibrary operator /(CustomBigNumbersLibrary a, CustomBigNumbersLibrary b)
        {
            if (debug) Console.WriteLine($"Dividing: {a} / {b}");

            if (b.Base == 0) return a;

            float newBase = a.Base / b.Base;
            int newExponent = a.Exponent - b.Exponent;
            int newSecondExponent = a.SecondExponent - b.SecondExponent;

            if (newBase < 0)
            {
                return new CustomBigNumbersLibrary(0, 0);
            }

            CustomBigNumbersLibrary result = new(newBase, newExponent, newSecondExponent);
            if (debug) Console.WriteLine($"Before Normalize: {result}");
            result.Normalize();
            if (debug) Console.WriteLine($"After Normalize: {result}");
            return result;
        }

        // Overloaded operator for scalar division with debugging
        public static CustomBigNumbersLibrary operator /(CustomBigNumbersLibrary a, float scalar)
        {
            if (debug) Console.WriteLine($"Dividing: {a} / {scalar}");

            if (scalar == 0) return a;

            float newBase = a.Base / scalar;
            int newExponent = a.Exponent;
            int newSecondExponent = a.SecondExponent;

            if (newBase < 0)
            {
                return new CustomBigNumbersLibrary(0, 0);
            }

            CustomBigNumbersLibrary result = new(newBase, newExponent, newSecondExponent);
            if (debug) Console.WriteLine($"Before Normalize: {result}");
            result.Normalize();
            if (debug) Console.WriteLine($"After Normalize: {result}");
            return result;
        }
    }
}
