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
        [Get("/")]
        Task<List<ItemsModel>> GetItems();

        [Post("/")]
        Task<ItemsModel> AddItem ([Body] ItemsModel item);
        
        [Put("/{id}")]
        Task UpdateItem(Guid id,[Body] ItemsModel item);

        [Delete("/{id}")]
        Task DeleteItem(Guid id);
    }
}
