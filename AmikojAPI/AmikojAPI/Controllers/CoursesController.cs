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
    public class CoursesController : ControllerBase
    {
        private readonly Amikoj_DBContext _context;

        public CoursesController(Amikoj_DBContext context)
        {
            _context = context;
        }

        // GET: api/Courses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseModel>>> GetCourses()
        {
            return await _context.Courses.ToListAsync();
        }

        // GET: api/Courses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CourseModel>> GetCoursebyId(int id)
        {
            var course = await _context.Courses.FindAsync(id);

            if (course == null)
            {
                return NotFound();
            }

            return Ok(course);
        }

        
        // GET: api/Courses/5
        [HttpGet("{myLangCode}/{learnLangCode}")]
        public async Task<ActionResult<CourseModel>> GetCourseByLanguage(string MyLangCode, string LearnLangCode)
        {
            var wordModel = await _context.Courses.FirstOrDefaultAsync(u => u.MyLangCode.Equals(MyLangCode) && u.LearnLangCode.Equals(LearnLangCode));

            if (wordModel == null)
            {
                return NotFound();
            }

            return Ok(wordModel);
        }

        // PUT: api/Courses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutCourse(CourseModel course)
        {


            try
            {
                _context.Entry(course).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseExists(course.Id))
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

        // POST: api/Courses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CourseModel>> PostCourse(CourseModel courseModel)
        {
            try
            {
                var course = await _context.Courses.Where(c => c.LearnLangCode.Equals(courseModel.LearnLangCode) && c.MyLangCode.Equals(courseModel.MyLangCode)).FirstOrDefaultAsync();
                if (course != null)
                {
                    return Conflict(course);
                }
                _context.Courses.Add(courseModel);
                await _context.SaveChangesAsync();
                course = await _context.Courses.Where(c => c.LearnLangCode.Equals(courseModel.LearnLangCode) && c.MyLangCode.Equals(courseModel.MyLangCode)).FirstOrDefaultAsync();
                if (course != null)
                {
                    return Ok(course);
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

        // DELETE: api/Courses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CourseExists(int id)
        {
            return _context.Courses.Any(e => e.Id == id);
        }
    }
}
