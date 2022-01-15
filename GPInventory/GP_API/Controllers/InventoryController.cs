using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GP_API.Models;
using GP_API.Service;
using Microsoft.AspNetCore.Mvc;

namespace GP_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService _inventoryService;
        public InventoryController(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        [HttpGet]
        public IActionResult GetAllItems()
        {
            var model = _inventoryService.GetItems();
            var outputModel = ToOutputModel(model);
            return Ok(outputModel);
        }

        [HttpGet("{id}", Name = "GetItem")]
        public IActionResult Get(Guid id)
        {
            var model = _inventoryService.GetItem(id);
            if (model == null)
                return NotFound();

            var outputModel = ToOutputModel(model);
            return Ok(outputModel);
        }

        [HttpPost]
        public IActionResult AddItem([FromBody] ItemInputModel item)
        {
            if (item == null)
                return BadRequest();

            var model = ToDomainModel(item);
            _inventoryService.AddItem(model);

            var outputModel = ToOutputModel(model);
            return CreatedAtRoute("GetItem", new { id = outputModel.Id }, outputModel);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateItem(Guid id,[FromBody] ItemInputModel item)
        {
            if (item == null || id != item.Id)
                return BadRequest();

            if (!_inventoryService.ItemExists(id))
                return NotFound();

            var model = ToDomainModel(item);
            _inventoryService.UpdateItem(model);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteItem(Guid id)
        {
            if (!_inventoryService.ItemExists(id))
                return NotFound();

            _inventoryService.DeleteItem(id);
            return NoContent();
        }

        // Mapeamentos: modelos envia/recebe dados via API

        private ItemOutputModel ToOutputModel(Item model)
        {
            return new ItemOutputModel
            {
                Id = model.Id,
                Name = model.Name,
                Category = model.Category,
                IsUpdated = model.IsUpdated,
                Quantity =model.Quantity
            };
        }
        private List<ItemOutputModel> ToOutputModel(List<Item> model)
        {
            return model.Select(item=> ToOutputModel(item)).ToList();
        }

        private Item ToDomainModel(ItemInputModel inputModel)
        {
            return new Item
            {
                Id = inputModel.Id,
                Name = inputModel.Name,
                Category = inputModel.Category,
                IsUpdated = inputModel.IsUpdated,
                Quantity = inputModel.Quantity
            };
        }

        private ItemInputModel ToInputModel(Item model)
        {
            return new ItemInputModel
            {
                Id = model.Id,
                Name = model.Name,
                Category = model.Category,
                IsUpdated = model.IsUpdated,
                Quantity = model.Quantity
            };
        }
    }
}
