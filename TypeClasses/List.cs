using System;
using System.Collections.Generic;
using Xunit;

namespace TypeClasses
{
    public static class List
    {
        public struct ListFunctor<TInput, TOutput>
            : Prelude.IFunctor<List<TInput>, List<TOutput>, TInput, TOutput>
        {
            public List<TOutput> Fmap(List<TInput> input, Func<TInput, TOutput> function)
            {
                var output = new List<TOutput>(input.Count);
                foreach (var x in input)
                {
                    output.Add(function(x));
                }
                return output;
            }
        }

        public struct ListApplicative<TInput, TOutput, TFunctor>
            : Prelude.IApplicative<
                List<TInput>,
                List<TOutput>,
                List<Func<TInput, TOutput>>,
                TInput,
                TOutput,
                TFunctor>
            where TFunctor : Prelude.IFunctor<List<TInput>, List<TOutput>, TInput, TOutput>
        {
            public List<TOutput> Apply(List<Func<TInput, TOutput>> function, List<TInput> input)
            {
                var output = new List<TOutput>(function.Count * input.Count);
                var functor = default(TFunctor);
                foreach (var f in function)
                {
                    output.AddRange(functor.Fmap(input, f));
                }
                return output;
            }

            public List<TOutput> Pure(TOutput output)
            {
                return new List<TOutput> { output };
            }
        }

        [Fact]
        public static void FunctorExample()
        {
            Assert.Equal(
                new[] { 2, 4, 6, 8 },
                default(ListFunctor<int, int>).Fmap(
                    new List<int> { 1, 2, 3, 4 },
                    x => x * 2));
        }

        [Fact]
        public static void ApplicativeExample()
        {
            Assert.Equal(
                new[] { 1, 2, 3, 2, 4, 6, 2, 3, 4 },
                default(ListApplicative<int, int, ListFunctor<int, int>>).Apply(
                    new List<Func<int, int>> { x => x, x => x * 2, x => x + 1 },
                    new List<int> { 1, 2, 3 }));
        }

    }
}
