using System;

namespace BigOCalculator
{
    public interface Algorithm
    {
        public int count { get; set; }

        public int[] func(int[] arr);
    }
}
