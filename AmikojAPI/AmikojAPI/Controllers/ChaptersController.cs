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
    public class ChaptersController : ControllerBase
    {
        private readonly Amikoj_DBContext _context;

        public ChaptersController(Amikoj_DBContext context)
        {
            _context = context;
        }

        // GET: api/Chapters
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ChapterModel>>> GetDeChapters()
        {
            return await _context.Chapters.ToListAsync();
        }

        // GET: api/Chapters/pt/en
        [HttpGet("{myLangCode}/{learnLangCode}")]
        public async Task<ActionResult<List<ChapterModel>>> GetChapterModels(string learnLangCode, string myLangCode)
        {
            var chapterModel = await _context.Chapters.Where<ChapterModel>(u => u.LearnLangCode == learnLangCode && u.MyLangCode == myLangCode).ToListAsync();

            if (chapterModel == null || chapterModel.Count == 0)
            {
                return NotFound();
            }

            return Ok(chapterModel);
        }

        // GET: api/Chapters/pt/en/1
        [HttpGet("{myLangCode}/{learnLangCode}/{chapterNumber}")]
        public async Task<ActionResult<ChapterModel>> GetChapterModelbyLangAndNumb(string learnLangCode, string myLangCode, int chapterNumber)
        {
            var chapterModel = await _context.Chapters.Where<ChapterModel>(u => u.LearnLangCode == learnLangCode && u.MyLangCode == myLangCode && u.ChapterNumber == chapterNumber).FirstOrDefaultAsync();

            if (chapterModel == null)
            {
                return NotFound();
            }

            return Ok(chapterModel);
        }

        // GET: api/Chapters/5
        [HttpGet("{chapterId}")]
        public async Task<ActionResult<ChapterModel>> GetChapterModelById(int chapterId)
        {
            var chapterModel = await _context.Chapters.Where<ChapterModel>(u => u.Id == chapterId).FirstOrDefaultAsync();

            if (chapterModel == null)
            {
                return NotFound();
            }

            return Ok(chapterModel);
        }

        // PUT: api/Chapters/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutChapterModel(ChapterModel chapterModel)
        {
            try
            {
                var chapter = await _context.Chapters.Where<ChapterModel>(u => u.Id == chapterModel.Id).FirstOrDefaultAsync();
                if (chapter != null)
                {
                    chapter.NoOfClasses = chapterModel.NoOfClasses;
                    chapter.ChapterName = chapterModel.ChapterName;
                    chapter.ChapterDescription = chapterModel.ChapterDescription;
                    chapter.TranslateName = chapterModel.TranslateName;
                    chapter.TranslateDescription = chapterModel.TranslateDescription;
                    _context.Chapters.Update(chapter);
                    await _context.SaveChangesAsync();
                    chapter = await _context.Chapters.Where<ChapterModel>(u => u.Id == chapterModel.Id).FirstOrDefaultAsync();
                    return Ok(chapter);
                }
                else
                {
                    return NotFound();
                }

            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        // POST: api/Chapters
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ChapterModel>> PostChapterModel(ChapterModel chapterModel)
        {
            try
            {
                var chapter = await _context.Chapters.Where(c => c.ChapterNumber == chapterModel.ChapterNumber && c.LearnLangCode.Equals(chapterModel.LearnLangCode) && c.MyLangCode.Equals(chapterModel.MyLangCode)).FirstOrDefaultAsync();
                if (chapter != null)
                {
                    return Conflict(chapter);
                }
                _context.Chapters.Add(chapterModel);
                await _context.SaveChangesAsync();
                chapter = await _context.Chapters.Where(c => c.ChapterNumber == chapterModel.ChapterNumber && c.LearnLangCode.Equals(chapterModel.LearnLangCode) && c.MyLangCode.Equals(chapterModel.MyLangCode)).FirstOrDefaultAsync();
                if (chapter != null)
                {
                    return Ok(chapter);
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

        // DELETE: api/Chapters/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChapterModel(int id)
        {
            var chapterModel = await _context.Chapters.FindAsync(id);
            if (chapterModel == null)
            {
                return NotFound();
            }

            _context.Chapters.Remove(chapterModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ChapterModelExists(int id)
        {
            return _context.Chapters.Any(e => e.Id == id);
        }
    }
}
