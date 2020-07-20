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
    public class CustomerController : ControllerBase
    {
        private readonly TABCUSTRepository custRepository;

        public CustomerController(TABCUSTRepository custRepository)
        {
            this.custRepository = custRepository;
        }

        public IActionResult Get()
        {
            var response = custRepository.GetAll();
            if (response != null)
            {
                return Ok(response);
            }

            return NotFound("Item not found");
        }
    }
}
