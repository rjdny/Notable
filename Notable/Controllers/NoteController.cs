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

        [HttpGet("notecategories/{noteId}")]
        public IActionResult GetCategories(int noteId)
        {
            return Ok(_categoryRepository.GetCategories(noteId));
        }

        // POST: api/Note/categoryadd
        [HttpPost("categoryadd")]
        public IActionResult AddToCategory([FromBody]CategoryNote cn)
        {
            var up = Authentication.GetCurrentUserProfile(User, _profileRepository);
            if(up == null)
            {
                return Unauthorized();
            }
            cn.UserProfileId = up.Id;
            _noteRepository.AddCategoryNote(cn);
            return NoContent();
        }


        [HttpDelete("categoryremove/{categoryNoteId}")]
        public IActionResult RemoveFromCateogory(int categoryNoteId)
        {
            _noteRepository.RemoveCategoryNote(categoryNoteId);
            return NoContent();
        }

        [HttpDelete("categoryremove/{categoryId}/{noteId}")]
        public IActionResult RemoveFromCateogory(int categoryId, int noteId)
        {
            _noteRepository.RemoveCategoryNote(categoryId, noteId);
            return NoContent();
        }


        // GET: api/Note
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_noteRepository.GetAll());
        }

        // GET: api/Note
        [HttpGet("public")]
        public IActionResult GetPublicNotes(string q)
        {
            if(string.IsNullOrEmpty(q))
                return Ok(_noteRepository.GetPublicNotes());

            return Ok(_noteRepository.GetPublicNotes(q));
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

            UserProfile up = Authentication.GetCurrentUserProfile(User, _profileRepository);
            note.Belongs = (up != null && up.Id == note.UserProfileId);
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
