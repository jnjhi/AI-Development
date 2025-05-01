namespace AITestApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            double[] weights = {0.5, 0.5, 0.5, 0.5};
            double learningSpeed = 0.001;
            double bias = 0.5;

            List<InputTarget> InputTargetPair = new List<InputTarget>();

            // Student studies a lot, perfect attendance, high quiz scores → should pass
            InputTargetPair.Add(new InputTarget(new double[] { 0.9, 0.95, 0.85, 1.0 }, 1));

            // Student makes a moderate effort, mixed scores and attendance → should fail
            InputTargetPair.Add(new InputTarget(new double[] { 0.3, 0.5, 0.4, 0.6 }, 0));

            // Student is consistent and above average, good attendance → should pass
            InputTargetPair.Add(new InputTarget(new double[] { 0.7, 0.8, 0.6, 0.9 }, 1));

            // Student does very little across the board → should fail
            InputTargetPair.Add(new InputTarget(new double[] { 0.1, 0.3, 0.2, 0.2 }, 0));

            double error = 0;
            double WeightGradient = 0;
            double BiasGradient = 0;

            for (int i = 0; i < 1000000; i++)
            {
                foreach (var pair in InputTargetPair)
                {
                    var prediction = sigmoid(DotProduct(pair.Input , weights) + bias);
                    error = prediction - pair.Target;

                    for (int j = 0; j < weights.Length; j++)
                    {
                        WeightGradient = CalculateGradientCrossEntropy(error, pair.Input[j]);
                        weights[j] -= WeightGradient * learningSpeed;
                    }

                    BiasGradient = CalculateGradientCrossEntropy(error, 1);
                    bias -= BiasGradient * learningSpeed;
                }
            }

            Console.WriteLine("weight: " + ArrayToString(weights));
            Console.WriteLine("Bias: " + bias);
            Console.WriteLine("Final Results After Training:");

            foreach (var pair in InputTargetPair)
            {
                double[] input = pair.Input;
                double target = pair.Target;
                double prediction = sigmoid(DotProduct(pair.Input, weights) + bias);
                double err = ((target - prediction) * (target - prediction)) / 2;

                Console.WriteLine($"Input: {ArrayToString(input)}, Target: {target}, Prediction: {prediction:F4}, Error: {err:F4}");
            }
        }
        
        private static double sigmoid(double x) => 1 / (1 + Math.Pow(Math.E, -x));

        private static double CalculateGradientMSE(double error, double prediction, double input) => error * prediction * (1 - prediction) * input;

        private static double CalculateGradientCrossEntropy(double error, double input) => error * input;
        
        private static double DotProduct(double[] array1, double[] array2)
        {
            if (array1.Length != array2.Length)
            {
                throw new Exception("The arrays must be the same length");
            }

            double sum = 0;

            for (int i = 0; i < array1.Length; i++)
            {
                sum += array1[i] * array2[i];
            }

            return sum;
        }

        private static string ArrayToString(double[] array)
        {
            string outputString = "";

            for (int i = 0; i < array.Length - 1; i++)
            {
                outputString += array[i] + ", ";
            }

            outputString += array[array.Length - 1] + ".";

            return outputString;
        }
    }
}
