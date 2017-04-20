using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace HololensTimerUWP.Content
{
    class HoloTimer
    {
        public int sec = 0;
        public int min = 5;
        public int defaultSec = 0;
        public int defaultMin = 5;

        DispatcherTimer timer;
        TextBox timeDisplay;

        public HoloTimer(TextBox timeDisplay)
        {
            this.timer = new DispatcherTimer();
            this.timer.Interval = new TimeSpan(0, 0, 1);
            this.timer.Tick += Timer_Tick;
            this.timeDisplay = timeDisplay;
            this.timeDisplay.GotFocus += TimeDisplay_GotFocus;
            this.timeDisplay.LostFocus += TimeDisplay_LostFocus;
        }

        private void TimeDisplay_LostFocus(object sender, RoutedEventArgs e)
        {
            this.displayTime();
        }

        private void TimeDisplay_GotFocus(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("On Focus");
            // Stop timer
            this.timer.Stop();
            this.timeDisplay.Text = "";

            // Set default text
            
            // Reset Input Text
        }

        private void Timer_Tick(object sender, object e)
        {
            Debug.WriteLine("tick");
            if (this.sec > 0)
            {
                this.sec--;
            } else if (this.sec == 0 && this.min > 0)
            {
                this.min--;
                this.sec = 59;
            } else
            {
                this.timer.Stop();
            }
            this.timeDisplay.Text = this.ToString();
        }

        public override string ToString()
        {
            return min.ToString("d2") + ":" + sec.ToString("d2");
        }

        public void toggleTimer()
        {
            if (this.timer.IsEnabled)
            {
                this.timer.Stop();
            } else
            {
                this.timer.Start();
            }
        }

        public bool IsEnable()
        {
            return this.timer.IsEnabled;
        }

        // Time - must be in (2 integers + : + 2 integers
        public void SetTime(string time)
        {
            if (time.Length == 4)
            {
                int min = int.Parse(time.Substring(0, 2));
                int sec = int.Parse(time.Substring(3, 5));

                this.SetTime(min, sec);
            } else
            {
                Debug.WriteLine("Error: Set time != 4");
            }
        }

        public void SetTime(int min, int sec)
        {
            this.min = min;
            this.sec = sec;
            this.defaultSec = sec;
            this.defaultMin = min;
            this.displayTime();
        }

        public void ResetTime()
        {
            this.timer.Stop();
            this.min = this.defaultMin;
            this.sec = this.defaultSec;
            this.displayTime();
        }

        public void displayTime()
        {
            this.timeDisplay.Text = this.ToString();
        }
    }
}
