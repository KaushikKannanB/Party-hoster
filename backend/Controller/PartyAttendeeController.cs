using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Bcpg;
using PartyHosting.Data;
using PartyHosting.Models;

namespace PartyHosting.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class PartyAttendeeController : ControllerBase
    {
        private readonly Party_db_context context;

        public PartyAttendeeController(Party_db_context context)
        {
            this.context = context;
        }

        [HttpGet("get-available-party")]
        public async Task<IActionResult> GetParty()
        {
            var parties = await context.Party.ToListAsync();

            return Ok(parties);
        }

        [HttpGet("get-my-party")]
        public async Task<IActionResult> GetMyParty(int UserId)
        {
            var parties = await context.PartyAttendee.Where(pa=>pa.UserId==UserId).ToListAsync();
            if(parties.Count==0)
            {
                return BadRequest("Bo parties joined");
            }
            return Ok(parties);
        }

        [HttpGet("get-party-deets")]
        
        public async Task<IActionResult> GetPartyDetails(int id)
        {
            var parties = await context.Party.Where(p=> p.Id==id).ToListAsync();
            if(parties.Count==0)
            {
                return BadRequest("Bo parties joined");
            }

            return Ok(parties);
        }
        

        [HttpGet("get_attendees")]
        public async Task<IActionResult> GetAttendees(int id)
        {
            var attendees = await context.PartyAttendee.Where(pa => pa.PartyId == id).Select(pa => pa.UserId).ToListAsync();

            if(attendees.Count==0)
            {
                return BadRequest("No one has joined the party yet");
            }

            return Ok(attendees);
        }

        [HttpGet("get-created-by-me")]
        public async Task<IActionResult> Getcreatedparties(int createdby)
        {
            var party = await context.Party.Where(pa=> pa.Created_by==createdby).ToListAsync();
            if(party.Count==0)
            {
                return BadRequest("No parties created");
            }
            return Ok(party);
        }


        [HttpDelete("leave-party")]
        public async Task<IActionResult> LeaveParty([FromBody] Leaverequest request)
        {
            var party = await context.PartyAttendee.FirstOrDefaultAsync(pa => pa.PartyId == request.PartyId && pa.UserId == request.UserId);

            if(party==null)
            {
                return BadRequest("No such user in the party");
            }
            var party__ = await context.Party.FindAsync(request.PartyId);

            if(party__!=null)
            {
                party__.Seats+=1;
            }
            context.PartyAttendee.Remove(party);
            await context.SaveChangesAsync();

            return Ok("Left the party");
        }
        public class Leaverequest
        {
            public int PartyId {get; set;}
            public int UserId {get; set;}
        }
    }
}