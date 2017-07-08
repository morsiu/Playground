using Xunit;

namespace TypeClasses
{
    public static class Integer
    {
        public struct IntegerMonoid : Prelude.IMonoid<int>
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
        public static void MonoidExample()
        {
            Assert.Equal(
                10,
                Prelude.Concat<int, IntegerMonoid>(
                    new[] { 1, 2, 3, 4 }));
        }
    }
}
