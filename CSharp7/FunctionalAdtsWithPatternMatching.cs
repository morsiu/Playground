using System.Collections.Generic;
using Xunit;

namespace CSharp7
{
    public class FunctionalAdtsWithPatternMatching
    {
        public class Tests
        {
            [Theory]
            [MemberData("AreaTestData")]
            public void TestArea(object input, double? result)
            {
                Assert.Equal(result, Functions.Area(input));
            }

            public static IEnumerable<object[]> AreaTestData()
            {
                return new TheoryData<object, double?>
                {
                    { null, null },
                    { new Circle(1), 6.28d },
                    { new Rectangle(2, 3), 6d }
                };
            }
        }

        public static class Functions
        {
            public static double? Area(object x)
            {
                switch (x)
                {
                    case Circle c:
                    {
                        var (r, _) = c;
                        return 2.0 * r * 3.14d;
                    }
                    case Rectangle r:
                    {
                        var (w, h) = r;
                        return w * h;
                    }
                    default:
                        return null;
                }
            }
        }

        public sealed class Rectangle
        {
            public Rectangle(double width, double height)
            {
                Width = width;
                Height = height;
            }

            private double Width { get; }
            private double Height { get; }

            public void Deconstruct(out double width, out double height)
            {
                width = Width;
                height = Height;
            }
        }

        public sealed class Circle
        {
            public Circle(double radius)
            {
                Radius = radius;
            }

            private double Radius { get; }

            public void Deconstruct(out double radius, out double unused)
            {
                radius = Radius;
                unused = 0d;
            }
        }
    }
}
