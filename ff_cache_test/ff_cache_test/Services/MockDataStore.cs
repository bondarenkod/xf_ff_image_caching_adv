using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ff_cache_test.Models;

namespace ff_cache_test.Services
{
    public class MockDataStore : IDataStore<Item>
    {
        readonly List<Item> items;

        public static string[] ImageSet = new[]
        {
              "https://bdfss.blob.core.windows.net/shots/qemu-system-x86_64_10_09_2020__23_21_31__d66ef817-d96d-4e9f-95e2-464ddc0a57c9.png",
              "https://bdfss.blob.core.windows.net/shots/11_09_2020__08_47_54__53143889-9e40-48d9-ba64-45105bc41b88.jpg",
              "https://bdfss.blob.core.windows.net/shots/11_09_2020__08_47_54__5f2230e4-a798-470d-b8c7-336a85ce7f0f.jpg",
              "https://bdfss.blob.core.windows.net/shots/11_09_2020__08_47_54__e33454ec-5f30-4ab4-9366-d04199f74938.jpg",
        };

        public MockDataStore()
        {
            items = new List<Item>()
            {
                new Item
                {
                    Image0 = ImageSet[0],
                    Image1 = ImageSet[1],
                    Image2 = ImageSet[2],
                    Image3 = ImageSet[3],
                    Id = Guid.NewGuid().ToString(),
                    Text = "First item",
                    Description="This is an item description."
                },
            };
        }

        public async Task<bool> AddItemAsync(Item item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            var oldItem = items.Where((Item arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((Item arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Item> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}