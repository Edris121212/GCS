﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace GCS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static string elevationFolder = "elevation/srtm";
        be.elevation.srtm.SRTMData elevation;
        int? result = 0;
        public MainWindow()
        {
            InitializeComponent();

            this.SourceInitialized += new EventHandler(mainWindow_SourceInitialized);

     

            //check elevation folder
            if (!Directory.Exists(elevationFolder))
            {
                Directory.CreateDirectory(elevationFolder);
            }
            //init srtm directory
            elevation = new be.elevation.srtm.SRTMData(elevationFolder);
            //get elevation data from lat and long
            result = elevation.GetElevation(40.9201024, 29.3137833);//visit this web site for elevation datas : https://dwtkns.com/srtm30m/


            //check gstreamer dll and load
            if (!File.Exists("libgstreamer-1.0-0.dll"))
            {
                File.WriteAllBytes("libgstreamer-1.0-0.dll", GCS.Properties.Resources.libgstreamer_1_0_0);
            }

            

            //test pipeline
            //camView.Pipeline = "videotestsrc ! video/x-raw, width=1280, height=720,framerate=25/1 ! clockoverlay ! videoconvert ! video/x-raw,format=BGRA ! appsink name=outsink";
            //camView.startVideo();

            //test counter
            Thread threadTimer = new Thread(new ThreadStart(threadTimer_DoWork));
            threadTimer.Start();

            
        }

        private double counter = 100;
        private void threadTimer_DoWork()
        {
            do
            {
                counter-= 0.1;

                if(counter < 0)
                {
                    counter = 100;
                }

                timeLine.RemainingTime = "00:00";

                timeLine.Percent = counter;
                //camView.Sembology = "MehmetCBGL\n" + counter.ToString();
                Thread.Sleep(50);
            } while (true);
        }
        private void takeSnapshot_Click(object sender, RoutedEventArgs e)
        {
            //camView.takeSnapshot();
        }


        private void mainWindow_SourceInitialized(object sender, EventArgs e)
        {
            timeLine.Width = mainWindow.Width;
            timeLine.LineWidth = timeLine.Width;
        }
    }
}
