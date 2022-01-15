using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GPInventory.Entity;
using GPInventory.Models;
using Refit;

namespace GPInventory.Service
{
    public interface IInventoryClient
    {
        [Get("/inventory")]
        Task<List<ItemsModel>> GetItems();

        [Post("/inventory")]
        Task<ItemsModel> AddItem();
        
        [Put("/inventory/{id}")]
        Task UpdateItem(Guid id, Items items);

        [Put("/inventory/{id}")]
        Task DeleteItem(Guid id);
    }
}
