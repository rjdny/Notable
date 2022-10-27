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
    //[Authorize]
    public class UserProfileController : ControllerBase
    {
        private readonly IUserProfileRepository _userProfileRepository;

        public UserProfileController(IUserProfileRepository userProfileRepository)
        {
            _userProfileRepository = userProfileRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_userProfileRepository.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            UserProfile up = _userProfileRepository.GetById(id);
            if (up == null)
            {
                return NotFound();
            }
            return Ok(up);
        }

        [HttpGet("getbyfirebaseid/{firebaseUserId}")]
        public IActionResult Get(string firebaseUserId)
        {
            UserProfile up = _userProfileRepository.GetByFirebaseId(firebaseUserId);
            if (up == null)
            {
                return NotFound();
            }
            return Ok(up);
        }

        [HttpGet("DoesUserExist/{firebaseUserId}")]
        public IActionResult DoesUserExist(string firebaseUserId)
        {
            UserProfile up = _userProfileRepository.GetByFirebaseId(firebaseUserId);
            if (up == null)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpPost]
        public IActionResult Post(UserProfile profile)
        {
            _userProfileRepository.Add(profile);
            return CreatedAtAction("Get", new { id = profile.Id }, profile);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, UserProfile profile)
        {
            if (id != profile.Id)
            {
                return BadRequest();
            }

            _userProfileRepository.Update(profile);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _userProfileRepository.Delete(id);
            return NoContent();
        }

    }
}
