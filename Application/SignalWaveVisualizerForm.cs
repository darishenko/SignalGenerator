using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Numerics;
using System.Windows.Forms;
using OxyPlot;
using OxyPlot.Series;
using SignalGenerator.FourierTransform;
using SignalGenerator.FourierTransform.Impl;
using SignalGenerator.Signal;
using SignalGenerator.Signal.SignalType;
using SignalGenerator.Signal.SignalWave;
using SignalGenerator.Sound;

using SignalGenerator.Spectrum;

namespace SignalGenerator
{
    public partial class SignalWaveVisualizerForm : Form
    {
        private int TIME = 1;
        private const int SAMPLING = 256;

        
        private static PlotModel _signalWavePlotModel;
        private readonly SoundGenerator _generator = new SoundGenerator();
        private SoundPlayer _player;
        private int _sampling;
        private ISignalWave _selectedSignalWave;
        private double[] _signalWavePoints;
        private double M = 1;

        //2
        private static IFourierTransformer fourierTransformer;
        private static PlotModel _signalPlotModel;
        private static PlotModel _signalFTPlotModel;
        private static PlotModel _amplPlotModel;
        private static PlotModel _phasePlotModel;

        //f
        private static PlotModel _signalPlotModel_f;
        private static PlotModel _filterPlotModel_f;
        private static PlotModel _amplPlotModel_f;
        private static PlotModel _phasePlotModel_f;

        public SignalWaveVisualizerForm()
        {
            InitializeComponent();
            comboBoxSignalWaveTypes.SelectedIndex = 0;

            _signalWavePlotModel = new PlotModel { Title = "Signal Wave" };
            _signalPlotModel = new PlotModel();
            _signalFTPlotModel = new PlotModel();
            _amplPlotModel = new PlotModel ();
            _phasePlotModel = new PlotModel();

            _signalPlotModel_f = new PlotModel();
            _filterPlotModel_f = new PlotModel();
            _amplPlotModel_f = new PlotModel();
            _phasePlotModel_f = new PlotModel();
            
            fourierTransformer = new FourierTransformer();
            //fourierTransformer = new FastFourierTransformer();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox.Text.Length > 0)
            {
                try
                {
                    var d = double.Parse(textBox.Text);
                }
                catch
                {
                    MessageBox.Show("Please enter only numbers.");
                    textBox.Text = textBox.Text.Remove(textBox.Text.Length - 1);
                }
            }
        }

        private void GenerateSignalWave(object sender, EventArgs e)
        {
            var time = int.Parse(TextBox_Time.Text);
            _sampling = int.Parse(TextBox_Sampling.Text);

            _selectedSignalWave = GetSignalWave(comboBoxSignalWaveTypes);
            _signalWavePoints = _selectedSignalWave.GenerateSignalWaveDots(time, _sampling);
            DrawSignalWave(_selectedSignalWave.Values, _signalWavePlotModel, SignalWavePlot);
        }

        private ISignalWave GetSignalWave(ComboBox comboBoxSignalWaveTypes)
        {
            double amplitude = 1;
            double phase = 0;
            double frequency = 1;
            var signalWaveType = (SignalWaveType)comboBoxSignalWaveTypes.SelectedIndex;
            if (signalWaveType != SignalWaveType.NOISE)
            {
                amplitude = double.Parse(TextBox_Amplitude.Text);
                phase = double.Parse(TextBox_Phase.Text);
                frequency = double.Parse(TextBox_Frequency.Text);
            }

            ISignalWave selectedSignalWave;
            switch (signalWaveType)
            {
                case SignalWaveType.HARMONIC:
                    selectedSignalWave = new HarmonicSignalWave(amplitude, phase, frequency);
                    break;
                case SignalWaveType.SQUARE:
                    selectedSignalWave = new SquareSignalWave(amplitude, phase, frequency);
                    break;
                case SignalWaveType.TRIANGLE:
                    selectedSignalWave = new TriangleSignalWave(amplitude, phase, frequency);
                    break;
                case SignalWaveType.SAWTOOTH:
                    selectedSignalWave = new SawtoothSignalWave(amplitude, phase, frequency);
                    break;
                case SignalWaveType.NOISE:
                    selectedSignalWave = new Noise(amplitude);
                    break;
                case SignalWaveType.PULSEWITHDIFFERENTDUTYCYCLE:
                    var dutyCycle = float.Parse(DutyCycle.Text);
                    selectedSignalWave = new PulseWithDifferentDutyCycle(amplitude, dutyCycle, frequency);
                    break;
                default:
                    selectedSignalWave = new HarmonicSignalWave(amplitude, phase, frequency);
                    break;
            }

            return selectedSignalWave;
        }

