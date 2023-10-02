namespace SignalGenerator.Signal.SignalWave
{
    internal class PulseWithDifferentDutyCycle : ISignalWave
    {
        public PulseWithDifferentDutyCycle(double amplitude, double dutyCycle, double frequency)
        {
            Amplitude = amplitude;
            DutyCycle = dutyCycle;
            Frequency = frequency;
        }

        public double Amplitude { get; set; }
        public double DutyCycle { get; set; }
        public double Phase { get; set; }
        public double Frequency { get; set; }
        public double[] Values { get; set; }

        public double[] GenerateSignalWaveDots(int time, int sampling)
        {
            var steps = time * sampling;
            var samplingStep = (double) time / steps;
            var signalWaveDots = new double[steps + 1];

            var T = 1 / Frequency;
            for (var i = 0; i < steps + 1; i++)
            {
                var d = (double) i / sampling % T / T;
                if (d < 1 / DutyCycle)
                    signalWaveDots[i] = Amplitude;
                else
                    signalWaveDots[i] = -Amplitude;
            }

            Values = signalWaveDots;
            return Values;
        }
    }
}