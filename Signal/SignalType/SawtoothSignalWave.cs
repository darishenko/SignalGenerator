using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalGenerator.Signal.SignalType
{
    class SawtoothSignalWave : ISignalWave
    {
        public double amplitude { get; set; }
        public double phase { get; set; }
        public double frequency { get; set; }

        public SawtoothSignalWave(double amplitude, double phase, double frequency)
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
                signalWaveDots[i] = 2 * amplitude / Math.PI * Math.Atan(Math.Tan(Math.PI * frequency * i * samplingStep + phase));
            }

            return signalWaveDots;
        }
    }
}
