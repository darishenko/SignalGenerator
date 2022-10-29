using System;

namespace SignalGenerator.Signal.SignalType 
{
    class TriangleSignalWave : ISignalWave
    {
        public double amplitude { get; set; }
        public double phase { get; set; }
        public double frequency { get; set; }

        public TriangleSignalWave(double amplitude, double phase, double frequency)
        {
            this.amplitude = amplitude;
            this.phase = phase;
            this.frequency = frequency;
        }

        public double[] GenerateSignalWaveDots(int time, int sampling)
        {
            int steps = time * sampling;
            double samplingStep = (double)time / steps;
            double[] signalWaveDots = new double[steps + 1];

            for (int i = 0; i < steps + 1; i++)
            {
                signalWaveDots[i] = 2 * amplitude / Math.PI * Math.Asin(Math.Sin(2 * Math.PI * samplingStep*i +phase));
                //signalWaveDots[i] = amplitude * (1f - 4f * (float)Math.Abs(Math.Round(samplingStep* i * frequency + phase - 0.25f) - (samplingStep * i * frequency + phase - 0.25f)));
            }

            return signalWaveDots;
        }
    }
}
