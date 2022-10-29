namespace SignalGenerator.Signal
{
    interface ISignalWave
    {
        double amplitude { get; set; }
        double phase { get; set; }
        double frequency { get; set; }

        double[] GenerateSignalWaveDots(int time, int sampling);
    }
}
