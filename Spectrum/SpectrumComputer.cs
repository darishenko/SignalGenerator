using System.Numerics;

namespace SignalGenerator.Spectrum
{
    public static class SpectrumComputer
    {
        public static double[] ComputeAmplitudeSpectrum(Complex[] spectrum)
        {
            var N = spectrum.Length;
            var amplitudeSpectrum = new double[N];

            for (var k = 0; k < N; k++) amplitudeSpectrum[k] = 2 * spectrum[k].Magnitude / N;

            return amplitudeSpectrum;
        }

        public static double[] ComputePhaseSpectrum(Complex[] spectrum)
        {
            var N = spectrum.Length;
            var phaseSpectrum = new double[N];
            
            for (var k = 0; k < N; k++) phaseSpectrum[k] = spectrum[k].Phase;
            
            return phaseSpectrum;
        }
        
    }
}