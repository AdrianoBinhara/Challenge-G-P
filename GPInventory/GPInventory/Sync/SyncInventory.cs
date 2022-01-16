using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GPInventory.Entity;
using GPInventory.Models;
using GPInventory.Service;

namespace GPInventory.Sync
{
    public class SyncInventory
    {
        protected IInventoryService _inventoryService;
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
                        await _inventoryService.DeleteItem(item.Id);
                    }

                if(itemsToAdd.Any())
                    foreach (var item in itemsToAdd)
                    {
                        await _inventoryService.AddItem(item);
                    }

                if(itemsToUpdate.Any())
                    foreach (var item in itemsToUpdate)
                    {
                        item.IsUpdated = 0;
                        await _inventoryService.UpdateItem(item.Id, item);
                    }
           
        }
    }
}
