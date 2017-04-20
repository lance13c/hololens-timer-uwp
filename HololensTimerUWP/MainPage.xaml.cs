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
            int tempMin = 5;    // Min stored in temp -> in the end it is assigned to holoTimer
            int tempSec = 0;    // Second stored in temp -> in the end it is assigned to holoTimer
            
            // 00:00

            // "4 min" & "04 min" 
                // Valid: 444 min -> set 44 minutes
                // Valid: 3 min 2 sec -> set 3 minutes

            //Your code goes here
            int minI = input.IndexOf("min");
            int subStart = -1;
            int subEnd = minI - 1;


            if (minI - 3 >= 0)
            {
                subStart = minI - 3;
            }
            else if (minI - 2 >= 0)
            {
                subStart = minI - 2;
            }
            else
            {
                valid = false;
            }
            if (valid)
            {
                anyTrue = true;
                string val = input.Substring(subStart, subEnd);
                tempMin = int.Parse(val);                           // Might Error
                //Console.WriteLine("Hello, index! " + i.ToString());
                //Console.WriteLine("Hello, val! " + val);
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