        private void DrawSignalWave(double[] signalWavePoints, PlotModel plotModel, OxyPlot.WindowsForms.PlotView plotView)
        {
            var signalWave = new LineSeries
            {
                Color = OxyPlot.OxyColors.CornflowerBlue,
                StrokeThickness = 0.1,
                MarkerSize = 0.9,
                MarkerType = MarkerType.Circle
            };

            for (var i = 0; i < signalWavePoints.Length; i++)
                signalWave.Points.Add(new DataPoint(i * ((double)TIME / signalWavePoints.Length), signalWavePoints[i]));

            plotModel.Series.Clear();
            plotModel.Series.Add(signalWave);
            plotView.Model = plotModel;
            plotView.InvalidatePlot(true);
        }

        private void DrawSpectrum(double[] spectrum, PlotModel plotModel, OxyPlot.WindowsForms.PlotView plotView)
        {
            var signalWave = new LineSeries
            {
                Color = OxyPlot.OxyColors.CornflowerBlue,
                StrokeThickness = 0.1,
                MarkerSize = 0.9,
                MarkerType = MarkerType.Circle
            };

            for (var i = 0; i < spectrum.Length; i++)
                signalWave.Points.Add(new DataPoint(i, spectrum[i]));

            plotModel.Series.Clear();
            plotModel.Series.Add(signalWave);
            plotView.Model = plotModel;
            plotView.InvalidatePlot(true);
        }

        private void b_play_Click(object sender, EventArgs e)
        {
            PlaySignal("result.wave", _sampling);
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void comboBoxSignalWaveTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var waves = new List<ISignalWave>();
            ISignalWave tempWave;
            for (var i = 1; i < 6; i++)
                switch (i)
                {
                    case 1:
                        tempWave = GetSignalWaveForPolyphonicSignal(comboBox1, a1, ph1, fr1, dc1, t1, null);
                        if (tempWave != null) waves.Add(tempWave);
                        break;
                    case 2:
                        tempWave = GetSignalWaveForPolyphonicSignal(comboBox2, a2, ph2, fr2, dc2, t2, null);
                        if (tempWave != null) waves.Add(tempWave);
                        break;
                    case 3:
                        tempWave = GetSignalWaveForPolyphonicSignal(comboBox3, a3, ph3, fr3, dc3, t3, null);
                        if (tempWave != null) waves.Add(tempWave);
                        break;
                    case 4:
                        tempWave = GetSignalWaveForPolyphonicSignal(comboBox4, a4, ph4, fr4, dc4, t4, null);
                        if (tempWave != null) waves.Add(tempWave);
                        break;
                    case 5:
                        tempWave = GetSignalWaveForPolyphonicSignal(comboBox5, a5, ph5, fr5, dc5, t5, null);
                        if (tempWave != null) waves.Add(tempWave);
                        break;
                }

            _signalWavePoints = generatePolyphonicSignal(waves);
            DrawSignalWave(_signalWavePoints, _signalWavePlotModel, SignalWavePlot);
        }

        private double[] generatePolyphonicSignal(List<ISignalWave> waves)
        {
            var maxSize = 0;
            var wavesStream = waves.AsEnumerable();
            foreach (var wave in wavesStream) maxSize = wave.Values.Length > maxSize ? wave.Values.Length : maxSize;
            var result = new double[maxSize];
            foreach (var wave in wavesStream)
                for (var i = 0; i < wave.Values.Length; i++)
                    result[i] += wave.Values[i];

            return result;
        }

