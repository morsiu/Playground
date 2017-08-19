namespace SelfType
{
    public static class NonGenericManipulation
    {
        private static void Consumer()
        {
            var x = new Implementation();
            var result = x.Manipulation();
        }

        private sealed class Implementation
        {
            public Implementation Manipulation()
            {
                return DoA().DoB();
            }

            Implementation DoA() => this;

            Implementation DoB() => this;
        }
    }
}
