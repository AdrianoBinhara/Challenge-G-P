using System;
using System.Collections.Generic;
using System.Linq;
using GP_API.Controllers;
using GP_API.Models;

namespace GP_API.Service
{
    public class InventoryService : IInventoryService
    {
        private readonly List<Item> Items;
        private readonly ApiContext _context;

        public InventoryService(ApiContext context)
        {
            _context = context;
        }

        public void AddItem(Item item)
        {
            item.Id = Guid.NewGuid();

            _context.items.Add(item);
            _context.SaveChanges();
        }

        public void DeleteItem(Guid id)
        {
            var model = _context.items.Where(m => m.Id == id).FirstOrDefault();
            _context.items.Remove(model);
            _context.SaveChanges();
        }

        public Item GetItem(Guid id)
        {
            return _context.items.Where(m => m.Id == id).FirstOrDefault();
        }

        public List<Item> GetItems()
        {
            return _context.items.ToList();
        }

        public bool ItemExists(Guid id)
        {
            return _context.items.Any(m => m.Id == id);
        }

        public void UpdateItem(Item item)
        {
            var model = _context.items.Where(m => m.Id == item.Id).FirstOrDefault();

            model.Quantity = item.Quantity;
            model.Name = item.Name;
            model.Category = item.Category;
            model.IsUpdated = 0;
            _context.items.Update(model);
            _context.SaveChanges();
        }
    }
}
