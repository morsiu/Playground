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
    }
}
