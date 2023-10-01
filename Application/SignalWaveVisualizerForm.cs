using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using SignalGenerator.Signal;
using SignalGenerator.Signal.SignalType;
using SignalGenerator.Signal.SignalWave;
using SignalGenerator.Signal.Sound;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Media;
using System.Windows.Forms;

namespace SignalGenerator
{
    public partial class SignalWaveVisualizerForm : Form
    {
        private static PlotModel SignalWavePlotModel;
        private ISignalWave selectedSignalWave;
        private SoundPlayer player;
        private SoundGenerator generator = new SoundGenerator();
        private double[] signalWavePoints;

        public SignalWaveVisualizerForm()
        {
            InitializeComponent();
            comboBoxSignalWaveTypes.SelectedIndex = 0;

            SignalWavePlotModel = new PlotModel { Title = "Signal Wave" };
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (System.Text.RegularExpressions.Regex.IsMatch(textBox.Text, "[^0-9]"))
            {
                MessageBox.Show("Please enter only numbers.");
                textBox.Text = textBox.Text.Remove(textBox.Text.Length - 1);
            }
        }

        private void GenerateSignalWave(object sender, EventArgs e)
        {
            int Time = int.Parse(TextBox_Time.Text);
            int Sampling = int.Parse(TextBox_Sampling.Text);

            selectedSignalWave = getSignalWave();
            signalWavePoints = selectedSignalWave.GenerateSignalWaveDots(Time, Sampling);
            DrawSignalWave(signalWavePoints, Time, Sampling);
        }

        private ISignalWave getSignalWave()
        {
            double Amplitude = 1;
            double Phase = 0;
            double Frequency = 1;
            var signalWaveType = (SignalWaveType)comboBoxSignalWaveTypes.SelectedIndex;
            if (signalWaveType != SignalWaveType.NOISE) {
                Amplitude = Double.Parse(TextBox_Amplitude.Text);
                Phase = Double.Parse(TextBox_Phase.Text);
                Frequency = Double.Parse(TextBox_Frequency.Text);
            }

            ISignalWave selectedSignalWave;
            switch (signalWaveType)
            {
                case SignalWaveType.HARMONIC:
                    selectedSignalWave = new HarmonicSignalWave(Amplitude, Phase, Frequency);
                    break;
                case SignalWaveType.SQUARE:
                    selectedSignalWave = new SquareSignalWave(Amplitude, Phase, Frequency);
                    break;
                case SignalWaveType.TRIANGLE:
                    selectedSignalWave = new TriangleSignalWave(Amplitude, Phase, Frequency);
                    break;
                case SignalWaveType.SAWTOOTH:
                    selectedSignalWave = new SawtoothSignalWave(Amplitude, Phase, Frequency);
                    break;
                case SignalWaveType.NOISE:
                    selectedSignalWave = new Noise(Amplitude);
                    break;
                case SignalWaveType.PulseWithDifferentDutyCycle:
                    float dutyCycle = float.Parse(DutyCycle.Text);
                    selectedSignalWave = new PulseWithDifferentDutyCycle(Amplitude, dutyCycle, Frequency);
                    break;
                default:
                    selectedSignalWave = new HarmonicSignalWave(Amplitude, Phase, Frequency);
                    break;
            }

            return selectedSignalWave;
        }

        private void DrawSignalWave(double[] signalWavePoints, int Time , int Sampling)
        {
            LineSeries SignalWave = new LineSeries()
            {
                Color = OxyColor.FromRgb(47, 94, 94),
                StrokeThickness = 2,
                MarkerSize = 3,
                MarkerType = MarkerType.Circle,
            };

            for (int i = 0; i < signalWavePoints.Length; i++)
            {
                SignalWave.Points.Add(new DataPoint(i * (2f / signalWavePoints.Length), signalWavePoints[i]));
            }

            SignalWavePlotModel.Series.Clear();
            SignalWavePlotModel.Series.Add(SignalWave);
            SignalWavePlot.Model = SignalWavePlotModel;
            SignalWavePlot.InvalidatePlot(true);
        }

        private void b_play_Click(object sender, EventArgs e)
        {
            string filename = "result.wave";
            generator.WriteWaveFile(filename, signalWavePoints, signalWavePoints.Length);
            player = new SoundPlayer(filename);
            player.Play();
        }
    }
}
