namespace VGA.Mutations.TestAssembly
{
    public class Calculator
    {
        public int Add(int a, int b)
        {
            return a + b;
        }

        public int Sub(int a, int b)
        {
            return a - b;
        }

        public int Mult(int a, int b)
        {
            return a*b;
        }

        public int Div(int a, int b)
        {
            return a/b;
        }

        public int DivWithCheck(int a, int b)
        {
            if ( a < b)
                return a / b;

            return b/a;
        }
    }
}