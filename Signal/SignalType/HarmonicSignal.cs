using System;

namespace SignalGenerator.Signal.SignalType
{
    class HarmonicSignal
    {
        public double amplitude;
        public double phase;
        public double frequency;

        public HarmonicSignal(double amplitude, double phase, double frequency)
        {
            this.amplitude = amplitude;
            this.phase = phase;
            this.frequency = frequency;
        }

        public double[] GenerateSignalWaveDots(int time, int sampling)
        {
            int steps = time * sampling;
            double samplingStep = (double)time / steps;
            double[] signalWaveDots = new double[ steps +1 ];

            for(int i=0; i < steps +1; i++)
            {
                signalWaveDots[i] = amplitude * Math.Cos(i * samplingStep * frequency + phase);
                Console.WriteLine( i+1 +"   "+i * samplingStep);
            }

            return signalWaveDots;
        }
    }
}
