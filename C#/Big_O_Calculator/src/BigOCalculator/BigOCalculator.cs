using System;
using System.Collections.Generic;
using System.Linq;

namespace BigOCalculator
{
    public static class BigO
    {
        public static Random Random = new Random();

        public static int[] N = new int[] {10, 50, 100, 500, 1000, 5000, 10000};

        public static List<BigOType> types = new List<BigOType>
        {
            new BigOConstant(),
            new BigOLog(),
            new BigOLinear(),
            new BigONLog(),
            new BigONSquared(),
            new BigOExponential(),
            new BigOFactorial()
        };

        public static void CalculateBigO(Algorithm algorithm, int n = 10)
        {
            Dictionary<string, int> counts = new Dictionary<string, int>
            {
                {"O(1)", 0},
                {"O(log n)", 0},
                {"O(n)", 0},
                {"O(n log n)", 0},
                {"O(n^2)", 0},
                {"O(2^n)", 0},
                {"O(n!)", 0}
            };

            if (n < 0)
                n = 0;

            for(int i = 0; i < n; i++)
            {
                BigOType guess = SinglePass(algorithm, i);
                Console.WriteLine();
                counts[guess.form]++;
            }

            PrettyPrintDict(counts.Where(kvp => kvp.Value > 0));

            string form = counts.Aggregate((x,y) => x.Value > y.Value ? x : y).Key;
            Console.WriteLine($"The algorithm has a Big O of approximately {form}");
        }

        public static void PrettyPrintDict(IEnumerable<KeyValuePair<string, int>> counts)
        {
            string text = "Final results of the passes:";
            Console.WriteLine(text);
            PrintLine(text.Length);
            foreach(KeyValuePair<string, int> kvp in counts)
            {
                Console.WriteLine($"{kvp.Key}: {kvp.Value}");
            }
            PrintLine(text.Length);
            Console.WriteLine();
        }

        public static BigOType SinglePass(Algorithm algorithm, int pass)
        {
            Console.WriteLine($"Start running pass {pass + 1}...");

            List<(int, int)> points = GetBigOPoints(algorithm);
            BigOType guess = DoChecks(points);

            Console.WriteLine($"Pass evaluated to {guess.form}");
            PrettyPrintTable(points);

            return guess;
        }

        public static void PrettyPrintTable(List<(int, int)> points)
        {
            int n_count = points[points.Count-1].Item1.ToString().Length;
            int p_count = points[points.Count-1].Item2.ToString().Length;
            if ("OUTPUT".Length > p_count)
                p_count = "OUTPUT".Length;
            int line_length = n_count + p_count + 3;
            
            PrintLine(line_length);
            Console.Write("|");
            FillLine("N", n_count);
            Console.Write("|");
            FillLine("OUTPUT", p_count);
            Console.Write("|\n");
            PrintLine(line_length);

            foreach((int, int) point in points)
            {
                Console.Write("|");
                FillLine(point.Item1.ToString(), n_count);
                Console.Write("|");
                FillLine(point.Item2.ToString(), p_count);
                Console.Write("|\n");
            }

            PrintLine(line_length);
        }

        public static void FillLine(string text, int line_length)
        {
            int l = line_length - text.Length;
            Console.Write(text);
            for (int i = 0; i < l; i++)
            {
                Console.Write(" ");
            }
        }

        public static void PrintLine(int line_length)
        {
            for (int i = 0; i < line_length; i++)
            {
                Console.Write("-");
            }
            Console.Write("\n");
        }

        public static BigOType DoChecks(List<(int, int)> points)
        {
            BigOType res = new BigOFactorial();
            decimal minimum_distance = Decimal.MaxValue;

            foreach(BigOType b in types)
            {
                decimal dist = b.GetAverageDistance(points);
                if (dist <= minimum_distance)
                {
                    minimum_distance = dist;
                    res = b;
                }
            }

            return res;
        }

        public static List<(int, int)> GetBigOPoints(Algorithm algorithm)
        {
            List<(int, int)> points = new List<(int, int)>();

            for (int i = 0; i < N.Length; i++)
            {
                int[] arr = FillArray(N[i]);

                algorithm.func(arr);
                points.Add((N[i], algorithm.count));
                algorithm.count = 0;
            }

            return points;
        }

        public static int[] FillArray(int n)
        {
            int[] arr = new int[n];

            for (int i = 0; i < arr.Length; i++)
            {
                int r = Random.Next(10000);

                arr[i] = r;
            }

            return arr;
        }
    }
}
