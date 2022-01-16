using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GPInventory.Core;
using GPInventory.Entity;
using GPInventory.Models;
using Refit;
using Xamarin.Forms;

namespace GPInventory.Service
{
    public class InventoryService : IInventoryService
    {

        public InventoryService()
        {
        }

        public async Task<ItemsModel> AddItem(ItemsModel item)
        {
            var itemsApi = RestService.For<IInventoryClient>(AppSettings.BaseUrl);
            var result = new ItemsModel();

            try
            {
                result = await itemsApi.AddItem(item);
            }
            catch
            {
                MessagingCenter.Send(this, "Sync");
            }
            return result;
        }

        public async Task DeleteItem(ItemsModel item)
        {
            try
            {
                var itemsApi = RestService.For<IInventoryClient>(AppSettings.BaseUrl);
                await itemsApi.DeleteItem(item.Id);
            }
            catch(Exception ex)
            {
                MessagingCenter.Send(this, "Sync");
            }

        }

        public async Task<List<ItemsModel>> GetItems()
        {
            var result = new List<ItemsModel>();
            var itemsApi = RestService.For<IInventoryClient>(AppSettings.BaseUrl);
           
            try
            {
                result = await itemsApi.GetItems();
            }
            catch
            {
                MessagingCenter.Send(this, "Sync");
            }

            if (result.Any() && result != null)
                return result;

            return result;
        }

        public async Task UpdateItem(Guid id, ItemsModel items)
        {
            try
            {
                var itemsApi = RestService.For<IInventoryClient>(AppSettings.BaseUrl);
                await itemsApi.UpdateItem(id, items);
                
            }
            catch
            {
                MessagingCenter.Send(this, "Sync");
            }
        }
    }
}
