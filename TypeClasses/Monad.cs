using System;

namespace TypeClasses
{
    public static class Monad
    {
        public interface IMonand<TInputMonad, TOutputMonad, TFunctionMonad, TInput, TOutput, TApplicative, TFunctor>
            where TApplicative : Applicative.IApplicative<TInputMonad, TOutputMonad, TFunctionMonad, TInput, TOutput, TFunctor>
            where TFunctor : Functor.IFunctor<TInputMonad, TOutputMonad, TInput, TOutput>
        {
            TOutputMonad Return(TOutput output);

            TOutputMonad Bind(Func<TInput, TOutputMonad> function, TInputMonad input);
        }
    }
}
