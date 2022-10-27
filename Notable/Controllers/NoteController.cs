using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notable.Models;
using Notable.Repositories;
using System;
using System.Collections.Generic;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Notable.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NoteController : ControllerBase
    {
        private readonly INoteRepository _noteRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUserProfileRepository _profileRepository;
        public NoteController(INoteRepository noteRepository,
            ICategoryRepository categoryRepository,
            IUserProfileRepository profileRepository)
        {
            _noteRepository = noteRepository;
            _categoryRepository = categoryRepository;
            _profileRepository = profileRepository;
        }

        [HttpGet("usernotes/{userId}")]
        public IActionResult GetByUser(int userId)
        {
            return Ok(_noteRepository.GetAllByUser(userId));
        }


        [HttpGet("categorynotes/{categoryId}")]
        public IActionResult GetNotes(int categoryId)
        {
            return Ok(_categoryRepository.GetNotes(categoryId));
        }

        // POST: api/Note/categoryaddd
        [HttpPost("categoryadd")]
        public IActionResult AddToCategory(CategoryNote cn)
        {
            _noteRepository.AddCategoryNote(cn);
            return NoContent();
        }

        // GET: api/Note
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_noteRepository.GetAll());
        }

        // GET api/Note/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Note note = _noteRepository.GetById(id);
            if (note == null)
            {
                return NotFound();
            }
            note.Belongs = (Authentication.GetCurrentUserProfile(User, _profileRepository).Id == note.UserProfileId);
            return Ok(note);
        }

        // POST api/Note
        [HttpPost]
        public IActionResult Post([FromBody] Note note)
        {
            var up = Authentication.GetCurrentUserProfile(User, _profileRepository);
            if (up != null)
            {
                note.UserProfileId = up.Id;
                _noteRepository.Add(note);
                return CreatedAtAction(nameof(Get), note.Id, note);
            }
            return BadRequest();
        }

        // PUT api/Note/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, Note note)
        {
            if (id != note.Id)
            {
                return BadRequest();
            }

            _noteRepository.Update(note);
            return NoContent();
        }

        // DELETE api/Note/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _noteRepository.Delete(id);
            return NoContent();
        }


    }
}
