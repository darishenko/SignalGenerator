using System;
using System.Numerics;

namespace SignalGenerator.Filter
{
    public static class Filter
    {
        public static double[] Low(double[] spectrum, int sampling, double cutoffFrequency)
        {
            if (cutoffFrequency == 0) return spectrum;
            
            int N = spectrum.Length;
            double[] result = new double[N];

            for (int i = 0; i < N; i++)
            {
                double f = (i * (double)sampling) / N;
                result[i] = f > cutoffFrequency ? 0 : f;
            }

            return result;
        }

        public static double[] High(double[] spectrum, int sampling, double cutoffFrequency)
        {
            if (cutoffFrequency == 0) return spectrum;
            
            int N = spectrum.Length;
            double[] result = new double[N];

            for (int i = 0; i < N; i++)
            {
                double f = (i * (double)sampling) / N;
                result[i] = f < cutoffFrequency ? 0 : f;
            }

            return result;
        }

        public static double[] Band(double[] spectrum, int sampling, double cutoffFrequencyL, double cutoffFrequencyH)
        {
            if (cutoffFrequencyL == 0 && cutoffFrequencyH == 0) return spectrum;

            int N = spectrum.Length;
            double[] result = new double[N];

            for (int i = 0; i < N; i++)
            {
                double f = (i * (double)sampling) / N;
                result[i] = f < cutoffFrequencyL || f > cutoffFrequencyH ? 0 : f;
            }

            return result;
        }

    }
}