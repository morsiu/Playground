using System;
using System.Collections.Generic;
using Xunit;

namespace TypeClasses
{
    public static class Applicative
    {
        public interface IApplicative<
            TInputApplicative,
            TOutputApplicative,
            TFunctionApplicative,
            TInput,
            TOutput,
            TFunctor>
            where TFunctor : Functor.IFunctor<TInputApplicative, TOutputApplicative, TInput, TOutput>
        {
            TOutputApplicative Pure(TOutput output);

            TOutputApplicative Apply(TFunctionApplicative function, TInputApplicative input);
        }

        public static TOutputApplicative Apply<TInputApplicative, TOutputApplicative, TFunctionApplicative, TInput, TOutput, TFunctor, TApplicative>(
            TFunctionApplicative function,
            TInputApplicative input)
            where TFunctor : Functor.IFunctor<TInputApplicative, TOutputApplicative, TInput, TOutput>
            where TApplicative : IApplicative<TInputApplicative, TOutputApplicative, TFunctionApplicative, TInput, TOutput, TFunctor>
        {
            var applicative = default(TApplicative);
            return applicative.Apply(function, input);
        }

        public struct ListApplicative<TInput, TOutput, TFunctor>
            : IApplicative<
                List<TInput>,
                List<TOutput>,
                List<Func<TInput, TOutput>>,
                TInput,
                TOutput,
                TFunctor>
            where TFunctor : Functor.IFunctor<List<TInput>, List<TOutput>, TInput, TOutput>
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
        public static void Lists()
        {
            Assert.Equal(
                new[] { 1, 2, 3, 2, 4, 6, 2, 3, 4 },
                Apply<List<int>, List<int>, List<Func<int, int>>, int, int, Functor.ListFunctor<int, int>, ListApplicative<int, int, Functor.ListFunctor<int, int>>>(
                    new List<Func<int, int>> { x => x, x => x * 2, x => x + 1 },
                    new List<int> { 1, 2, 3 }));
        }
    }
}
