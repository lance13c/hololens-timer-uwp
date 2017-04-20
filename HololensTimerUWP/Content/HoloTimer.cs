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
            timeDisplay.KeyDown += TimeDisplay_KeyDown;
        }

        private void TimeDisplay_LostFocus(object sender, RoutedEventArgs e)
        {
            this.displayTime();
        }

        private void TimeDisplay_KeyDown(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            VirtualKey key = e.Key;
            if (e.Key >= VirtualKey.Number0 && e.Key <= VirtualKey.Number9)
            {
                // If text is length 2 add :
                if (this.timeDisplay.Text.Length == 2)
                {
                    this.timeDisplay.Text += ":";
                }
                // If length = 5 -> send answer to parser and display + unfocus
                if (this.timeDisplay.Text.Length == 5)
                {
                    this.SetTime(this.timeDisplay.Text);
                }
                //Debug.WriteLine(this.timeDisplay.Text);
                Debug.WriteLine("Digit");
            } else if(e.Key >= VirtualKey.A && e.Key <= VirtualKey.Z)
            {
                // Remove previously added item
                // this.timeDisplay.Text.Remove(this.timeDisplay.MaxLength - 1);
                Debug.WriteLine("Letter");
            }
            //switch (e.Key)
            //{
            //    case VirtualKey.Number0:
                               
            //        break;
            //    case VirtualKey.Number1:

            //        break;
            //    case VirtualKey.Number2:

            //        break;
            //    case VirtualKey.Number3:

            //        break;
            //    case VirtualKey.Number4:

            //        break;
            //    case VirtualKey.Number5:

            //        break;
            //    case VirtualKey.Number6:

            //        break;
            //    case VirtualKey.Number7:

            //        break;
            //    case VirtualKey.Number8:

            //        break;
            //    case VirtualKey.Number9:

            //        break;
            //    case VirtualKey.Back:
            //        Debug.WriteLine("back");
            //        break;
            //    default:
            //        //this.timeDisplay 
            //        break;
            //}
            Debug.WriteLine(this.timeDisplay.Text);

            //Debug.WriteLine("KeyDown");
            //string currentText = this.timeDisplay.Text;
            //char recentChar = '@';
            //if (currentText.Length > 0)
            //{
            //    recentChar = currentText.ToCharArray()[currentText.Length-1];
            //}

            //if (char.IsDigit(recentChar))
            //{
            //    Debug.WriteLine("Is Digit");
            //} else
            //{
            //    Debug.WriteLine(this.timeDisplay.Text);
            //}

        }

        private void TimeDisplay_GotFocus(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("On Focus");
            // Stop timer
            this.timer.Stop();

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
