using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalGenerator.Signal.SignalWave
{
    class Noise: ISignalWave
    {
        private Random random;

        public Noise(double amplitude) 
        {
            random = new Random();
            this.Amplitude = amplitude;
        }

        public double Amplitude { get; set; }
        public double Phase { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public double Frequency { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public double[] GenerateSignalWaveDots(int time, int sampling)
        {
            int steps = time * sampling;
            double samplingStep = (double)time / steps;
            double[] signalWaveDots = new double[steps + 1];

            Random random = new Random();
            for (int i = 0; i < steps + 1; i++)
            {
                signalWaveDots[i] = Amplitude * (2 * random.NextDouble() - 1);
            }

            return signalWaveDots;
        }
    }
}
