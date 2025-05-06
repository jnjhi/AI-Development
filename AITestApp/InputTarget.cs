using System.Collections;

namespace AITestApp
{
    public class InputTarget
    {
        public double[] Input { get; }
        public double Target { get; }

        public InputTarget(double[] input, double target)
        {
            if (input.Length == 0)
            {
                throw new Exception("The input array must contain values");
            }

            Input = input;
            Target = target;
        }

        public override bool Equals(object obj) => obj is InputTarget it && StructuralComparisons.StructuralEqualityComparer.Equals(Input, it.Input) && Target == it.Target;
        public override int GetHashCode() => HashCode.Combine(StructuralComparisons.StructuralEqualityComparer.GetHashCode(Input), Target);
    } 
}
