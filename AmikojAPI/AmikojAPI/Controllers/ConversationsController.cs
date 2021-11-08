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
    public class ConversationsController : ControllerBase
    {
        private readonly Amikoj_DBContext _context;

        public ConversationsController(Amikoj_DBContext context)
        {
            _context = context;
        }

        // GET: api/Conversations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ConversationModel>>> GetDeConversations()
        {
            return Ok(await _context.Conversations.ToListAsync());
        }

        // GET: api/Conversations/en/2/3/63
        [HttpGet("{myLangCode}/{learnLangCode}/{chapterNum}/{classNum}/{orderId}")]
        public async Task<ActionResult<ConversationModel>> GetConversation(int chapterNum, int classNum, int orderId, string myLangCode, string learnLangCode)
        {
            var Conversation = await _context.Conversations.FirstOrDefaultAsync(u => u.ChapterNumber == chapterNum && u.ClassNumber == classNum && u.OrderId == orderId && u.MyLangCode == myLangCode && u.LearnLangCode == learnLangCode);

            if (Conversation == null)
            {
                return Accepted();
            }

            return Ok(Conversation);
        }

        // GET: api/Conversations/en/2/3
        [HttpGet("{myLangCode}/{learnLangCode}/{chapterNum}/{classNum}")]
        public async Task<ActionResult<List<ConversationModel>>> GetConversationsByClass(int chapterNum, int classNum, string myLangCode, string learnLangCode)
        {
            var Conversations = await _context.Conversations.Where<ConversationModel>(u => u.ChapterNumber == chapterNum && u.ClassNumber == classNum && u.MyLangCode == myLangCode && u.LearnLangCode == learnLangCode).ToListAsync();

            if (Conversations == null)
            {
                return NotFound();
            }

            return Ok(Conversations);
        }
        // PUT: api/Conversations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutConversation( ConversationModel Conversation)
        {
            try
            {
                if (ConversationExists(Conversation.Id))
                {
                    _context.Conversations.Update(Conversation);
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

        // POST: api/Conversations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ConversationModel>> PostConversation(ConversationModel conversationModel)
        {
            try
            {
                var conversarion = await _context.Conversations.Where(c => c.OrderId == conversationModel.OrderId && c.ClassNumber == conversationModel.ClassNumber && c.ChapterNumber == conversationModel.ChapterNumber && c.LearnLangCode.Equals(conversationModel.LearnLangCode) && c.MyLangCode.Equals(conversationModel.MyLangCode)).FirstOrDefaultAsync();
                if (conversarion != null)
                {
                    return Conflict(conversarion);
                }
                _context.Conversations.Add(conversationModel);
                await _context.SaveChangesAsync();
                conversarion = await _context.Conversations.Where(c => c.OrderId == conversationModel.OrderId && c.ClassNumber == conversationModel.ClassNumber && c.ChapterNumber == conversationModel.ChapterNumber && c.LearnLangCode.Equals(conversationModel.LearnLangCode) && c.MyLangCode.Equals(conversationModel.MyLangCode)).FirstOrDefaultAsync();
                if (conversarion != null)
                {
                    return Ok(conversarion);
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

        // DELETE: api/Conversations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConversation(int id)
        {
            try
            {
                var Conversation = await _context.Conversations.Where(c => c.Id == id).FirstOrDefaultAsync();
                if (Conversation == null)
                {
                    return NotFound();
                }

                _context.Conversations.Remove(Conversation);
                await _context.SaveChangesAsync();

                return Ok();

            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        private bool ConversationExists(int id)
        {
            return _context.Conversations.Any(e => e.Id == id);
        }
    }
}
