using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using OxyPlot;
using OxyPlot.Series;
using SignalGenerator.Signal;
using SignalGenerator.Signal.SignalType;
using SignalGenerator.Signal.SignalWave;
using SignalGenerator.Signal.Sound;

namespace SignalGenerator
{
    public partial class SignalWaveVisualizerForm : Form
    {
        private static PlotModel _signalWavePlotModel;
        private readonly SoundGenerator _generator = new SoundGenerator();
        private SoundPlayer _player;
        private int _sampling;
        private ISignalWave _selectedSignalWave;
        private double[] _signalWavePoints;
        private double M = 1;

        public SignalWaveVisualizerForm()
        {
            InitializeComponent();
            comboBoxSignalWaveTypes.SelectedIndex = 0;

            _signalWavePlotModel = new PlotModel {Title = "Signal Wave"};
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            var textBox = sender as TextBox;
            if (Regex.IsMatch(textBox.Text, "[^0-9]"))
            {
                MessageBox.Show("Please enter only numbers.");
                textBox.Text = textBox.Text.Remove(textBox.Text.Length - 1);
            }
        }

        private void GenerateSignalWave(object sender, EventArgs e)
        {
            var time = int.Parse(TextBox_Time.Text);
            _sampling = int.Parse(TextBox_Sampling.Text);

            _selectedSignalWave = GetSignalWave();
            _signalWavePoints = _selectedSignalWave.GenerateSignalWaveDots(time, _sampling);
            DrawSignalWave(_selectedSignalWave.Values);
        }

        private ISignalWave GetSignalWave()
        {
            double amplitude = 1;
            double phase = 0;
            double frequency = 1;
            var signalWaveType = (SignalWaveType) comboBoxSignalWaveTypes.SelectedIndex;
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

        private void DrawSignalWave(double[] signalWavePoints)
        {
            var signalWave = new LineSeries
            {
                Color = OxyColor.FromRgb(47, 94, 94),
                StrokeThickness = 2,
                MarkerSize = 3,
                MarkerType = MarkerType.Circle
            };

            for (var i = 0; i < signalWavePoints.Length; i++)
                signalWave.Points.Add(new DataPoint(i * (2f / signalWavePoints.Length), signalWavePoints[i]));

            _signalWavePlotModel.Series.Clear();
            _signalWavePlotModel.Series.Add(signalWave);
            SignalWavePlot.Model = _signalWavePlotModel;
            SignalWavePlot.InvalidatePlot(true);
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
                        tempWave = getSignalWaveForPolyphonicSignal(comboBox1, a1, ph1, fr1, dc1, t1, null);
                        if (tempWave != null) waves.Add(tempWave);
                        break;
                    case 2:
                        tempWave = getSignalWaveForPolyphonicSignal(comboBox2, a2, ph2, fr2, dc2, t2, null);
                        if (tempWave != null) waves.Add(tempWave);
                        break;
                    case 3:
                        tempWave = getSignalWaveForPolyphonicSignal(comboBox3, a3, ph3, fr3, dc3, t3, null);
                        if (tempWave != null) waves.Add(tempWave);
                        break;
                    case 4:
                        tempWave = getSignalWaveForPolyphonicSignal(comboBox4, a4, ph4, fr4, dc4, t4, null);
                        if (tempWave != null) waves.Add(tempWave);
                        break;
                    case 5:
                        tempWave = getSignalWaveForPolyphonicSignal(comboBox5, a5, ph5, fr5, dc5, t5, null);
                        if (tempWave != null) waves.Add(tempWave);
                        break;
                }

            _signalWavePoints = generatePolyphonicSignal(waves);
            DrawSignalWave(_signalWavePoints);
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

        private ISignalWave getSignalWaveForPolyphonicSignal(
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

            var time = int.Parse(textBoxTime.Text);
            _sampling = textBoxSampling == null ? 44100 : int.Parse(textBoxSampling.Text);
            _signalWavePoints = signalWave.GenerateSignalWaveDots(time, _sampling);

            return signalWave;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PlaySignal("result.wave", 44100);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ISignalWave carrierWave = getSignalWaveForPolyphonicSignal(cs_type, cs_a, cs_ph, cs_fr, cs_dc, textBox_modulationTime, textBox_modulationSampling);
            ISignalWave modulationWave = getSignalWaveForPolyphonicSignal(ms_type, ms_a, ms_ph, ms_fr, ms_dc, textBox_modulationTime, textBox_modulationSampling);
           
            var modulationType = (ModulationType) ComboBox_modulationType.SelectedIndex;
            if (modulationType == ModulationType.AMPLITIDE)
            {
                _signalWavePoints = AmplitudeModulation(carrierWave, modulationWave); 
            }
            else
            {
                
            }
            DrawSignalWave(_signalWavePoints);
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
    }
}