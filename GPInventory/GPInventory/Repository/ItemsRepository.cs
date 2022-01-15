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
                        Category = x.Category,
                        Name = x.Name,
                        Quantity = x.Quantity,
                        Id = x.Id,
                        IsUpdated = x.IsUpdated
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
                    Category = model.Category,
                    Id = model.Id,
                    Name = model.Name,
                    Quantity = model.Quantity,
                    IsUpdated = 0
                    
                };
                context.ItemsList.Add(items);
                context.SaveChanges();
            } 
            return Task.CompletedTask;
        }

        public Task Update(ItemsModel model)
        {
            using (InventoryContext context = new InventoryContext())
            {
                var existentItem = context.ItemsList.Find(model.Id);
                existentItem.Name = model.Name;
                existentItem.Quantity = model.Quantity;
                existentItem.Category = model.Category;
                existentItem.IsUpdated = 1;
                context.ItemsList.Update(existentItem);
                context.SaveChanges();
            }
            return Task.CompletedTask;
        }

        public Task Delete(ItemsModel model)
        {
            using (InventoryContext context = new InventoryContext())
            {
                var existentItem = context.ItemsList.Find(model.Id);
                context.ItemsList.Remove(existentItem);
                context.SaveChanges();
            }
            return Task.CompletedTask;
        }
    }
}
