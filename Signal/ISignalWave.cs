namespace SignalGenerator.Signal
{
    interface ISignalWave
    {
        double Amplitude { get; set; }
        double Phase { get; set; }
        double Frequency { get; set; }

        double[] values { get; set; }

        double[] GenerateSignalWaveDots(int time, int sampling);
    }
}
