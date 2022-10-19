using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notable.Models;
using Notable.Repositories;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Notable.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly INoteRepository _noteRepository;

        public NoteController(INoteRepository userProfileRepository)
        {
            _noteRepository = userProfileRepository;
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
            return Ok(note);
        }

        // POST api/Note
        [HttpPost]
        public void Post(Note note)
        {
            _noteRepository.Add(note);
        }

        // PUT api/Note/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, Note note)
        {
            if(id != note.Id)
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
