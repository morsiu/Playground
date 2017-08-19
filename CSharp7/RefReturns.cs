namespace CSharp7
{
    public static class RefReturns
    {
        public static ref int Method(ref int input)
        {
            return ref input;
        }
    }
}