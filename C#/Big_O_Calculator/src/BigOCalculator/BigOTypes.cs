using System;
using System.Collections.Generic;

namespace BigOCalculator
{
    public abstract class BigOType
    {
        public string form { get; set; }

        public BigOType()
        {
            form = "O()";
        }

        public virtual decimal Function(decimal x)
        {
            return x;
        }

        public virtual decimal[] MakeLine(List<(int, int)> points)
        {
            decimal[] output = new decimal[points.Count];

            for (int i = 0; i < output.Length; i++)
            {
                output[i] = Function(points[i].Item1);
            }

            return output;
        }

        public virtual decimal GetAverageDistance(List<(int, int)> points)
        {
            decimal average = 0;
            decimal[] line = MakeLine(points);

            for (int i = 0; i < line.Length; i++)
            {
                average += Math.Abs(points[i].Item2 - line[i]);
            }

            average = average / line.Length;

            return average;
        }
    }

    public class BigOLinear : BigOType
    {
        public BigOLinear()
        {
            form = "O(n)";
        }
    }

    public class BigOConstant : BigOType
    {
        public BigOConstant()
        {
            form = "O(1)";
        }

        public override decimal Function(decimal x)
        {
            return 1.0m;
        }
    }

    public class BigOLog : BigOType
    {
        public BigOLog()
        {
            form = "O(log n)";
        }

        public override decimal Function(decimal x)
        {
            return (decimal)Math.Log10((double)x);
        }
    }

    public class BigONLog : BigOType
    {
        public BigONLog()
        {
            form = "O(n log n)";
        }

        public override decimal Function(decimal x)
        {
            return x * (decimal)(Math.Log10((double)x));
        }
    }

    public class BigONSquared : BigOType
    {
        public BigONSquared()
        {
            form = "O(n^2)";
        }

        public override decimal Function(decimal x)
        {
            return (decimal) Math.Pow((double)x, 2);
        }
    }

    public class BigOExponential : BigOType
    {
        public BigOExponential()
        {
            form = "O(2^n)";
        }

        public override decimal Function(decimal x)
        {
            decimal res;
            
            try
            {
                res = (decimal) Math.Pow(2, (double)x);
            }
            catch
            {
                res = Decimal.MaxValue;
            }

            return res;
        }

        public override decimal GetAverageDistance(List<(int, int)> points)
        {
            decimal average = 0;
            decimal[] line = MakeLine(points);

            for (int i = 0; i < line.Length; i++)
            {
                decimal dif = Math.Abs(points[i].Item2 - line[i]);
                try
                {
                    average += dif;
                }
                catch
                {
                    average = Decimal.MaxValue;
                    break;
                }
            }

            average = average/line.Length;
            return average;
        }
    }

    public class BigOFactorial : BigOType
    {
        public BigOFactorial()
        {
            form = "O(n!)";
        }

        public override decimal Function(decimal x)
        {
            decimal res = 1;

            for (decimal i = x; i > 0; i--)
            {
                try
                {
                    res *= i;
                }
                catch
                {
                    res = Decimal.MaxValue;
                    break;
                }
            }

            return res;
        }

        public override decimal GetAverageDistance(List<(int, int)> points)
        {
            decimal average = 0;
            decimal[] line = MakeLine(points);

            for (int i = 0; i < line.Length; i++)
            {
                decimal dif = Math.Abs(points[i].Item2 - line[i]);
                try
                {
                    average += dif;
                }
                catch
                {
                    average = Decimal.MaxValue;
                    break;
                }
            }

            average = average / line.Length;
            return average;
        }
    }
}
