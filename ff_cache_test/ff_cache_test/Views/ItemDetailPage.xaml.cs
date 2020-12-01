using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using ff_cache_test.Models;
using ff_cache_test.ViewModels;
using System.Diagnostics;
using System.Collections.Generic;

namespace ff_cache_test.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class ItemDetailPage : ContentPage
    {
        ItemDetailViewModel viewModel;

        Stopwatch _sw;

        Dictionary<object, (TimeSpan finish, TimeSpan success, bool failed)> _dic = new Dictionary<object, (TimeSpan finish, TimeSpan success, bool failed)>();

        public ItemDetailPage(ItemDetailViewModel viewModel)
        {
            InitializeComponent();
            _dic[this.m0] = (TimeSpan.Zero, TimeSpan.Zero, false);
            _dic[this.m1] = (TimeSpan.Zero, TimeSpan.Zero, false);
            _dic[this.m2] = (TimeSpan.Zero, TimeSpan.Zero, false);
            _dic[this.m3] = (TimeSpan.Zero, TimeSpan.Zero, false);

            _sw = new Stopwatch();
            _sw.Start();
            UpdateOut();

            BindingContext = this.viewModel = viewModel;
        }

        public ItemDetailPage()
        {
            InitializeComponent();

            var item = new Item
            {
                Text = "Item 1",
                Description = "This is an item description."
            };

            viewModel = new ItemDetailViewModel(item);
            BindingContext = viewModel;
        }

        private void OnFinish(object sender, FFImageLoading.Forms.CachedImageEvents.FinishEventArgs e)
        {
            var v = _dic[sender];
            _dic[sender] = (this._sw.Elapsed, v.success, v.failed);
            UpdateOut();
        }

        private void OnSuccess(object sender, FFImageLoading.Forms.CachedImageEvents.SuccessEventArgs e)
        {
            var v = _dic[sender];
            _dic[sender] = (v.finish, this._sw.Elapsed, v.failed);
            UpdateOut();
        }

        private void UpdateOut()
        {
            Dispatcher.BeginInvokeOnMainThread(() =>
            {
                string get(string name, (TimeSpan finish, TimeSpan success, bool failed) a)
                {
                    return $"{name}: finish[{a.finish.TotalMilliseconds}ms]; success[{a.success.TotalMilliseconds}ms]; failed[{a.failed}]";
                }

                var res0 = get(nameof(this.m0), _dic[m0]);
                var res1 = get(nameof(this.m1), _dic[m1]);
                var res2 = get(nameof(this.m2), _dic[m2]);
                var res3 = get(nameof(this.m3), _dic[m3]);

                this.times.Text = $"{res0}\n{res1}\n{res2}\n{res3}";
            });
        }
    }
}