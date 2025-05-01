namespace AITestApp
{
    public class InputTarget
    {
        public double[] Input;
        public double Target;

        public InputTarget(double[] input, double target)
        {
            if (input.Length == 0)
            {
                throw new Exception("The input array must contain values");
            }

            Input = input;
            Target = target;
        }
    }
}
