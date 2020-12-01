using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ff_cache_test.Services;
using ff_cache_test.Views;
using FFImageLoading;
using FFImageLoading.Helpers;

namespace ff_cache_test
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            var config = new FFImageLoading.Config.Configuration()
            {
                ExecuteCallbacksOnUIThread = true,
                HttpReadTimeout = 120,
                VerboseLogging = true,
                VerboseLoadingCancelledLogging = true,
                VerboseMemoryCacheLogging = true,
                VerbosePerformanceLogging = true,
                Logger = new Logger()
            };
            ImageService.Instance.Initialize(config);

            DependencyService.Register<MockDataStore>();
            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }

    public class Logger : IMiniLogger
    {
        public void Debug(string message)
        {
            System.Diagnostics.Debug.WriteLine(message);
        }

        public void Error(string errorMessage)
        {
            System.Diagnostics.Debug.WriteLine(errorMessage);
        }

        public void Error(string errorMessage, Exception ex)
        {
        }
    }
}
