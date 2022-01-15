using System;
namespace GP_API.Models
{
    public class ItemInputModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public int IsUpdated { get; set; }
        public string Category { get; set; }
    }
}
