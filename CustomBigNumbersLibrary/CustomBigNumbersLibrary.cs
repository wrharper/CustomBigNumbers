using System;

namespace CustomBigNumbersLibrary
{
    public partial struct CustomBigNumbersLibrary
    {
        public float Base { get; private set; }
        public int Exponent { get; private set; }
        public int SecondExponent { get; private set; }

        public static readonly CustomBigNumbersLibrary MaxValue = new(9.99f, 999, 9999999);

        public CustomBigNumbersLibrary(float baseValue, int exponent, int secondExponent = 0)
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

            if (Exponent >= 1000)
            {
                SecondExponent += Exponent / 1000;
                Exponent %= 1000;
            }

            if (Exponent < 0 && SecondExponent > 0)
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
