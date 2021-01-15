using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Count_clicks
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private int _duration { get; set; }
        private bool _isStartClicked { get; set; } = false;
        private bool _isEnded { get; set; } = false;
        private int _clicks { get; set; }
        private DispatcherTimer _timer;
        private TimeSpan _time;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void CountDown()
        {
            _time = TimeSpan.FromSeconds(_duration);
            _timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                labelCountdown.Content = _time.TotalSeconds.ToString() + " s";
                if (_time == TimeSpan.Zero)
                {
                    _isEnded = true;
                    _timer.Stop();
                }
                _time = _time.Add(TimeSpan.FromSeconds(-1));
            }, Application.Current.Dispatcher);
            _timer.Start();
        }

        private void sliderDuration_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _duration = (int)sliderDuration.Value;
            if (labelDurationValue != null)
            {
                labelDurationValue.Content = _duration.ToString() + " s";
            }
            if (labelCountdown != null && !_isStartClicked)
            {
                labelCountdown.Content = _duration.ToString() + " s";
            }
        }

        private void buttonStart_Click(object sender, RoutedEventArgs e)
        {
            if (!_isStartClicked)
            {
                _isStartClicked = true;
                _isEnded = false;
                CountDown();
                buttonStart.Content = "Click me";
            }
            else
            {
                if(!_isEnded)
                {
                    _clicks += 1;
                    labelClicks.Content = _clicks.ToString();
                }
            }
        }

        private void buttonRetry_Click(object sender, RoutedEventArgs e)
        {
            _isStartClicked = false;
            _isEnded = true;
            _timer.Stop();
            _clicks = 0;
            labelClicks.Content = "0";
            labelCountdown.Content = _duration.ToString() + " s";
            buttonStart.Content = "Start";
        }
    }
}
