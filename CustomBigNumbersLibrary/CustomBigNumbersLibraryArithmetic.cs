using System;

namespace CustomBigNumbersLibrary
{
    public partial struct CustomBigNumbersLibrary
    {
        // Overloaded operator for addition with debugging
        public static CustomBigNumbersLibrary operator +(CustomBigNumbersLibrary a, CustomBigNumbersLibrary b)
        {
            if (a.SecondExponent >= double.MaxValue || b.SecondExponent >= double.MaxValue)
            {
                if (debug) Console.WriteLine("Addition skipped: one of the values has reached the max second exponent.");
                return a.SecondExponent >= double.MaxValue ? a : b;
            }

            if (debug) Console.WriteLine($"Adding: {a} + {b}");

            float newBase = a.Base + b.Base;
            int newExponent = a.Exponent + b.Exponent;
            double newSecondExponent = a.SecondExponent + b.SecondExponent;

            CustomBigNumbersLibrary result = new CustomBigNumbersLibrary(newBase, newExponent, newSecondExponent);
            if (debug) Console.WriteLine($"Before Normalize: {result}");
            result.Normalize();
            if (debug) Console.WriteLine($"After Normalize: {result}");
            return result;
        }

        // Overloaded operator for subtraction with debugging
        public static CustomBigNumbersLibrary operator -(CustomBigNumbersLibrary a, CustomBigNumbersLibrary b)
        {
            if (a.SecondExponent >= double.MaxValue || b.SecondExponent >= double.MaxValue)
            {
                if (debug) Console.WriteLine("Subtraction: one of the values has reached the max second exponent.");
            }

            if (debug) Console.WriteLine($"Subtracting: {a} - {b}");

            float newBase = a.Base - b.Base;
            int newExponent = a.Exponent - b.Exponent;
            double newSecondExponent = a.SecondExponent - b.SecondExponent;

            if (newBase < 0)
            {
                return new CustomBigNumbersLibrary(0, 0);
            }

            CustomBigNumbersLibrary result = new CustomBigNumbersLibrary(newBase, newExponent, newSecondExponent);
            if (debug) Console.WriteLine($"Before Normalize: {result}");
            result.Normalize();
            if (debug) Console.WriteLine($"After Normalize: {result}");
            return result;
        }

        // Overloaded operator for multiplication with debugging
        public static CustomBigNumbersLibrary operator *(CustomBigNumbersLibrary a, CustomBigNumbersLibrary b)
        {
            if (a.SecondExponent >= double.MaxValue || b.SecondExponent >= double.MaxValue)
            {
                if (debug) Console.WriteLine("Multiplication skipped: one of the values has reached the max second exponent.");
                return a.SecondExponent >= double.MaxValue ? a : b;
            }

            if (debug) Console.WriteLine($"Multiplying: {a} * {b}");

            float newBase = a.Base * b.Base;
            int newExponent = a.Exponent + b.Exponent;
            double newSecondExponent = a.SecondExponent + b.SecondExponent;

            CustomBigNumbersLibrary result = new CustomBigNumbersLibrary(newBase, newExponent, newSecondExponent);
            if (debug) Console.WriteLine($"Before Normalize: {result}");
            result.Normalize();
            if (debug) Console.WriteLine($"After Normalize: {result}");
            return result;
        }

        // Overloaded operator for scalar multiplication with debugging
        public static CustomBigNumbersLibrary operator *(CustomBigNumbersLibrary a, float scalar)
        {
            if (a.SecondExponent >= double.MaxValue)
            {
                if (debug) Console.WriteLine("Scalar multiplication skipped: value has reached the max second exponent.");
                return a;
            }

            if (debug) Console.WriteLine($"Multiplying: {a} * {scalar}");

            float newBase = a.Base * scalar;
            int newExponent = a.Exponent;
            double newSecondExponent = a.SecondExponent;

            CustomBigNumbersLibrary result = new CustomBigNumbersLibrary(newBase, newExponent, newSecondExponent);
            if (debug) Console.WriteLine($"Before Normalize: {result}");
            result.Normalize();
            if (debug) Console.WriteLine($"After Normalize: {result}");
            return result;
        }

        // Overloaded operator for division with debugging
        public static CustomBigNumbersLibrary operator /(CustomBigNumbersLibrary a, CustomBigNumbersLibrary b)
        {
            if (a.SecondExponent >= double.MaxValue || b.SecondExponent >= double.MaxValue)
            {
                if (debug) Console.WriteLine("Division: one of the values has reached the max second exponent.");
            }

            if (debug) Console.WriteLine($"Dividing: {a} / {b}");

            if (b.Base == 0) return a;

            float newBase = a.Base / b.Base;
            int newExponent = a.Exponent - b.Exponent;
            double newSecondExponent = a.SecondExponent - b.SecondExponent;

            if (newBase < 0)
            {
                return new CustomBigNumbersLibrary(0, 0);
            }

            CustomBigNumbersLibrary result = new CustomBigNumbersLibrary(newBase, newExponent, newSecondExponent);
            if (debug) Console.WriteLine($"Before Normalize: {result}");
            result.Normalize();
            if (debug) Console.WriteLine($"After Normalize: {result}");
            return result;
        }

        // Overloaded operator for scalar division with debugging
        public static CustomBigNumbersLibrary operator /(CustomBigNumbersLibrary a, float scalar)
        {
            if (a.SecondExponent >= double.MaxValue)
            {
                if (debug) Console.WriteLine("Scalar division: value has reached the max second exponent.");
            }

            if (debug) Console.WriteLine($"Dividing: {a} / {scalar}");

            if (scalar == 0) return a;

            float newBase = a.Base / scalar;
            int newExponent = a.Exponent;
            double newSecondExponent = a.SecondExponent;

            if (newBase < 0)
            {
                return new CustomBigNumbersLibrary(0, 0);
            }

            CustomBigNumbersLibrary result = new CustomBigNumbersLibrary(newBase, newExponent, newSecondExponent);
            if (debug) Console.WriteLine($"Before Normalize: {result}");
            result.Normalize();
            if (debug) Console.WriteLine($"After Normalize: {result}");
            return result;
        }
    }
}
