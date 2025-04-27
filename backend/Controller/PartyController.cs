using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PartyHosting.Data;
using PartyHosting.Models;

namespace PartyHosting.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    
    public class PartyController : ControllerBase
    {
        private readonly Party_db_context context;
        public PartyController(Party_db_context context)
        {
            this.context = context;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateParty([FromBody] Party party)
        {
            if(party.Seats<=0)
            {
                return BadRequest("Please make some seats for your attendees");
            }
            context.Party.Add(party);
            await context.SaveChangesAsync();
            return Ok("Created party successfully");
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateParty(int id, [FromBody] Party updatedParty)
        {
            var party = await context.Party.FindAsync(id);
            if(party==null)
            {
                return NotFound("No such party exists");
            }
            party.Title = updatedParty.Title;
            party.Description = updatedParty.Description;
            party.PartyDate = updatedParty.PartyDate;
            party.Seats = updatedParty.Seats;

            await context.SaveChangesAsync();
            return Ok("Changes made!");

        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteParty(int id)
        {
            var party = await context.Party.FindAsync(id);
            var partyattendees_party = await context.PartyAttendee.Where(pa=>pa.PartyId==id).ToListAsync();

            foreach(var v in partyattendees_party)
            {
                context.PartyAttendee.Remove(v);
            }
            
            if(party==null)
            {
                return NotFound("No such party exists");
            }

            context.Party.Remove(party);
            await context.SaveChangesAsync();

            return Ok("Party Deleted");
        }
         
        [HttpPost("join")]
        public async Task<IActionResult> JoinParty([FromBody] Partyrequest request)
        {
            var party = await context.Party.FindAsync(request.PartyId);
            var user = await context.Users.FindAsync(request.UserId);


            if(party == null)
            {
                return NotFound("No such party is found in our db");
            }
            if(party.Seats<=0)
            {
                return BadRequest("No seats available --> check out other parties!");
            }
            if(user == null)
            {
                return NotFound("No such user is found in our db");
            }
            var already_joined = await context.PartyAttendee.FirstOrDefaultAsync(pa => pa.PartyId == request.PartyId && pa.UserId == request.UserId);

            if(already_joined!=null)
            {
                return BadRequest("ALready joined the party dude");
            }
            var attendee = new PartyAttendee{
                PartyId = request.PartyId,
                UserId = request.UserId
            };

            context.PartyAttendee.Add(attendee);
            party.Seats-=1;
            await context.SaveChangesAsync();
            return Ok("Enjoy the party");
        }   

        public class Partyrequest{
            public int PartyId {get; set;}
            public int UserId {get; set;}
        }
    }
}