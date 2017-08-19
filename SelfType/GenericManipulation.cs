namespace SelfType
{
    public static class GenericManipulation
    {
        private interface IInterface<TSelf>
            where TSelf : IInterface<TSelf>
        {
            TSelf DoA();

            TSelf DoB();
        }

        private static T Manipulation<T>(T input)
            where T : IInterface<T>
        {
            return input.DoA().DoB();
        }

        private static void Consumer()
        {
            var x = new Implementation();
            var result = Manipulation(x);
        }

        private sealed class Implementation : IInterface<Implementation>
        {
            public Implementation DoA() => this;

            public Implementation DoB() => this;
        }
    }
}
