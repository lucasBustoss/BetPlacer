using MathNet.Numerics.Distributions;

namespace BetPlacer.Punter.API.Utils
{
    public static class MathUtils
    {
        public static double StandardDeviation(IEnumerable<double> sequence)
        {
            double result = 0;

            if (sequence.Any())
            {
                double average = sequence.Average();
                double sum = sequence.Sum(d => Math.Pow(d - average, 2));
                result = Math.Sqrt((sum) / sequence.Count());
            }
            return result;
        }

        public static double PoissonProbability(double lambda, int k)
        {
            if (lambda == 0)
                return 0;
            
            double result = 1 - Poisson.CDF(lambda, k);
            return result;
        }

        public static double Confidence(double alpha, double stdDev, double sampleSize)
        {
            double z = Normal.InvCDF(0, 1, 1 - alpha / 2);

            // Cálculo da confiança
            double confidence = z * (stdDev / Math.Sqrt(sampleSize));

            return confidence;
        }

        private static int Factorial(int n)
        {
            if (n == 0)
                return 1;
            int result = 1;
            for (int i = 1; i <= n; i++)
            {
                result *= i;
            }
            return result;
        }
    }
}
