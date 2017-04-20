using HololensTimerUWP.Content;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace HololensTimerUWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        HoloTimer holoTimer; 

        public MainPage()
        {
            this.InitializeComponent();
            this.holoTimer = new HoloTimer(timeText);
            this.timeText.KeyDown += TimeText_KeyDown;
            Debug.WriteLine(this.holoTimer.ToString());
        }

        private void TimeText_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Enter)
            {
                Debug.WriteLine("Current Text: " + this.timeText.Text);
                bool outcome = this.InterpretInput(this.timeText.Text);
                Debug.WriteLine("Interpretation: " + outcome);
            }
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            //this.holoTimer.toggleTimer();
            this.holoTimer.toggleTimer();
            Debug.WriteLine("Start Button");
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            this.holoTimer.toggleTimer();
            if (this.holoTimer.IsEnable())
            {
                start.Content = "Stop";
            } else
            {
                start.Content = "Start";
            }
            Debug.WriteLine("Start Button");
        }

        // Interprets the input on a text change event for a givin time.
        // If successfully interpreted this will set and display the appropiate time.
        // input - string to interpret as time
        // returns a bool that represents if the data was successfully interpreted
        private bool InterpretInput(string input)
        {
            bool valid = true;  // Tells if an interpretation is valid. Every section sets this to true in the beginning.
            bool anyTrue = false; // If any interpretation is valid, this is set to true, the method returns true;
            int tempMin = 0;    // Min stored in temp -> in the end it is assigned to holoTimer
            int tempSec = 0;    // Second stored in temp -> in the end it is assigned to holoTimer
            int subStart = -1;
            int subEnd = -1;
            // 00:00

            // "4 min" & "04 min" & m, min, minute, minutes 
            // Valid: 444 min -> set 44 minutes
            // Valid: 3 min 2 sec -> set 3 minutes
            int minI = input.IndexOf("m");
            subStart = -1;
            subEnd = minI;


            char preChar1M = '!';
            char preChar2M= '!';
            char preChar3M = '!';


            // Must have either a digit or white space before "s"

            // 44 Minutes
            if (minI - 3 >= 0)
            {
                //subStart = secI - 3;
                preChar1M = input.ElementAt<char>(minI - 1);
                preChar2M = input.ElementAt<char>(minI - 2);
                preChar3M = input.ElementAt<char>(minI - 3);

                if (char.IsDigit(preChar3M))
                {
                    subStart = minI - 3;
                    subEnd = 3;
                }
                else if (char.IsDigit(preChar2M))
                {
                    subStart = minI - 2;
                    subEnd = 2;
                }
                else if (char.IsDigit(preChar1M))
                {
                    subStart = minI - 1;
                    subEnd = 1;
                }
            }
            // 44Minutes or "4 Minutes" or "4Minutes"
            else if (minI - 2 >= 0)
            {
                //subStart = secI - 2;
                preChar1M = input.ElementAt<char>(minI - 1);
                preChar2M = input.ElementAt<char>(minI - 2);
                if (char.IsDigit(preChar2M))
                {
                    subStart = minI - 2;
                }
                else if (char.IsDigit(preChar1M))
                {
                    subStart = minI - 1;
                }
            }
            // 4Minutes
            else if (minI - 1 >= 0)
            {
                preChar1M = input.ElementAt<char>(minI - 1);
                if (char.IsDigit(preChar1M))
                {
                    subStart = minI - 1;
                }
                else
                {
                    valid = false;
                }

            }
            else
            {
                valid = false;
            }
            if (valid)
            {
                try
                {
                    anyTrue = true;
                    string val = input.Substring(subStart, subEnd);
                    val = val.Trim();                                   // Remove white space
                    tempMin = int.Parse(val);                           // Might Error
                } catch (System.ArgumentOutOfRangeException e)
                {
                    Debug.WriteLine(e);
                }
            }

            // "4 sec" & "04 sec & 4 s & 4 seconds" & s, sec, second, seconds
            // Valid: 444 sec -> set 44 seconds
            // Valid: 3 min 2 sec -> set 2 seconds
            valid = true;
            int secI = input.IndexOf("sec");
            subStart = -1;
            subEnd = secI;
            char preChar1 = '!';
            char preChar2 = '!';
            char preChar3 = '!';


            // Must have either a digit or white space before "s"

            // 44 Minutes
            if (secI - 3 >= 0)
            {
                //subStart = secI - 3;
                preChar1 = input.ElementAt<char>(secI - 1);
                preChar2 = input.ElementAt<char>(secI - 2);
                preChar3 = input.ElementAt<char>(secI - 3);

                if (char.IsDigit(preChar3))
                {
                    subStart = secI - 3;
                    subEnd = 3;
                }
                else if (char.IsDigit(preChar2))
                {
                    subStart = secI - 2;
                    subEnd = 2;
                }
                else if (char.IsDigit(preChar1))
                {
                    subStart = secI - 1;
                    subEnd = 1;
                }
            }
            // 44Seconds or "4 Seconds" or "4Seconds"
            else if (secI - 2 >= 0)
            {
                //subStart = secI - 2;
                preChar1 = input.ElementAt<char>(secI - 1);
                preChar2 = input.ElementAt<char>(secI - 2);
                if (char.IsDigit(preChar2))
                {
                    subStart = secI - 2;
                } else if (char.IsDigit(preChar1))
                {
                    subStart = secI - 1;
                }
            }
            // 4Seconds
            else if (secI - 1 >= 0)
            {
                preChar1 = input.ElementAt<char>(secI - 1);
                if (char.IsDigit(preChar1))
                {
                    subStart = secI - 1;
                } else
                {
                    valid = false;
                }
                
            }
            else
            {
                valid = false;
            }
            if (valid)
            {
                try
                {
                    anyTrue = true;
                    string val = input.Substring(subStart, subEnd);
                    val = val.Trim();                                   // Remove white space
                    tempSec = int.Parse(val);                           // Might Error
                }
                catch (System.ArgumentOutOfRangeException e)
                {
                    Debug.WriteLine(e);
                }
            }

            // 4 minutes


            // Clear, Set, and Display
            if (anyTrue)
            {
                this.holoTimer.SetTime(tempMin, tempSec);
            }

            return anyTrue;
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            this.holoTimer.ResetTime();
        }
    }
}
