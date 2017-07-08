using System;
using System.Collections.Generic;

namespace TypeClasses
{
    public static class Prelude
    {
        public interface IMonoid<T>
        {
            T Zero();
            T Add(T first, T second);
        }

        public interface IFunctor<TInputFunctor, TOutputFunctor, TInput, TOutput>
        {
            TOutputFunctor Fmap(TInputFunctor input, Func<TInput, TOutput> function);
        }

        public interface IApplicative<
            TInputApplicative,
            TOutputApplicative,
            TFunctionApplicative,
            TInput,
            TOutput,
            TFunctor>
            where TFunctor : IFunctor<TInputApplicative, TOutputApplicative, TInput, TOutput>
        {
            TOutputApplicative Pure(TOutput output);

            TOutputApplicative Apply(TFunctionApplicative function, TInputApplicative input);
        }

        public interface IMonand<TInputMonad, TOutputMonad, TFunctionMonad, TInput, TOutput, TApplicative, TFunctor>
            where TApplicative : IApplicative<TInputMonad, TOutputMonad, TFunctionMonad, TInput, TOutput, TFunctor>
            where TFunctor : IFunctor<TInputMonad, TOutputMonad, TInput, TOutput>
        {
            TOutputMonad Return(TOutput output);

            TOutputMonad Bind(Func<TInput, TOutputMonad> function, TInputMonad input);
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
    }
}
