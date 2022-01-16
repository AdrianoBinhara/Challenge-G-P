using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GPInventory.Entity;
using GPInventory.Models;

namespace GPInventory.Service
{
    public interface IInventoryService
    {
        Task<List<ItemsModel>> GetItems();

        Task<ItemsModel> AddItem(ItemsModel item);

        Task UpdateItem(Guid id, ItemsModel item);

        Task DeleteItem(ItemsModel id);
    }
}
