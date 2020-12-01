using System;
using System.ComponentModel;
using System.Threading;
using Xamarin.Forms;

namespace ff_cache_test.Models
{
    public class Item : INotifyPropertyChanged
    {
        private string id;
        private string text;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Image0 { get; set; }
        public string Image1 { get; set; }
        public string Image2 { get; set; }
        public string Image3 { get; set; }


        public string Id
        {
            get => id;
            set
            {
                id = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Id)));
            }
        }
        public string Text
        {
            get => text; set
            {
                text = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Text)));
            }
        }
        public string Description { get; set; }

        public Item()
        {
            var t = new Timer((a) =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    this.Text = DateTime.Now.ToString("F");
                });
            }, null, 1000, 1000);
        }
    }
}