        private ISignalWave GetSignalWaveForPolyphonicSignal(
            ComboBox comboBox,
            TextBox textBoxAmplitude,
            TextBox textBoxPhase,
            TextBox textBoxFrequency,
            TextBox textBoxDutyCycle,
            TextBox textBoxTime,
            TextBox textBoxSampling
        )
        {
            if (comboBox.SelectedIndex == -1) return null;

            double amplitude = 1, phase = 0, frequency = 1;
            var signalWaveType = (SignalWaveType) comboBox.SelectedIndex;
            if (signalWaveType != SignalWaveType.NOISE)
            {
                phase = double.Parse(textBoxPhase.Text);
                frequency = double.Parse(textBoxFrequency.Text);
            }
            amplitude = double.Parse(textBoxAmplitude.Text);

            ISignalWave signalWave;
            switch (signalWaveType)
            {
                case SignalWaveType.HARMONIC:
                    signalWave = new HarmonicSignalWave(amplitude, phase, frequency);
                    break;
                case SignalWaveType.SQUARE:
                    signalWave = new SquareSignalWave(amplitude, phase, frequency);
                    break;
                case SignalWaveType.TRIANGLE:
                    signalWave = new TriangleSignalWave(amplitude, phase, frequency);
                    break;
                case SignalWaveType.SAWTOOTH:
                    signalWave = new SawtoothSignalWave(amplitude, phase, frequency);
                    break;
                case SignalWaveType.NOISE:
                    signalWave = new Noise(amplitude);
                    break;
                case SignalWaveType.PULSEWITHDIFFERENTDUTYCYCLE:
                    var dutyCycle = float.Parse(textBoxDutyCycle.Text);
                    signalWave = new PulseWithDifferentDutyCycle(amplitude, dutyCycle, frequency);
                    break;
                default:
                    signalWave = new HarmonicSignalWave(amplitude, phase, frequency);
                    break;
            }

             TIME = textBoxTime == null ? 1 : int.Parse(textBoxTime.Text);
            _sampling = textBoxSampling == null ? SAMPLING : int.Parse(textBoxSampling.Text);
            _signalWavePoints = signalWave.GenerateSignalWaveDots(TIME, _sampling);

            return signalWave;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PlaySignal("result.wave", 44100);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ISignalWave carrierWave = GetSignalWaveForPolyphonicSignal(cs_type, cs_a, cs_ph, cs_fr, cs_dc, textBox_modulationTime, textBox_modulationSampling);
            ISignalWave modulationWave = GetSignalWaveForPolyphonicSignal(ms_type, ms_a, ms_ph, ms_fr, ms_dc, textBox_modulationTime, textBox_modulationSampling);
           
            var modulationType = (ModulationType) ComboBox_modulationType.SelectedIndex;
            if (modulationType == ModulationType.AMPLITIDE)
            {
                _signalWavePoints = AmplitudeModulation(carrierWave, modulationWave); 
            }
            else
            {
                _signalWavePoints = carrierWave.FrequencyModulation(modulationWave, _sampling);
            }
            DrawSignalWave(_signalWavePoints, _signalWavePlotModel, SignalWavePlot);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            PlaySignal("modulation.wave", _sampling);
        }

        private void PlaySignal(String fileName, int sampleRate)
        {
            _generator.WriteWaveFile(fileName, _signalWavePoints, sampleRate);
            _player = new SoundPlayer(fileName);
            _player.Play();
        }
        
        double[] AmplitudeModulation(ISignalWave carrierSignal, ISignalWave modulationSignal)
        {
            for (int i = 0; i < carrierSignal.Values.Length; i++)
            {
                carrierSignal.Values[i] *= (1 + M * modulationSignal.Values[i]);
            }

            return carrierSignal.Values;
        }

        private void b_start_fourier_Click(object sender, EventArgs e)
        {
            var selectedSignalWave = GetSignalWaveForPolyphonicSignal(comboBoxForFourier, textBox1, textBox2, textBox3, textBox4, null, null);
            var signalWavePoints = selectedSignalWave.GenerateSignalWaveDots(TIME, SAMPLING - 1);
            DrawSignalWave(signalWavePoints, _signalPlotModel, plotSignal);

            Complex[] spectrum = fourierTransformer.DiscreteFourierTransform(signalWavePoints);
            double[] signalFT = fourierTransformer.InverseDiscreteFourierTransform(spectrum);
            DrawSignalWave(signalFT, _signalFTPlotModel, plotSignalAfterFT);

            double[] amplitudeSpectrum = SpectrumComputer.ComputeAmplitudeSpectrum(spectrum);
            DrawSpectrum(amplitudeSpectrum, _amplPlotModel, plotAmpl);

            double[] phaseSpectrum = SpectrumComputer.ComputePhaseSpectrum(spectrum);
            DrawSpectrum(phaseSpectrum, _phasePlotModel, plotPh);

            double l_fr = 0;
            double h_fr = 0;
            try
            {
                 l_fr = int.Parse(lFr.Text);
                 h_fr = int.Parse(hFr.Text);
            }
            catch(FormatException ex)
            {

            }


            DrawSignalWave(selectedSignalWave.Values, _signalPlotModel_f, plot0);
            double[] filtaredSignal = DoFiltration(selectedSignalWave.Values, filterType1, l_fr, h_fr);
            DrawSignalWave(filtaredSignal, _filterPlotModel_f, plotff);

            Complex[] spectrum_f = fourierTransformer.DiscreteFourierTransform(filtaredSignal);
            double[] amplitudeSpectrum_f = SpectrumComputer.ComputeAmplitudeSpectrum(spectrum_f);
            DrawSpectrum(amplitudeSpectrum_f, _amplPlotModel_f, plotfa);
            double[] phaseSpectrumFiltared = SpectrumComputer.ComputePhaseSpectrum(spectrum_f);
            DrawSpectrum(phaseSpectrumFiltared, _phasePlotModel_f, plotfp);


        }

