using System;

namespace CustomBigNumbersLibrary
{
    public partial struct CustomBigNumbersLibrary
    {
        public static CustomBigNumbersLibrary operator +(CustomBigNumbersLibrary a, CustomBigNumbersLibrary b)
        {
            if (a.Base == 0) return b;
            if (b.Base == 0) return a;

            if (Math.Abs(a.Exponent + a.SecondExponent * 1000 - (b.Exponent + b.SecondExponent * 1000)) > 7)
            {
                return (a.Exponent + a.SecondExponent * 1000 > b.Exponent + b.SecondExponent * 1000) ? a : b;
            }

            float resultBase;
            int resultExponent;
            int resultSecondExponent;

            if (a.SecondExponent > b.SecondExponent || (a.SecondExponent == b.SecondExponent && a.Exponent > b.Exponent))
            {
                resultBase = a.Base + NormalizeBase(b.Base, a.Exponent - b.Exponent);
                resultExponent = a.Exponent;
                resultSecondExponent = a.SecondExponent;
            }
            else
            {
                resultBase = NormalizeBase(a.Base, b.Exponent - a.Exponent) + b.Base;
                resultExponent = b.Exponent;
                resultSecondExponent = b.SecondExponent;
            }

            CustomBigNumbersLibrary result = new(resultBase, resultExponent, resultSecondExponent);
            result.Normalize();
            return result;
        }

        public static CustomBigNumbersLibrary operator -(CustomBigNumbersLibrary a, CustomBigNumbersLibrary b)
        {
            if (a.Base == 0) return new CustomBigNumbersLibrary(0, 0);
            if (b.Base == 0) return a;

            if (Math.Abs(a.Exponent + a.SecondExponent * 1000 - (b.Exponent + b.SecondExponent * 1000)) > 7)
            {
                return (a.Exponent + a.SecondExponent * 1000 > b.Exponent + b.SecondExponent * 1000) ? a : new CustomBigNumbersLibrary(0, 0);
            }

            float resultBase;
            int resultExponent;
            int resultSecondExponent;

            if (a.SecondExponent > b.SecondExponent || (a.SecondExponent == b.SecondExponent && a.Exponent > b.Exponent))
            {
                resultBase = a.Base - NormalizeBase(b.Base, a.Exponent - b.Exponent);
                resultExponent = a.Exponent;
                resultSecondExponent = a.SecondExponent;
            }
            else
            {
                resultBase = NormalizeBase(a.Base, b.Exponent - a.Exponent) - b.Base;
                resultExponent = b.Exponent;
                resultSecondExponent = b.SecondExponent;
            }

            if (resultBase < 0)
            {
                return new CustomBigNumbersLibrary(0, 0);
            }

            CustomBigNumbersLibrary result = new(resultBase, resultExponent, resultSecondExponent);
            result.Normalize();
            return result;
        }

        public static CustomBigNumbersLibrary operator *(CustomBigNumbersLibrary a, CustomBigNumbersLibrary b)
        {
            float resultBase = a.Base * b.Base;
            int resultExponent = a.Exponent + b.Exponent;
            int resultSecondExponent = a.SecondExponent + b.SecondExponent;

            CustomBigNumbersLibrary result = new(resultBase, resultExponent, resultSecondExponent);
            result.Normalize();
            return result;
        }

        public static CustomBigNumbersLibrary operator *(CustomBigNumbersLibrary a, float scalar)
        {
            float resultBase = a.Base * scalar;
            int resultExponent = a.Exponent;
            int resultSecondExponent = a.SecondExponent;

            CustomBigNumbersLibrary result = new(resultBase, resultExponent, resultSecondExponent);
            result.Normalize();
            return result;
        }

        public static CustomBigNumbersLibrary operator /(CustomBigNumbersLibrary a, CustomBigNumbersLibrary b)
        {
            if (b.Base == 0) return a;

            float resultBase = a.Base / b.Base;
            int resultExponent = a.Exponent - b.Exponent;
            int resultSecondExponent = a.SecondExponent - b.SecondExponent;

            if (resultBase < 0)
            {
                return new CustomBigNumbersLibrary(0, 0);
            }

            CustomBigNumbersLibrary result = new(resultBase, resultExponent, resultSecondExponent);
            result.Normalize();
            return result;
        }

        public static CustomBigNumbersLibrary operator /(CustomBigNumbersLibrary a, float scalar)
        {
            if (scalar == 0) return a;

            float resultBase = a.Base / scalar;
            int resultExponent = a.Exponent;
            int resultSecondExponent = a.SecondExponent;

            if (resultBase < 0)
            {
                return new CustomBigNumbersLibrary(0, 0);
            }

            CustomBigNumbersLibrary result = new(resultBase, resultExponent, resultSecondExponent);
            result.Normalize();
            return result;
        }
    }
}
