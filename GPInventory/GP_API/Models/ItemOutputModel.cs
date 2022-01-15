using System;
namespace GP_API.Models
{
    public class ItemOutputModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public int IsUpdated { get; set; }
        public string Category { get; set; }
        public DateTime LastModified { get; set; }
    }
}
