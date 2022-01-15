using System;
namespace GPInventory.Models
{
    public class ItemsModel
    {
        
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public string Category { get; set; }
        public int IsUpdated { get; set; }
    }

}
