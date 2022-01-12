using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GPInventory.Entity
{ 
    [Table("Items")]
    public class Items
    {
        [Key]
        [DataType("int")]
        [Column("Id")]
        public int Id { get; set; }

        [Required]
        [DataType("nvarchar(200)")]
        [Column("Name")]
        public string Name { get; set; }
        public string Quantity { get; set; }
    }

}
