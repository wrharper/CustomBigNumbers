namespace CustomBigNumbersLibrary
{
    public partial struct CustomBigNumbersLibrary
    {
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
    }
}
