using System;

namespace CustomBigNumbersLibrary
{
    public partial struct CustomBigNumbersLibrary
    {
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

        public readonly int CompareTo(CustomBigNumbersLibrary other)
        {
            if (SecondExponent != other.SecondExponent) return SecondExponent.CompareTo(other.SecondExponent);
            if (Exponent != other.Exponent) return Exponent.CompareTo(other.Exponent);
            return Base.CompareTo(other.Base);
        }

        public static bool operator >(CustomBigNumbersLibrary a, CustomBigNumbersLibrary b) => a.CompareTo(b) > 0;
        public static bool operator <(CustomBigNumbersLibrary a, CustomBigNumbersLibrary b) => a.CompareTo(b) < 0;
        public static bool operator >=(CustomBigNumbersLibrary a, CustomBigNumbersLibrary b) => a.CompareTo(b) >= 0;
        public static bool operator <=(CustomBigNumbersLibrary a, CustomBigNumbersLibrary b) => a.CompareTo(b) <= 0;
        public static bool operator ==(CustomBigNumbersLibrary a, CustomBigNumbersLibrary b) => a.CompareTo(b) == 0;
        public static bool operator !=(CustomBigNumbersLibrary a, CustomBigNumbersLibrary b) => a.CompareTo(b) != 0;

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
