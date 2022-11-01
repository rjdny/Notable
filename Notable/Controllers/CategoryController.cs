using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notable.Models;
using Notable.Repositories;
using System.Collections.Generic;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Notable.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoryController : ControllerBase
    {
        private readonly IUserProfileRepository _profileRepository;
        private readonly ICategoryRepository _categoryRepository;
        
        public CategoryController(ICategoryRepository categoryRepository,
            IUserProfileRepository userProfileRepository)
        {
            _categoryRepository = categoryRepository;
            _profileRepository = userProfileRepository; 
        }

        // GET: api/CategoryController
        [HttpGet]
        public IActionResult Get()
        {
            var up = Authentication.GetCurrentUserProfile(User, _profileRepository);
            if (up != null)
            {
                return Ok(_categoryRepository.GetAll(up.Id));
            }
            return BadRequest();
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
        public IActionResult Post(Category category)
        {
            var up = Authentication.GetCurrentUserProfile(User, _profileRepository);
            if (up != null)
            {
                category.UserProfileId = up.Id;
                _categoryRepository.Add(category);
                return CreatedAtAction(nameof(Get),category.Id,category);
            }
            return BadRequest();
        }

        // PUT api/CategoryController/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, Category category)
        {
            if (id != category.Id)
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
