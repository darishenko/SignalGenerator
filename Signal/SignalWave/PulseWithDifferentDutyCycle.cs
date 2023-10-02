using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalGenerator.Signal.SignalWave
{
    class PulseWithDifferentDutyCycle : ISignalWave
    {
        public double Amplitude { get; set; }
        public double DutyCycle { get; set; }
        public double Phase { get; set; }
        public double Frequency { get; set; }
        public double[] values { get; set; }

        public PulseWithDifferentDutyCycle(double amplitude, double dutyCycle, double frequency)
        {
            Amplitude = amplitude;
            DutyCycle = dutyCycle;
            Frequency = frequency;
        }

        public double[] GenerateSignalWaveDots(int time, int sampling)
        {
            int steps = time * sampling;
            double samplingStep = (double)time / steps;
            double[] signalWaveDots = new double[steps + 1];

            double T = 1 / Frequency;
            for (int i = 0; i < steps + 1; i++)
            {
                double d = (((double)i / sampling) % T) / T;
                if ( d < 1 / DutyCycle)
                {
                    signalWaveDots[i] = Amplitude;
                }
                else
                {
                    signalWaveDots[i] = -Amplitude;
                }
            }

            values = signalWaveDots;
            return signalWaveDots;
        }
    }
}
