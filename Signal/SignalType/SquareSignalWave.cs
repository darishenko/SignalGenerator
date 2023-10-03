using System;

namespace SignalGenerator.Signal.SignalType
{
    internal class SquareSignalWave : ISignalWave
    {
        public SquareSignalWave(double amplitude, double phase, double frequency)
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
                signalWaveDots[i] = Amplitude * Math.Sign(Math.Sin(2 * Math.PI * i * samplingStep * Frequency + Phase));

            Values = signalWaveDots;
            return Values;
        }

        public double[] FrequencyModulation(ISignalWave modulationSignal, int sampling)
        {
            double sum = 0;
            for (int i = 0; i < Values.Length; i++)
            {
                sum += 2 * Math.PI * Frequency * (1 + modulationSignal.Values[i]) / sampling;
                Values[i] = Amplitude * Math.Sign(Math.Sin(sum + Phase));
            }
            return Values;
        }
    }
}