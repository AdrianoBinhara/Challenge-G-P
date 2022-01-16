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

        public void Sync(List<ItemsModel> Items)
        {
            Task.Run(async () =>
            {
                var itemsInDB = await _inventoryService.GetItems();
                var itemToExclude = itemsInDB.Where(p => !Items.Any(x => x.Id == p.Id));
                var itemsToAdd = Items.Where(p => !itemsInDB.Any(x => x.Id == p.Id));
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

                foreach (var item in Items)
                {
                    if (item.IsUpdated == 1)
                        await _inventoryService.UpdateItem(item.Id, item);
                    

                }
            });
        }
    }
}
