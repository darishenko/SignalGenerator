using System;
using System.Numerics;

namespace SignalGenerator.FourierTransform.Impl
{
    public class FourierTransformer : IFourierTransformer
    {
        public Complex[] DiscreteFourierTransform(double[] signalValues)
        {
            var N = signalValues.Length;
            var spectrum = new Complex[N];
            for (var k = 0; k < N; k++)
            {
                spectrum[k] = Complex.Zero;
                var angle = 2 * Math.PI * k / N;
                for (var n = 0; n < N; n++)
                {
                    var complex = new Complex(Math.Cos(angle * n), -Math.Sin(angle * n));
                    spectrum[k] += signalValues[n] * complex;
                }
            }

            return spectrum;
        }

        public double[] InverseDiscreteFourierTransform(Complex[] spectrum)
        {
            var N = spectrum.Length;
            var result = new double[N];
            for (var n = 0; n < N; n++)
            {
                var sum = Complex.Zero;
                var angle = 2 * Math.PI * n / N;
                for (var k = 0; k < N; k++)
                {
                    var complex = Complex.FromPolarCoordinates(1, angle * k);
                    sum += spectrum[k] * complex;
                }

                result[n] = sum.Real / N;
            }

            return result;
        }
    }
}