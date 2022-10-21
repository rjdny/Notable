using Microsoft.AspNetCore.Mvc;
using Notable.Models;
using Notable.Repositories;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Notable.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        // GET: api/CategoryController
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_categoryRepository.GetAll());
        }

        // GET api/CategoryController/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Category note = _categoryRepository.GetById(id);
            if (note == null)
            {
                return NotFound();
            }
            return Ok(note);
        }

        // POST api/CategoryController
        [HttpPost]
        public void Post(Category category)
        {
            _categoryRepository.Add(category);
        }

        // PUT api/CategoryController/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, Category category)
        {
            if(id != category.Id)
            {
                return BadRequest();
            }
            _categoryRepository.Update(category);
            return NoContent();
        }

        // DELETE api/CategoryController/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _categoryRepository.Delete(id);
            return NoContent();
        }
    }
}
