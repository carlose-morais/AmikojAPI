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
    public class SentencesController : ControllerBase
    {
        private readonly Amikoj_DBContext _context;

        public SentencesController(Amikoj_DBContext context)
        {
            _context = context;
        }

        // GET: api/Sentences
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SentenceModel>>> GetDeSentences()
        {
            return await _context.Sentences.ToListAsync();
        }

        // GET: api/Sentences/pt/en/1/1/1
        [HttpGet("{myLangCode}/{learnLangCode}/{chapterId}/{classId}/{orderId}")]
        public async Task<ActionResult<SentenceModel>> GetSentence(int chapterId, int classId, int orderId, string myLangCode, string learnLangCode)
        {
            try
            {
                var sentenceModel = await _context.Sentences.FirstOrDefaultAsync(u => u.Id == chapterId && u.Id == classId && u.OrderId == orderId && u.MyLangCode == myLangCode && u.LearnLangCode == learnLangCode);

                if (sentenceModel == null)
                {
                    return NotFound();
                }

                return Ok(sentenceModel);

            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

        // GET: api/Words/pt/en/1/1
        [HttpGet("{myLangCode}/{learnLangCode}/{chapterId}/{classId}")]
        public async Task<ActionResult<List<SentenceModel>>> GetSentences(int chapterId, int classId, string myLangCode, string learnLangCode)
        {
            var sentendeModel = await _context.Sentences.Where<SentenceModel>(u => u.Id == chapterId && u.Id == classId && u.MyLangCode == myLangCode && u.LearnLangCode == learnLangCode).ToListAsync();

            if (sentendeModel == null)
            {
                return NotFound();
            }

            return Ok(sentendeModel);
        }

        // PUT: api/Sentences/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutSentence(SentenceModel sentenceModel)
        {


            try
            {
                if (SentenceExists(sentenceModel.Id))
                {
                    _context.Entry(sentenceModel).State = EntityState.Modified;
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

        // POST: api/Sentences
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SentenceModel>> PostSentence(SentenceModel sentenceModel)
        {
            try
            {
                var sentence = await _context.Sentences.Where(c => c.OrderId == sentenceModel.OrderId && c.ClassNumber == sentenceModel.ClassNumber && c.ChapterNumber == sentenceModel.ChapterNumber && c.LearnLangCode.Equals(sentenceModel.LearnLangCode) && c.MyLangCode.Equals(sentenceModel.MyLangCode)).FirstOrDefaultAsync();
                if (sentence != null)
                {
                    return Conflict(sentence);
                }
                _context.Sentences.Add(sentenceModel);
                await _context.SaveChangesAsync();
                sentence = await _context.Sentences.Where(c => c.OrderId == sentenceModel.OrderId && c.ClassNumber == sentenceModel.ClassNumber && c.ChapterNumber == sentenceModel.ChapterNumber && c.LearnLangCode.Equals(sentenceModel.LearnLangCode) && c.MyLangCode.Equals(sentenceModel.MyLangCode)).FirstOrDefaultAsync();
                if (sentence != null)
                {
                    return Ok(sentence);
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

        // DELETE: api/Sentences/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSentence(int id)
        {
            try
            {
                var sentenceModel = await _context.Sentences.Where(s => s.Id == id).FirstOrDefaultAsync();
                if (sentenceModel == null)
                {
                    return NotFound();
                }

                _context.Sentences.Remove(sentenceModel);
                await _context.SaveChangesAsync();

                return Ok();

            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

        private bool SentenceExists(int id)
        {
            return _context.Sentences.Any(e => e.Id == id);
        }
    }
}
