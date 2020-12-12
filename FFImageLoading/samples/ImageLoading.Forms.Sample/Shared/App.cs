using System;
using ff_cache_test;
using ff_cache_test.Services;
using ff_cache_test.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamvvm;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace FFImageLoading.Forms.Sample
{
    public class App : Application
    {
        public App()
        {
            App.Current.Resources = new ResourceDictionary()
            {
                { "CustomCacheKeyFactory", new CustomCacheKeyFactory() }
            };

            CachedImage.FixedOnMeasureBehavior = true;
            CachedImage.FixedAndroidMotionEventHandler = true;

            // Xamvvm init
            var factory = new XamvvmFormsFactory(this);
            factory.RegisterNavigationPage<MenuNavigationPageModel>(() => this.GetPageFromCache<MenuPageModel>());
            XamvvmCore.SetCurrentFactory(factory);
			//MainPage = this.GetPageFromCache<MenuNavigationPageModel>() as Page;

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

			//ImageService.Instance.LoadCompiledResource("loading.png").Preload();
			//ImageService.Instance.LoadUrl("http://loremflickr.com/600/600/nature?filename=simple.jpg").DownloadOnly();
		}

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}

