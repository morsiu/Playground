using System;
using System.Collections.Generic;
using Xunit;

namespace TypeClasses
{
    public static class Monoid
    {
        public interface IMonoid<T>
        {
            T Zero();
            T Add(T first, T second);
        }

        public static T Concat<T, MonoidT>(IEnumerable<T> xs)
            where MonoidT : struct, IMonoid<T>
        {
            var monoidT = default(MonoidT);
            var sum = monoidT.Zero();
            foreach (var x in xs)
            {
                sum = monoidT.Add(x, sum);
            }
            return sum;
        }

        public struct IntegerMonoid : IMonoid<int>
        {
            public int Add(int first, int second)
            {
                return first + second;
            }

            public int Zero()
            {
                return 0;
            }
        }

        [Fact]
        public static void Integers()
        {
            Assert.Equal(10, Monoid.Concat<int, IntegerMonoid>(new[] { 1, 2, 3, 4 }));
        }
    }
}
