using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace BigDecimals
{
    public class BigDecimal : IComparable<BigDecimal>, ICloneable
    {
        private static readonly BigInteger TEN = new BigInteger(10);
        private static readonly BigInteger ONE = new BigInteger(1);
        private static readonly BigInteger ZERO = new BigInteger(0);
        private BigInteger Value { get; set; }
        private int Precision { get; set; }
        private int MaxPrecision { get; set; }

        public BigDecimal(BigDecimal v, int precision, int maxPrecision)
        {
            Value = v.Value;
            Precision = precision;
            MaxPrecision = maxPrecision;
        }

        public BigDecimal(BigDecimal v, int precision) : this(v, precision, 100) { }

        public BigDecimal(BigDecimal v) : this(v, 0) { }

        public BigDecimal(BigInteger v, int precision, int maxPrecision)
        {
            Value = v;
            Precision = precision;
            MaxPrecision = maxPrecision;
        }

        public BigDecimal(BigInteger v) : this(v, 0) { }

        public BigDecimal(BigInteger v, int precision) : this(v, precision, 100) { }

        public BigDecimal(int v) : this(new BigInteger(v), 0) { }

        public BigDecimal(int v, int precision) : this(new BigInteger(v), precision) { }

        public BigDecimal(int v, int precision, int maxPrecision) : this(new BigInteger(v), precision, maxPrecision) { }

        public BigDecimal(string s) : this(s, 100) { }

        public BigDecimal(string s, int maxPrecision)
        {
            int dec;
            if((dec = s.IndexOf('.')) >= 0)
            {
                s = s.Remove(dec, 1);
            }
            else
            {
                dec = s.Length;
            }
            this.Value = BigInteger.Parse(s);
            this.Precision = s.Length - dec;
            this.MaxPrecision = 100;
        }

        public static BigDecimal operator +(BigDecimal left, BigDecimal right)
        {
            return add(left, right, Math.Min(left.MaxPrecision, right.MaxPrecision));
        }

        private static BigDecimal add(BigDecimal left, BigDecimal right, int maxPrecision)
        {
            BigInteger newVal = BigInteger.Pow(TEN, right.Precision) * left.Value + BigInteger.Pow(TEN, left.Precision) * right.Value;
            BigDecimal result = new BigDecimal(newVal, left.Precision + right.Precision, maxPrecision);
            result.Clean();
            return result;
        }

        public static BigDecimal operator -(BigDecimal left, BigDecimal right)
        {
            return sub(left, right, Math.Min(left.MaxPrecision, right.MaxPrecision));
        }

        private static BigDecimal sub(BigDecimal left, BigDecimal right, int maxPrecision)
        {
            BigInteger newVal = BigInteger.Pow(TEN, right.Precision) * left.Value - BigInteger.Pow(TEN, left.Precision) * right.Value;
            BigDecimal result = new BigDecimal(newVal, left.Precision + right.Precision, maxPrecision);
            result.Clean();
            return result;
        }

        public static BigDecimal operator *(BigDecimal left, BigDecimal right)
        {
            return mul(left, right, Math.Min(left.MaxPrecision, right.MaxPrecision));
        }

        private static BigDecimal mul(BigDecimal left, BigDecimal right, int maxPrecision)
        {
            BigDecimal result = new BigDecimal(left.Value * right.Value, left.Precision + right.Precision, maxPrecision);
            result.Clean();
            return result;
        }

        public static BigDecimal operator /(BigDecimal left, BigDecimal right)
        {
            return div(left, right, Math.Min(left.MaxPrecision, right.MaxPrecision));
        }

        private static BigDecimal div(BigDecimal left, BigDecimal right, int maxPrecision)
        {
            BigDecimal result = new BigDecimal(new BigInteger(0), left.Precision - right.Precision, maxPrecision);
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
            if(b.Value > 0)
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
            else if(b.Value == 0)
            {
                return new BigDecimal(ZERO, 0, maxPrecision);
            }
            throw new ArgumentException("Argument to square root is negative.");
            
        }

        public static BigDecimal Sqrt(BigDecimal b)
        {
            return Sqrt(b, b.MaxPrecision);
        }

        private static BigDecimal PowRecur(BigDecimal b, int e, int maxPrecision)
        {
            BigDecimal result;
            if(e == 0)
            {
                return new BigDecimal(ONE, maxPrecision);
            }
            if(e == 1)
            {
                return new BigDecimal(b, maxPrecision + b.Precision, maxPrecision);
            }
            if(e == 2)
            {
                result = mul(b, b, maxPrecision);
                return result;
            }
            if(e % 2 == 1)
            {
                result = mul(b, Pow(b, e - 1, maxPrecision + b.Precision), maxPrecision + b.Precision);
                return result;
            }
            else
            {
                result = Pow(b, e / 2, maxPrecision + b.Precision);
                result = mul(result, result, maxPrecision + b.Precision);
                return result;
            }
        }

        public static BigDecimal Pow(BigDecimal b, int e, int maxPrecision)
        {
            BigDecimal result = PowRecur(b, e, maxPrecision);
            result.Clean(b.Precision);
            return result;
        }

        public static BigDecimal Pow(BigDecimal b, int e)
        {
            BigDecimal result = PowRecur(b, e, b.MaxPrecision);
            result.Clean();
            return result;
        }

        public void Clean(int offset)
        {
            while(Precision > 0 && Value % TEN == 0)
            {
                Value /= TEN;
                Precision--;
            }
            while(Precision > (MaxPrecision - offset))
            {
                Value /= TEN;
                Precision--;
            }
        }

        public void Clean()
        {
            Clean(0);
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
            return new BigDecimal(this);
        }
    }
}
