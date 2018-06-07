using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace BigDecimal
{
    public class BigDecimal : IComparable<BigDecimal>, ICloneable
    {
        private static readonly BigInteger TEN = new BigInteger(10);
        private static readonly BigInteger ZERO = new BigInteger(0);
        private BigInteger Value { get; set; }
        private int Precision { get; set; }
        private int MaxPrecision { get; set; }

        public BigDecimal(BigInteger v, int precision, int maxPrecision)
        {
            Value = v;
            Precision = precision;
            MaxPrecision = maxPrecision;
        }

        public BigDecimal(BigInteger v) : this(v, 0) { }

        public BigDecimal(BigInteger v, int precision) : this(v, precision, 25) { }

        public BigDecimal(int v) : this(new BigInteger(v), 0) { }

        public BigDecimal(int v, int precision) : this(new BigInteger(v), 0) { }

        public BigDecimal(int v, int precision, int maxPrecision) : this(new BigInteger(v), precision, maxPrecision) { }

        public static BigDecimal operator +(BigDecimal left, BigDecimal right)
        {
            BigInteger newVal = BigInteger.Pow(TEN, right.Precision) * left.Value + BigInteger.Pow(TEN, left.Precision) * right.Value;
            BigDecimal result = new BigDecimal(newVal, left.Precision + right.Precision, Math.Min(left.MaxPrecision, right.MaxPrecision));
            result.Clean();
            return result;
        }

        public static BigDecimal operator -(BigDecimal left, BigDecimal right)
        {
            BigInteger newVal = BigInteger.Pow(TEN, right.Precision) * left.Value - BigInteger.Pow(TEN, left.Precision) * right.Value;
            BigDecimal result = new BigDecimal(newVal, left.Precision + right.Precision, Math.Min(left.MaxPrecision, right.MaxPrecision));
            result.Clean();
            return result;
        }

        public static BigDecimal operator *(BigDecimal left, BigDecimal right)
        {
            BigDecimal result = new BigDecimal(left.Value * right.Value, left.Precision + right.Precision, Math.Min(left.MaxPrecision, right.MaxPrecision));
            result.Clean();
            return result;
        }

        public static BigDecimal operator /(BigDecimal left, BigDecimal right)
        {
            BigDecimal result = new BigDecimal(new BigInteger(0), left.Precision - right.Precision, Math.Max(left.MaxPrecision, right.MaxPrecision));
            BigInteger leftVal = left.Value, rightVal = right.Value, division;
            while(result.Precision < result.MaxPrecision && leftVal != ZERO)
            {
                if(leftVal < rightVal)
                {
                    leftVal *= TEN;
                    result.Precision++;
                }
                division = leftVal / rightVal;
                result.Value = (result.Value * TEN) + division;
                leftVal -= division * rightVal;

            }
            result.Clean();
            return result;
        }

        public static BigDecimal Sqrt(BigDecimal b, int maxPrecision)
        {
            string s_b = b.ToString();
            int decimalIndex = s_b.IndexOf('.');
            BigDecimal result = new BigDecimal(0, -(int)Math.Ceiling(decimalIndex / 2.0), maxPrecision);
            if(decimalIndex % 2 == 1)
            {
                s_b = "0" + s_b;
                s_b = s_b.Remove(decimalIndex + 1, 1);
            }
            else
            {
                s_b = s_b.Remove(decimalIndex, 1);
            }
            if((s_b.Length - decimalIndex) % 2 == Convert.ToInt32(s_b[0] != '0'))
            {
                s_b = s_b + "0";
            }

            Queue<int> digitPairs = new Queue<int>();
            for(int i = 0; i < s_b.Length; i += 2)
            {
                digitPairs.Enqueue(int.Parse(s_b.Substring(i, 2)));
            }
            BigInteger remainder = 0, currentValue = 0;
            while(digitPairs.Count >= 0 && result.Precision <= maxPrecision)
            {
                if(remainder == 0 && digitPairs.Count == 0)
                {
                    break;
                }
                if(digitPairs.Count > 0)
                {
                    currentValue = remainder * 100 + digitPairs.Dequeue();
                }
                else
                {
                    currentValue = remainder * 100;
                }

                BigInteger y_base = 20 * result.Value, y = 0;
                int x_max = 0;
                do
                {
                    x_max++;
                    y = x_max * (y_base + x_max);
                } while(y <= currentValue);
                x_max--;
                y = x_max * (y_base + x_max);
                remainder = currentValue - y;
                result.Value = result.Value * 10 + x_max;
                result.Precision++;

            }
            return result;
        }

        public static BigDecimal Sqrt(BigDecimal b)
        {
            if(b.Value > ZERO)
            {
                return Sqrt(b, b.MaxPrecision);
            }
            else if(b.Value == 0)
            {
                return new BigDecimal(ZERO, 0, b.MaxPrecision);
            }
            throw new ArgumentException("Argument to square root is negative.");

        }

        public void Clean()
        {
            while(Precision > 0 && Value % TEN == 0)
            {
                Value /= TEN;
                Precision--;
            }
        }

        public override string ToString()
        {
            string s = Value.ToString();
            while(Precision > s.Length)
            {
                s = "0" + s;
            }
            return s.Insert(s.Length - Precision, ".");
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if(obj is BigDecimal)
            {
                return this.CompareTo((BigDecimal)obj) == 0;
            }
            throw new ArgumentException("Object is not BigDecimal");
        }

        public int CompareTo(BigDecimal other)
        {
            int precisionDifference = this.Precision - other.Precision;
            BigInteger thisV = this.Value, otherV = other.Value;
            while(precisionDifference > 0)
            {
                otherV *= TEN;
                precisionDifference--;
            }
            while(precisionDifference < 0)
            {
                thisV *= TEN;
                precisionDifference++;
            }
            if(thisV > otherV)
            {
                return 1;
            }
            else if(thisV < otherV)
            {
                return -1;
            }
            return 0;
        }

        public object Clone()
        {
            return new BigDecimal(this.Value, this.Precision, this.MaxPrecision);
        }
    }
}
