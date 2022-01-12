using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace GP_API.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class InventoryController : ControllerBase
    {
        private static List<Item> itens = new List<Item>();
        private static int id = 1;

        [HttpPost]
        public void Sync([FromBody] Item item)
        {
            item.Id = id++;
            itens.Add(item);
        }
    }
}
