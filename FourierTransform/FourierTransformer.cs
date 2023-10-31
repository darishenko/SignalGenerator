using System;

namespace SignalGenerator.FourierTransform
{
    public class FourierTransformer
    {
        public static Complex[] DiscreteFourierTransform(double[] signalValues)
        {
            var N = signalValues.Length;
            Complex[] spectrum = new Complex[N];
            for (var k = 0; k < N; k++)
            {
                spectrum[k] = 0;
                var angle = 2 * Math.PI * k / N;
                for (var n = 0; n < N; n++)
                {
                    Complex complex = new Complex(Math.Cos(angle * n), -Math.Sin(angle * n));
                    spectrum[k] += signalValues[k] * complex;
                }
            }

            return spectrum;
        }

        public static double[] InverseDiscreteFourierTransform(Complex[] spectrum)
        {
            var N = spectrum.Length;
            var result = new double[N];
            for (var n = 0; n < N; n++)
            {
                Complex sum = 0;
                var angle = 2 * Math.PI * n / N;
                for (var k = 0; k < N; k++)
                {
                    Complex complex = Complex.FromPolarCoordinates(1, angle * k); //комплексное умножение
                    //Complex complex = new Complex(Math.Cos(angle * k), Math.Sin(angle * k));
                    sum += spectrum[k] * complex;
                }

                result[n] = sum.Real / N;
            }

            return result;
        }
    }
}