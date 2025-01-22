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

        public float Base { get; private set; }
        public int Exponent { get; private set; }
        public double SecondExponent { get; private set; }

        public static readonly CustomBigNumbersLibrary MaxValue = new(9.99f, 999, double.MaxValue);

        public CustomBigNumbersLibrary(float baseValue, int exponent, double secondExponent = 0)
        {
            Base = Math.Max(baseValue, 0f);
            Exponent = Math.Max(exponent, 0);
            SecondExponent = Math.Max(secondExponent, 0);

            Normalize();
        }

        private void Normalize()
        {
            if (Base == 0)
            {
                Base = 0;
                Exponent = 0;
                SecondExponent = 0;
                return;
            }

            // Enhanced Debugging
            if (debug) Console.WriteLine($"Normalizing: Base={Base}, Exponent={Exponent}, SecondExponent={SecondExponent}");

            // Normalize Base and Exponent
            while (Base >= 10)
            {
                Base /= 10;
                Exponent += 1;
            }

            while (Base > 0 && Base < 1)
            {
                Base *= 10;
                Exponent -= 1;
            }

            // Handle Exponent and SecondExponent correctly
            if (Exponent >= 1000)
            {
                SecondExponent += Exponent / 1000.0;
                Exponent %= 1000;
            }

            while (Exponent < 0 && SecondExponent > 0)
            {
                int shift = (-Exponent / 1000) + 1;
                Exponent += shift * 1000;
                SecondExponent -= shift;
            }

            if (SecondExponent < 0)
            {
                Base = 0;
                Exponent = 0;
                SecondExponent = 0;
            }

            // Handle very large second exponents without resetting
            if (SecondExponent >= double.MaxValue)
            {
                Base = 9.99f;
                Exponent = 999;
                SecondExponent = double.MaxValue;
                if (debug) Console.WriteLine("Reached max second exponent value, setting all values to max.");
            }

            // Enhanced Debugging
            if (debug) Console.WriteLine($"Normalized: Base={Base}, Exponent={Exponent}, SecondExponent={SecondExponent}");
        }

        private static float NormalizeBase(float baseValue, int exponentDiff)
        {
            return baseValue / (float)Math.Pow(10, exponentDiff);
        }

        public static CustomBigNumbersLibrary FromPercentage(float percentage)
        {
            float baseValue = percentage / 100f;
            return new CustomBigNumbersLibrary(baseValue, 0);
        }

        public override readonly bool Equals(object obj)
        {
            if (obj == null || obj is not CustomBigNumbersLibrary other)
            {
                return false;
            }

            return Base == other.Base && Exponent == other.Exponent && SecondExponent == other.SecondExponent;
        }

        public override readonly int GetHashCode()
        {
            return HashCode.Combine(Base, Exponent, SecondExponent);
        }

        public override readonly string ToString()
        {
            string baseString;
            if (Base % 1 == 0) // Check if it's an integer
            {
                baseString = Base.ToString("0"); // No decimal places
            }
            else
            {
                baseString = Base.ToString("0.##"); // Up to two decimal places, suppressing trailing zeros
            }

            string secondExponentString = SecondExponent.ToString("E0"); // Convert to scientific notation

            return $"{baseString}e{Exponent}B{secondExponentString}";
        }

        public static implicit operator string(CustomBigNumbersLibrary v)
        {
            return v.ToString();
        }
    }
}
