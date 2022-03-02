using System;
using System.Timers;

using System.Windows;
using System.Windows.Controls;

using ScreenPreview.Model;

namespace ScreenPreview.ViewModel {

    class MainViewModel {
        public MainViewModel() {
            ScreenRecorder.Init();

            ScreenImage = new Image();
            ScreenImage.Source = ScreenRecorder.WriteableBitmap;

            ScreenRefreshTimer = new Timer();
            ScreenRefreshTimer.Elapsed += UpdateImageSource;

            ScreenRefreshTimer.Interval = 1000.0f / 30.0f;
            ScreenRefreshTimer.Start();
        }

        private void UpdateImageSource(object sender, ElapsedEventArgs e) {
            if(Application.Current == null) {
                return;
            }

            Application.Current.Dispatcher.BeginInvoke(
                new Action(() => { 
                    ScreenRecorder.TakeScreenshot();
                })
            );
        }


        // Variables
        public Image ScreenImage { get; set; }
        public Timer ScreenRefreshTimer { get; set; }
    }

}