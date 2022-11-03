using System;

namespace SignalGenerator.Signal.SignalType
{
    class HarmonicSignalWave : ISignalWave
    {
        public double Amplitude { get; set; }
        public double Phase { get; set; }
        public double Frequency { get; set; }

        public HarmonicSignalWave(double amplitude, double phase, double frequency)
        {
            this.Amplitude = amplitude;
            this.Phase = phase;
            this.Frequency = frequency;
        }

        public double[] GenerateSignalWaveDots(int time, int sampling)
        {
            int steps = time * sampling;
            double samplingStep = (double)time / steps;
            double[] signalWaveDots = new double[ steps +1 ];

            for(int i=0; i < steps +1; i++)
            {
                signalWaveDots[i] = Amplitude * Math.Sin(2 * Math.PI * i * samplingStep * Frequency + Phase);
            }

            return signalWaveDots;
        }
    }
}
