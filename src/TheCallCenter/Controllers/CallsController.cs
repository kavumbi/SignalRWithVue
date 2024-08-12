using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TheCallCenter.Data;

namespace TheCallCenter.Controllers
{
    [ApiController]
    [Route("api/calls")]
    public class CallsController : Controller
    {
        private readonly CallCenterContext _ctx;

        public CallsController(CallCenterContext ctx)
        {
            _ctx = ctx;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var calls = await _ctx.Calls.Where(c => !c.Answered).ToListAsync();

            return Ok(calls);
        }

        [HttpPatch("{id:int}/answer")]
        public async Task<IActionResult> Patch(int id)
        {
            try
            {
                var call = await _ctx.Calls.Where(c => c.Id == id).FirstOrDefaultAsync();
                if (call == null) return BadRequest();

                call.Answered = true;
                call.AnswerTime = DateTime.UtcNow;

                if (await _ctx.SaveChangesAsync() > 0)
                {
                    return Ok(new { success = true });
                }
                else
                {
                    return BadRequest("Database Error");
                }
            }
            catch
            {
                return StatusCode(500);
            }
        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var call = await _ctx.Calls.Where(c => c.Id == id).FirstOrDefaultAsync();
                if (call == null) return BadRequest();

                _ctx.Remove(call);
                if (await _ctx.SaveChangesAsync() > 0)
                {
                    return Ok(new { success = true });
                }

                return BadRequest("Database Error");
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}
