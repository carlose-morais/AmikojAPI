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
    public class ClassesController : ControllerBase
    {
        private readonly Amikoj_DBContext _context;

        public ClassesController(Amikoj_DBContext context)
        {
            _context = context;
        }

        // GET: api/Classes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClassesModel>>> GetDeClasses()
        {
            return await _context.Classes.ToListAsync();
        }

        // GET: api/Classes/pt/en/1
        [HttpGet("{myLangCode}/{learnLangCode}/{chapterNumber}")]
        public async Task<ActionResult<List<ClassesModel>>> GetClassesByLangAndChapterNumber(string learnLangCode, string myLangCode, int chapterNumber)
        {
            try
            {
                var classModel = await _context.Classes.Where(u => u.LearnLangCode == learnLangCode && u.MyLangCode == myLangCode && u.ChapterNumber == chapterNumber).ToListAsync();

                if (classModel == null)
                {
                    return NotFound();
                }

                return Ok(classModel);

            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        // GET: api/Classes/5
        [HttpGet("{myLangCode}/{learnLangCode}/{chapterNumber}/{classNumber}")]
        public async Task<ActionResult<ClassesModel>> GetClass(string learnLangCode, string myLangCode, int chapterNumber, int classNumber)
        {
            try
            {
                var classModel = await _context.Classes.Where<ClassesModel>(u => u.LearnLangCode == learnLangCode && u.MyLangCode == myLangCode && u.ChapterNumber == chapterNumber && u.ClassNumber == classNumber).FirstOrDefaultAsync();

                if (classModel == null)
                {
                    return NotFound();
                }

                return Ok(classModel);

            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // GET: api/Classes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClassesModel>> GetClassById(int id)
        {
            var Class = await _context.Classes.FirstOrDefaultAsync(u => u.Id == id);

            if (Class == null)
            {
                return NotFound();
            }

            return Class;
        }

        // GET: api/Classes/5
        [HttpGet("ChapterId={id}")]
        public async Task<ActionResult<List<ClassesModel>>> GetClassbyChapterId(int id)
        {
            var Class = await _context.Classes.Where<ClassesModel>(u => u.ChapterId == id).ToListAsync();

            if (Class == null)
            {
                return NotFound();
            }

            return Class;
        }

        // PUT: api/Classes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutClass(int id, ClassesModel Class)
        {
            if (id != Class.Id)
            {
                return BadRequest();
            }

            _context.Entry(Class).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClassExists(id))
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
        public async Task<ActionResult<ClassesModel>> PostClass(ClassesModel classModel)
        {
            try
            {
                var _class = await _context.Classes.Where(c => c.ClassNumber == classModel.ClassNumber && c.ChapterNumber == classModel.ChapterNumber && c.LearnLangCode.Equals(classModel.LearnLangCode) && c.MyLangCode.Equals(classModel.MyLangCode)).FirstOrDefaultAsync();
                if (_class != null)
                {
                    return Conflict(_class);
                }
                _context.Classes.Add(classModel);
                await _context.SaveChangesAsync();
                _class = await _context.Classes.Where(c => c.ClassNumber == classModel.ClassNumber && c.ChapterNumber == classModel.ChapterNumber && c.LearnLangCode.Equals(classModel.LearnLangCode) && c.MyLangCode.Equals(classModel.MyLangCode)).FirstOrDefaultAsync();
                if (_class != null)
                {
                    return Ok(_class);
                }
                else
                {
                    return BadRequest();
                }

            }
            catch (Exception)
            {

                return BadRequest();
            }

        }

        // DELETE: api/Classes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClass(int id)
        {
            var Class = await _context.Classes.FindAsync(id);
            if (Class == null)
            {
                return NotFound();
            }

            _context.Classes.Remove(Class);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClassExists(int id)
        {
            return _context.Classes.Any(e => e.Id == id);
        }
    }
}
