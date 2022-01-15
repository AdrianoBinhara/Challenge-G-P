using System;
using System.ComponentModel.DataAnnotations;

namespace GP_API.Controllers
{
    public class Item
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage ="O campo Nome é obrigatório")]
        [StringLength(200, ErrorMessage = "O nome não pode passar de 200 caracteres")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O campo Quantidade é obrigatório")]
        public int Quantity { get; set; }
    }   
}