        private void fourierPLG_Click(object sender, EventArgs e)
        {
            var waves = new List<ISignalWave>();
            ISignalWave tempWave;
            for (var i = 1; i < 6; i++)
                switch (i)
                {
                    case 1:
                        tempWave = GetSignalWaveForPolyphonicSignal(comboBox1, a1, ph1, fr1, dc1, t1, null);
                        if (tempWave != null) waves.Add(tempWave);
                        break;
                    case 2:
                        tempWave = GetSignalWaveForPolyphonicSignal(comboBox2, a2, ph2, fr2, dc2, t2, null);
                        if (tempWave != null) waves.Add(tempWave);
                        break;
                    case 3:
                        tempWave = GetSignalWaveForPolyphonicSignal(comboBox3, a3, ph3, fr3, dc3, t3, null);
                        if (tempWave != null) waves.Add(tempWave);
                        break;
                    case 4:
                        tempWave = GetSignalWaveForPolyphonicSignal(comboBox4, a4, ph4, fr4, dc4, t4, null);
                        if (tempWave != null) waves.Add(tempWave);
                        break;
                    case 5:
                        tempWave = GetSignalWaveForPolyphonicSignal(comboBox5, a5, ph5, fr5, dc5, t5, null);
                        if (tempWave != null) waves.Add(tempWave);
                        break;
                }

            _signalWavePoints = generatePolyphonicSignal(waves);
            DrawSignalWave(_signalWavePoints, _signalPlotModel, plotSignal);

            Complex[] spectrum = fourierTransformer.DiscreteFourierTransform(_signalWavePoints);
            double[] signalFT = fourierTransformer.InverseDiscreteFourierTransform(spectrum);
            DrawSignalWave(signalFT, _signalFTPlotModel, plotSignalAfterFT);

            double[] amplitudeSpectrum = SpectrumComputer.ComputeAmplitudeSpectrum(spectrum);
            DrawSpectrum(amplitudeSpectrum, _amplPlotModel, plotAmpl);

            double[] phaseSpectrum = SpectrumComputer.ComputePhaseSpectrum(spectrum);
            DrawSpectrum(phaseSpectrum, _phasePlotModel, plotPh);

            double l_fr = 0;
            double h_fr = 0;
            try
            {
                l_fr = int.Parse(lf_p.Text);
                h_fr = int.Parse(hf_p.Text);
            }
            catch (FormatException ex)
            {

            }

            DrawSignalWave(_signalWavePoints, _signalPlotModel_f, plot0);
            double[] filtaredSignal = DoFiltration(_signalWavePoints, filterType, l_fr, h_fr);
            DrawSignalWave(filtaredSignal, _filterPlotModel_f, plotff);

            Complex[] spectrum_f = fourierTransformer.DiscreteFourierTransform(filtaredSignal);
            double[] amplitudeSpectrum_f = SpectrumComputer.ComputeAmplitudeSpectrum(spectrum_f);
            DrawSpectrum(amplitudeSpectrum_f, _amplPlotModel_f, plotfa);
            double[] phaseSpectrumFiltared = SpectrumComputer.ComputePhaseSpectrum(spectrum_f);
            DrawSpectrum(phaseSpectrumFiltared, _phasePlotModel_f, plotfp);
        }

        private double[] DoFiltration(double[] signal, ComboBox comboBox, double l, double h)
        {
            var Type = (Filter.FilterType)comboBox.SelectedIndex;
            switch (Type)
            {
                case Filter.FilterType.LowPass:
                    return Filter.Filter.Low(signal, SAMPLING, l);
                case Filter.FilterType.HighPass:
                    return Filter.Filter.High(signal, SAMPLING, h);
                case Filter.FilterType.BandPass:
                    return Filter.Filter.Band(signal, SAMPLING, l, h);
                default:
                    return Filter.Filter.Band(signal, SAMPLING, l, h);
            }
        }
    }
}