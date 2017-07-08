using System;
using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace TypeClasses
{
    public static class Functor
    {
        public interface IFunctor<TInputFunctor, TOutputFunctor, TInput, TOutput>
        {
            TOutputFunctor Fmap(TInputFunctor input, Func<TInput, TOutput> function);
        }

        public struct ListFunctor<TInput, TOutput>
            : IFunctor<List<TInput>, List<TOutput>, TInput, TOutput>
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

        public static TOutputFunctor Map<TInputFunctor, TOutputFunctor, TInput, TOutput, TFunctor>(
            TInputFunctor input,
            Func<TInput, TOutput> function)
            where TFunctor : struct, IFunctor<TInputFunctor, TOutputFunctor, TInput, TOutput>
        {
            return default(TFunctor).Fmap(input, function);
        }

        [Fact]
        public static void Lists()
        {
            Assert.Equal(
                new[] { 2, 4, 6, 8 },
                Map<List<int>, List<int>, int, int, ListFunctor<int, int>>(
                    new List<int> { 1, 2, 3, 4 },
                    x => x * 2));
        }
    }
}
