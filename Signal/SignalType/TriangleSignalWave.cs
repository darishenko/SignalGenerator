using System;

namespace SignalGenerator.Signal.SignalType
{
    internal class TriangleSignalWave : ISignalWave
    {
        public TriangleSignalWave(double amplitude, double phase, double frequency)
        {
            Amplitude = amplitude;
            Phase = phase;
            Frequency = frequency;
        }

        public double Amplitude { get; set; }
        public double Phase { get; set; }
        public double Frequency { get; set; }
        public double[] Values { get; set; }

        public double[] GenerateSignalWaveDots(int time, int sampling)
        {
            var steps = time * sampling;
            var samplingStep = (double) time / steps;
            var signalWaveDots = new double[steps + 1];

            for (var i = 0; i < steps + 1; i++)
                signalWaveDots[i] = 2 * Amplitude *
                    Math.Asin(Math.Sin(2 * Math.PI * i * samplingStep * Frequency + Phase)) / Math.PI;

            Values = signalWaveDots;
            return Values;
        }
    }
}