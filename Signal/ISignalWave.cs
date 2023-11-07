namespace SignalGenerator.Signal
{
    internal interface ISignalWave
    {
        double Amplitude { get; set; }
        double Phase { get; set; }
        double Frequency { get; set; }
        double[] Values { get; set; }
        double[] GenerateSignalWaveDots(int time, int sampling);
        double[] FrequencyModulation(ISignalWave modulationSignal, int sampling);
    }
}