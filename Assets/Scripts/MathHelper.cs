using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MRD
{
    public static class MathHelper
    {
        public static Vector3 RotateVector(Vector3 v, float degreeClockwise)
        {
            return Quaternion.AngleAxis(degreeClockwise, Vector3.forward) * v;
        }

        public static IEnumerable<List<T>> SubSetsOf<T>(List<T> items, int k)
        {
            return Combinations(items.Count, k).Select(comb => comb.Select(index => items[index]).ToList());
        }

        private static IEnumerable<IEnumerable<int>> Combinations(int n, int k)
        {
            long m = 1 << n;

            for (long i = 1; i < m; ++i)
                if (NumberOfSetBits((uint)i) == k)
                    yield return BitIndices((uint)i);
        }

        private static IEnumerable<int> BitIndices(uint n)
        {
            uint mask = 1;

            for (int bit = 0; bit < 32; ++bit, mask <<= 1)
                if ((n & mask) != 0)
                    yield return bit;
        }

        private static int NumberOfSetBits(uint i)
        {
            unchecked
            {
                i -= (i >> 1) & 0x55555555;
                i = (i & 0x33333333) + ((i >> 2) & 0x33333333);
                return (int)(((i + (i >> 4)) & 0x0F0F0F0F) * 0x01010101) >> 24;
            }
        }
    }
}
