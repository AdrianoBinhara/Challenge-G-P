using System;
using System.Collections.Generic;
using GP_API.Controllers;

namespace GP_API.Service
{
    public interface IInventoryService
    {
        List<Item> GetItems();
        Item GetItem(Guid id);
        void AddItem(Item item);
        void UpdateItem(Item item);
        void DeleteItem(Guid id);
        bool ItemExists(Guid id);   
    }
}
