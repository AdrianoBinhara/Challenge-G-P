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

        public Task<ItemsModel> AddItem()
        {
            throw new NotImplementedException();
        }

        public void DeleteItem(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ItemsModel>> GetItems()
        {
            var result = new List<ItemsModel>();
            var itemsApi = RestService.For<IInventoryClient>(AppSettings.BaseUrl);
           
            try
            {
                result = await itemsApi.GetItems();
            }
            catch (Exception ex)
            {
                ;
            }

            if (result.Any() && result != null)
                return result;

            return result;
        }

        public void UpdateItem(Guid id, Items items)
        {
            throw new NotImplementedException();
        }
    }
}
