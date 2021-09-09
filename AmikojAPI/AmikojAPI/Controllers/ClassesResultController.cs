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
    public class ClassesResultController : Controller
    {
        private readonly Amikoj_DBContext _context;

        public ClassesResultController(Amikoj_DBContext context)
        {
            _context = context;
        }

        // GET: api/Classes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClassResultModel>>> GetAllClassResults()
        {
            return await _context.ClassResult.ToListAsync();
        }

        // GET: api/Classes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClassResultModel>> GetClassResult(int id)
        {
            var Class = await _context.ClassResult.FirstOrDefaultAsync(u => u.Id == id);

            if (Class == null)
            {
                return NotFound();
            }

            return Class;
        }

        // GET: api/Classes/5
        [HttpGet("{id}/{userId}")]
        public async Task<ActionResult<List<ClassResultModel>>> GetClassResultbyChapterId(int id, string userId)
        {
            var Class = await _context.ClassResult.Where<ClassResultModel>(u => u.Id == id && u.UserId == userId).ToListAsync();

            if (Class == null)
            {
                return NotFound();
            }

            return Class;
        }

        // PUT: api/Classes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutClassResult(ClassResultModel Class)
        {
            try
            {
                _context.Entry(Class).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClassExists(Class.Id))
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

        // POST: api/Classes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ClassResultModel>> PostClassResult(ClassResultModel classResultModel)
        {

            var classResult = await _context.ClassResult.Where(c => c.ClassNumber == classResultModel.ClassNumber && c.ChapterNumber == classResultModel.ChapterNumber && c.LearnLangCode.Equals(classResultModel.LearnLangCode) && c.MyLangCode.Equals(classResultModel.MyLangCode)).FirstOrDefaultAsync();
            if (classResult != null)
            {
                return Conflict();
            }
            _context.ClassResult.Add(classResultModel);
            await _context.SaveChangesAsync();
            classResult = await _context.ClassResult.Where(c => c.ClassNumber == classResultModel.ClassNumber && c.ChapterNumber == classResultModel.ChapterNumber && c.LearnLangCode.Equals(classResultModel.LearnLangCode) && c.MyLangCode.Equals(classResultModel.MyLangCode)).FirstOrDefaultAsync();
            if (classResult != null)
            {
                return Ok(classResult);
            }
            else
            {
                return BadRequest();
            }
        }

        // DELETE: api/Classes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClassResult(int id)
        {
            var Class = await _context.ClassResult.FindAsync(id);
            if (Class == null)
            {
                return NotFound();
            }

            _context.ClassResult.Remove(Class);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClassExists(int id)
        {
            return _context.Classes.Any(e => e.Id == id);
        }
    }
}
