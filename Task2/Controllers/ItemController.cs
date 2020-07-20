using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Task2.DataAccess.Repository;

namespace Task2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly TABITEMRepository itemRepository;

        public ItemController(TABITEMRepository itemRepository)
        {
            this.itemRepository = itemRepository;
        }

        public IActionResult Get()
        {
            var response = itemRepository.GetAll();

            if(response != null)
            {
                return Ok(response);
            }

            return BadRequest("No item found");
        }
    }
}
