using MathNet.Numerics.Distributions;
using MathNet.Numerics.LinearAlgebra;

namespace AITestApp
{
    internal class Program
    {
        static void Main()
        {
            var rnd = new Random(190204); // seed for reproducibility

            // Hardcoded 3x3 input matrix X
            var X = Matrix<double>.Build.DenseOfArray(new double[,]
            {
                { 1, -2.0, 0.5 },
                { 0.3,  0.8, -1.1 },
                { -0.7, 1.5, 2.0 }
             });

            // Hardcoded 3x2 weight matrix W
            var W = Matrix<double>.Build.DenseOfArray(new double[,]
            {
                { 0.4, -1.2 },
                { 1.5,  0.7 },
                { -0.3, 0.6 }
            });


            var product = X * W;

            var dLdS = Matrix<double>.Build.Dense(product.RowCount, product.ColumnCount, 1.0);
            var dSigmaDp = product.Map(SigmoidDerivative);
            var DerivativeWithRespectToX = dLdS.PointwiseMultiply(dSigmaDp) * W.Transpose();
            var DerivativeWithRespectToW = X.Transpose() * dLdS.PointwiseMultiply(dSigmaDp);

            var L = product.Map(Sigmoid).RowSums().Sum();

            Console.WriteLine("L:");
            Console.WriteLine(L + "\n");

            Console.WriteLine("X:");
            Console.WriteLine(X);
            Console.WriteLine("\ndL/dX:");
            Console.WriteLine(DerivativeWithRespectToX);

            Console.WriteLine("W:");
            Console.WriteLine(W);
            Console.WriteLine("\ndL/dW:");
            Console.WriteLine(DerivativeWithRespectToW);
        }

        // Sigmoid activation
        private static double Sigmoid(double x) => 1.0 / (1.0 + Math.Exp(-x));

        // Derivative of sigmoid
        private static double SigmoidDerivative(double x)
        {
            var s = Sigmoid(x);
            return s * (1 - s);
        }
    }
}
