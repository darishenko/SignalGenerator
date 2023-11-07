using System.Numerics;

namespace SignalGenerator.FourierTransform
{
    public interface IFourierTransformer
    {
        Complex[] DiscreteFourierTransform(double[] signalValues);
        double[] InverseDiscreteFourierTransform(Complex[] spectrum);
    }
}