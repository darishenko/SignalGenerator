using System;

namespace SignalGenerator.Signal.SignalWave
{
    internal class Noise : ISignalWave
    {
        private Random random;

        public Noise(double amplitude)
        {
            random = new Random();
            Amplitude = amplitude;
        }

        public double[] Values { get; set; }
        public double Amplitude { get; set; }

        public double Phase
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public double Frequency
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public double[] GenerateSignalWaveDots(int time, int sampling)
        {
            var steps = time * sampling;
            var samplingStep = (double) time / steps;
            var signalWaveDots = new double[steps + 1];

            var random = new Random();
            for (var i = 0; i < steps + 1; i++) signalWaveDots[i] = Amplitude * (2 * random.NextDouble() - 1);

            Values = signalWaveDots;
            return Values;
        }

        public double[] FrequencyModulation(ISignalWave modulationSignal, int sampling)
        {
            return Values;
        }
    }
}