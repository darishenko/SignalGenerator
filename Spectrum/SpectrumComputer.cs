using System;

namespace SignalGenerator.Spectrum
{
    public class SpectrumComputer
    {
        public static double[] ComputeAmplitudeSpectrum(Complex[] spectrum)
        {
            var N = spectrum.Length;
            var amplitudeSpectrum = new double[N];

            for (var k = 0; k < N; k++) amplitudeSpectrum[k] = spectrum[k].Magnitude;

            return amplitudeSpectrum;
        }

        public static double[] ComputePhaseSpectrum(Comlex[] spectrum)
        {
            var N = spectrum.Length;
            var phaseSpectrum = new double[N];

            for (var k = 0; k < N; k++) phaseSpectrum[k] = Math.Atan(spectrum[k].Imaginary / spectrum[k].Real);

            return phaseSpectrum;
        }
    }
}