using System.Runtime.CompilerServices;

namespace CSharp7
{
    public static class RefLocal
    {
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static int Method(ref int parameter)
        {
            var variable = parameter + 17;
            return Method2(ref variable);
        }

        public static int Method2(ref int parameter)
        {
            return Method(ref parameter);
        }
    }
}