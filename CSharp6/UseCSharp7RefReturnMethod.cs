using System;
using CSharp7;

namespace CSharp6
{
    public static class UseCSharp7RefReturnMethod
    {
        public static int Method()
        {
            var variable = 1;
            var result = RefReturns.Method(ref variable);
            return result;
        }
    }
}