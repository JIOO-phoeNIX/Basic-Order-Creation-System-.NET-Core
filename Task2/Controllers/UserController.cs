using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Task2.DataAccess.Models;
using Task2.DataAccess.Repository;

namespace Task2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserRepository userRepository;
        private readonly JWTAuthManager jwtManager;

        public UserController(UserRepository userRepository, JWTAuthManager jwtManager)
        {
            this.userRepository = userRepository;
            this.jwtManager = jwtManager;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var responseBody = userRepository.GetAll();

            return Ok(responseBody);
        }

        [HttpPost("GetByDetails")]
        public IActionResult GetByDetails([FromBody] User user)
        {
            var response = userRepository.Get(user);
            if (response != null)
            {
                var token = jwtManager.GenerateToken(response);
                if (token == null)
                    return Unauthorized("Access denied");
                return Ok(token);
            }

            return BadRequest("No user found");
        }

        [HttpGet("{id}")]
        public IActionResult Get(decimal? id)
        {
            if (id == null)
            {
                return BadRequest("No id passed");
            }

            var responseBody = userRepository.GetById(id);

            if (responseBody == null)
            {
                return NotFound("User not found");
            }
            var token = jwtManager.GenerateToken(responseBody);
            if (token == null)
                return Unauthorized("Access denied");
            return Ok(token);
        }

        [HttpPost]
        public IActionResult Create([FromBody] User user)
        {
            var response = userRepository.Create(user);
            if (response != null)
            {
                var token = jwtManager.GenerateToken(response.Result);
                if (token == null)
                    return Unauthorized("Access denied");
                return new CreatedAtActionResult("Get", "User", new { id = response.Result.UserId }, token);
            }

            return BadRequest("Not created due to some errors");
        }
    }
}
