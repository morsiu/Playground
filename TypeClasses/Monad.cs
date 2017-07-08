using System;

namespace TypeClasses
{
    public static class Monad
    {
        public interface IMonand<TInputMonad, TOutputMonad, TInput, TOutput, TApplicative, TFunctor>
        {
            TOutputMonad Return(TOutput output);

            TOutputMonad Bind(Func<TInput, TOutputMonad> function, TInputMonad input);
        }
    }
}
