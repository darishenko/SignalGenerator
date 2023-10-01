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

            float t_imp = (float)((1 / Frequency) / DutyCycle * Frequency);
            for (int i = 0; i < steps + 1; i++)
            {
                if (t_imp > (i % sampling )* samplingStep)
                {
                    signalWaveDots[i] = Amplitude;
                }
                else
                {
                    signalWaveDots[i] = -Amplitude;
                }
            }
            return signalWaveDots;
        }
    }
}
