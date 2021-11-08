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
    public class WordsController : ControllerBase
    {
        private readonly Amikoj_DBContext _context;

        public WordsController(Amikoj_DBContext context)
        {
            _context = context;
        }

        // GET: api/Words
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WordModel>>> GetDeWords()
        {
            return await _context.Words.ToListAsync();
        }

        // GET: api/Words/pt/en/1/1/1
        [HttpGet("{myLangCode}/{learnLangCode}/{chapterNum}/{classNum}/{orderNum}")]
        public async Task<ActionResult<WordModel>> GetWord(int chapterNum, int classNum, int orderNum, string learnLangCode, string myLangCode)
        {
            var wordModel = await _context.Words.FirstOrDefaultAsync(u => u.ChapterNumber == chapterNum && u.ClassNumber == classNum && u.OrderId == orderNum && u.MyLangCode == learnLangCode && u.LearnLangCode == myLangCode);

            if (wordModel == null)
            {
                return NotFound();
            }

            return Ok(wordModel);
        }

        // GET: api/Words/pt/en/1/1
        [HttpGet("{myLangCode}/{learnLangCode}/{chapterNum}/{classNum}")]
        public async Task<ActionResult<List<WordModel>>> GetWords(int chapterNum, int classNum, string learnLangCode, string myLangCode)
        {
            var wordModel = await _context.Words.Where<WordModel>(u => u.ChapterNumber == chapterNum && u.ClassNumber == classNum && u.MyLangCode == myLangCode && u.LearnLangCode == learnLangCode).ToListAsync();

            if (wordModel == null)
            {
                return NotFound();
            }

            return Ok(wordModel);
        }

        // PUT: api/Words/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutWord(WordModel wordModel)
        {


            try
            {
                if (WordExists(wordModel.Id))
                {
                    _context.Words.Update(wordModel);
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest();
            }

        }

        // POST: api/Words
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<WordModel>> PostWord(WordModel wordModel)
        {
            try
            {
                var word = await _context.Words.Where(c => c.OrderId == wordModel.OrderId && c.ClassNumber == wordModel.ClassNumber && c.ChapterNumber == wordModel.ChapterNumber && c.LearnLangCode.Equals(wordModel.LearnLangCode) && c.MyLangCode.Equals(wordModel.MyLangCode)).FirstOrDefaultAsync();
                if (word != null)
                {
                    return Conflict(word);
                }
                _context.Words.Add(wordModel);
                await _context.SaveChangesAsync();
                word = await _context.Words.Where(c => c.OrderId == wordModel.OrderId && c.ClassNumber == wordModel.ClassNumber && c.ChapterNumber == wordModel.ChapterNumber && c.LearnLangCode.Equals(wordModel.LearnLangCode) && c.MyLangCode.Equals(wordModel.MyLangCode)).FirstOrDefaultAsync();
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

        // DELETE: api/Words/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWord(int id)
        {
            try
            {
                var wordModel = await _context.Words.Where(w => w.Id == id).FirstOrDefaultAsync();
                if (wordModel == null)
                {
                    return NotFound();
                }

                _context.Words.Remove(wordModel);
                await _context.SaveChangesAsync();

                return Ok();

            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        private bool WordExists(int id)
        {
            return _context.Words.Any(e => e.Id == id);
        }
    }
}
