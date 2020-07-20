using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Task2.DataAccess.Models;
using Task2.DataAccess.Repository;

namespace Task2.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SalesorderController : ControllerBase
    {
        private readonly SalesOrderRepository salesOrderRepository;

        public SalesorderController(SalesOrderRepository salesOrderRepository)
        {
            this.salesOrderRepository = salesOrderRepository;
        }

        #region SalesOrder
        [HttpGet()]
        public IActionResult Get()
        {
            var responseBody = salesOrderRepository.GetAllEXT();
            if (responseBody != null)
            {
                return Ok(responseBody);
            }

            return NotFound("Item not found");
        }

        [HttpPost]
        public IActionResult Post([FromBody] TABSORDER newOrder)
        {
            var newOrd = salesOrderRepository.CreateOrder(newOrder);

            if (newOrd.Result != null)
            {
                return new CreatedAtActionResult("Get", "Salesorder", new { orderId = newOrd.Result.SORDID }, newOrd.Result);
            }

            return BadRequest("Not created due to some errors");
        }

        [HttpGet("{orderId}")]
        public IActionResult Get(decimal orderId)
        {
            var response = salesOrderRepository.Get(orderId);
            if (response != null)
            {
                return Ok(response);
            }

            return NotFound("Item not found");
        }

        [HttpPatch("{orderId}")]
        public IActionResult Delete(decimal orderId)
        {
            var response = salesOrderRepository.Delete(orderId);
            if (response.Result == "DEL")
            {
                return Ok("Item deleted successfully");
            }
            return BadRequest("Error occured");
        }

        [HttpGet("{orderId}/Item")]
        public IActionResult GetAllItems(decimal orderId)
        {
            var responseBody = salesOrderRepository.GetAllItems(orderId);

            if (responseBody != null)
            {
                return Ok(responseBody);
            }

            return NotFound("Item not found");
        }

        [HttpPost("{orderId}/Item")]
        public IActionResult CreateItem([FromBody] TABSODATA newItem)
        {
            var newAddedItem = salesOrderRepository.AdditemToOrder(newItem);

            if (newAddedItem.Result != null)
            {
                return new CreatedAtActionResult("GetOrderItem", "Salesorder", new { orderId = newAddedItem.Result.SORDID, itemId = newAddedItem.Result.ITEMID }, newAddedItem.Result);
            }

            return BadRequest("Not created due to some errors");
        }

        [HttpGet("{orderId}/Item/{itemId}")]
        public IActionResult GetOrderItem(decimal orderId, decimal itemId)
        {
            var response = salesOrderRepository.GetOrderItem(orderId, itemId);

            if (response != null)
            {
                return Ok(response);
            }

            return NotFound("Item not found");
        }

        [HttpPut("{orderId}/Item/{itemId}")]
        public IActionResult UpdateOrderItem([FromBody] NewOrderItemPrice newPrices)
        {

            var response = salesOrderRepository.UpdateOrderItem(newPrices);

            if (response.Result.itemRate == newPrices.NewItemPrice && response.Result.totalRate == newPrices.NewTotalPrice)
            {
                return Ok("Updated successfully");
            }

            return Created("Error updating", "");
        }

        [HttpPatch("{orderId}/Item/{itemId}")]
        public IActionResult DeleteItem(decimal orderId, decimal itemId)
        {
            var response = salesOrderRepository.DeleteItem(orderId, itemId);
            if (response.Result == "DEL")
            {
                return Ok("Item deleted successfully");
            }
            return BadRequest("Error occured");
        }
        #endregion
    }
}
