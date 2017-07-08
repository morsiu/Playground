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
    }
}
