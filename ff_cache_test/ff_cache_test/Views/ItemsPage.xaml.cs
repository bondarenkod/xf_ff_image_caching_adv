using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using ff_cache_test.Models;
using ff_cache_test.Views;
using ff_cache_test.ViewModels;
using ff_cache_test.Services;
using FFImageLoading;

namespace ff_cache_test.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class ItemsPage : ContentPage
    {
        ItemsViewModel viewModel;

        public ItemsPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new ItemsViewModel();
        }

        async void OnItemSelected(object sender, EventArgs args)
        {
            var layout = (BindableObject)sender;
            var item = (Item)layout.BindingContext;
            await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(item)));
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewItemPage()));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Items.Count == 0)
                viewModel.IsBusy = true;
        }

        private async void cache_Clicked(object sender, EventArgs e)
        {
            var tasks = MockDataStore.ImageSet.Select(x => ImageService.Instance
                .LoadUrl(x)
                .Retry(3, 250)
                .DownSample(width: 50)
                .BitmapOptimizations(true)
                .WithCache(FFImageLoading.Cache.CacheType.All)
                .DownSampleMode(FFImageLoading.Work.InterpolationMode.Low)
                //.CacheKey(x + "w30p")
                .CacheKey(x)
                .Preload());
            output.Text = "loading items";
            await Task.WhenAll(tasks.Select(x => x.RunAsync()));
            output.Text = "items loaded";
        }

        private async void clearCache_Clicked(object sender, EventArgs e)
        {
            await ImageService.Instance.InvalidateCacheAsync(FFImageLoading.Cache.CacheType.All);
            output.Text = "All cache cleared";
        }

        private async void Disk_Clicked(object sender, EventArgs e)
        {
            await ImageService.Instance.InvalidateCacheAsync(FFImageLoading.Cache.CacheType.Disk);
            output.Text = "Disk cache cleared";
        }

        private async void Mem_Clicked(object sender, EventArgs e)
        {
            await ImageService.Instance.InvalidateCacheAsync(FFImageLoading.Cache.CacheType.Memory);
            output.Text = "Memory cache cleared";
        }
    }
}