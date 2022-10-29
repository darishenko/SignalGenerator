﻿using System;

namespace SignalGenerator.Signal.SignalType
{
    class SquareSignalWave : ISignalWave
    {
        public double amplitude { get; set; }
        public double phase { get; set; }
        public double frequency { get; set; }

        public SquareSignalWave(double amplitude, double phase, double frequency)
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
                signalWaveDots[i] = amplitude * Math.Sign(Math.Sin(2 * Math.PI * samplingStep * i * frequency));
            }

            return signalWaveDots;
        }
    }
}
