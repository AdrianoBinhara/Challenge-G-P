using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GPInventory.Entity;
using GPInventory.Models;
using GPInventory.Repository;
using GPInventory.Service;
using Xamarin.Forms;

namespace GPInventory.Sync
{
    public class SyncInventory
    {
        protected IInventoryService _inventoryService;

        protected ItemsRepository ItemsRepository { get; } = new ItemsRepository();

        public SyncInventory(IInventoryService invertoryContext)
        {
            _inventoryService = invertoryContext;
        }

        public async Task Sync(List<ItemsModel> Items)
        {
            
            var itemsInDB = await _inventoryService.GetItems();
            var itemToExclude = itemsInDB.Where(x => Items.All(p => x.Id != p.Id));
            var itemsToAdd = Items.Where(p => itemsInDB.All(p2 => p2.Id != p.Id));
            var itemsToUpdate = Items.Where(p => p.IsUpdated == 1);
            if(itemToExclude.Any())
                foreach (var item in itemToExclude)
                {
                    await _inventoryService.DeleteItem(item);
                }

            if(itemsToAdd.Any())
                foreach (var item in itemsToAdd )
                {
                   var result = await _inventoryService.AddItem(item);
                }

            if(itemsToUpdate.Any())
                foreach (var item in itemsToUpdate)
                {
                    item.IsUpdated = 0;
                    await _inventoryService.UpdateItem(item.Id, item);
                }
            MessagingCenter.Send(this, "AlreadySync");
        }
    }
}
