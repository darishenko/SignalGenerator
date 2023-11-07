using System;
using System.Linq;
using System.Numerics;

namespace SignalGenerator.FourierTransform.Impl
{
    public class FastFourierTransformer : IFourierTransformer
    {
        public Complex[] DiscreteFourierTransform(double[] signalValues)
        {
            return Iteration(CreateComplexArrayWithLengthPow2(signalValues), false);
        }

        public double[] InverseDiscreteFourierTransform(Complex[] spectrum)
        {
            return Iteration(spectrum, true).Select(d => d.Real).ToArray();
        }

        private Complex[] Iteration(Complex[] data, bool invert)
        {
            var N = data.Length;
            if (N == 1) return data;

            var result = new Complex[N];

            var even = new Complex[N / 2];
            var odd = new Complex[N / 2];

            for (int i = 0, j = 0; i < N; i += 2, j++)
            {
                even[j] = data[i];
                odd[j] = data[i + 1];
            }

            var evenRes = Iteration(even, invert);
            var oddRes = Iteration(odd, invert);

            var ang = 2 * Math.PI / N * (invert ? -1 : 1);

            var w = new Complex(1.0, 0.0);
            var wn = new Complex(Math.Cos(ang), Math.Sin(ang));

            for (var i = 0; i < N / 2; ++i)
            {
                result[i] = evenRes[i] + w * oddRes[i];
                result[i + N / 2] = evenRes[i] - w * oddRes[i];
                if (invert)
                {
                    result[i] /= 2;
                    result[i + N / 2] /= 2;
                }

                w *= wn;
            }

            return result;
        }

        private Complex[] CreateComplexArrayWithLengthPow2(double[] data)
        {
            var length = data.Length;
            var lengthNew = 1;
            while (lengthNew < length) lengthNew *= 2;
            var result = new Complex[lengthNew];
            if (lengthNew == length) return data.Select(d => new Complex(d, 0.0)).ToArray();
            for (var i = 0; i < lengthNew; i++)
                if (i < data.Length)
                    result[i] = new Complex(data[i], 0.0);
                else
                    result[i] = new Complex(0.0, 0.0);
            return result;
        }
    }
}