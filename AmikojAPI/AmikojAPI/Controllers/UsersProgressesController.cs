using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AmikojApi.Models;

namespace AmikojApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersProgressesController : ControllerBase
    {
        private readonly Amikoj_DBContext _context;

        public UsersProgressesController(Amikoj_DBContext context)
        {
            _context = context;
        }

        // GET: api/UsersProgresses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsersProgress>>> GetUsersProgresses()
        {
            return await _context.UsersProgresses.ToListAsync();
        }

        // GET: api/UsersProgresses/5
        [HttpGet("{myLangCode}/{learnLangCode}/{userId}")]
        public async Task<ActionResult<UsersProgress>> GetUsersProgress(string userId, string learnLangCode, string myLangCode)
        {
            var usersProgress = await _context.UsersProgresses.Where(u => u.UserId == userId && u.LearnLangCode == learnLangCode && u.MyLangCode == myLangCode).FirstOrDefaultAsync();

            if (usersProgress == null)
            {
                return NotFound();
            }

            return usersProgress;
        }


        // GET: api/UsersProgresses/5
        [HttpGet("{userId}")]
        public async Task<ActionResult<List<UsersProgress>>> GetUsersProgressById(string userId)
        {
            var usersProgress = await _context.UsersProgresses.Where(u => u.UserId == userId).OrderBy(x => x.ChapterNumber).ToListAsync();

            if (usersProgress == null)
            {
                return NotFound();
            }

            return usersProgress;
        }

        // PUT: api/UsersProgresses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutUsersProgress(UsersProgress usersProgress)
        {
            try
            {
                _context.Entry(usersProgress).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsersProgressExists(usersProgress.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/UsersProgresses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UsersProgress>> PostUsersProgress(UsersProgress usersProgress)
        {
            try
            {
                var word = await _context.UsersProgresses.Where(c =>  c.ChapterNumber == usersProgress.ChapterNumber && c.LearnLangCode.Equals(usersProgress.LearnLangCode) && c.MyLangCode.Equals(usersProgress.MyLangCode)).FirstOrDefaultAsync();
                if (word != null)
                {
                    return Conflict(word);
                }
                _context.UsersProgresses.Add(word);
                await _context.SaveChangesAsync();
                word = await _context.UsersProgresses.Where(c => c.ChapterNumber == usersProgress.ChapterNumber && c.LearnLangCode.Equals(usersProgress.LearnLangCode) && c.MyLangCode.Equals(usersProgress.MyLangCode)).FirstOrDefaultAsync();
                if (word != null)
                {
                    return Ok(word);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (DbUpdateException)
            {
                return BadRequest();
            }
        }

        // DELETE: api/UsersProgresses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsersProgress(int id)
        {
            var usersProgress = await _context.UsersProgresses.FindAsync(id);
            if (usersProgress == null)
            {
                return NotFound();
            }

            _context.UsersProgresses.Remove(usersProgress);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UsersProgressExists(int id)
        {
            return _context.UsersProgresses.Any(e => e.Id == id);
        }
    }
}
