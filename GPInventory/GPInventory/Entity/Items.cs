using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GPInventory.Entity
{ 
    [Table("Items")]
    public class Items
    {
        public Items()
        {

        }
        [Key]
        [DataType("int")]
        [Column("Guid")]
        public Guid Id { get; set; }

        [Required]
        [DataType("nvarchar(200)")]
        [Column("Name")]
        public string Name { get; set; }

        [Required]
        [DataType("int")]
        [Column("Quantity")]
        public int Quantity { get; set; }

        [DataType("nvarchar(30)")]
        [Column("Category")]
        public string Category { get; set; }

        [Required]
        [DataType("int")]
        [Column("IsUpdated")]
        public int IsUpdated { get; set; }

    }

}
