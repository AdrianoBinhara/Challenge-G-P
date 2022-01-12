using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GPInventory.Entity;
using GPInventory.Models;

namespace GPInventory.Repository
{
    public class ItemsRepository
    {
        public Task<List<ItemsModel>> GetAllasync()
        {
            using (InventoryContext context = new InventoryContext())
            {
                return Task.FromResult(context.ItemsList
                    .Select(x => new ItemsModel()
                    {
                        Name = x.Name,
                        Quantity = x.Quantity,
                        Id = x.Id
                    })
                    .ToList());
            }
        }

        public Task Create(ItemsModel model)
        {
            using (InventoryContext context = new InventoryContext())
            {
                Items items = new Items()
                {
                    Id = model.Id,
                    Name = model.Name,
                    Quantity = model.Quantity
                };
                context.ItemsList.Add(items);
                context.SaveChanges();
            }
            return Task.CompletedTask;
        }
    }
}
