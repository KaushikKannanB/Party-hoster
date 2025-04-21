using PartyHosting.Models;
using Microsoft.EntityFrameworkCore;

namespace PartyHosting.Data
{
    public class Party_db_context : DbContext
    {
        public Party_db_context(DbContextOptions<Party_db_context> options) : base(options)
        {

        }
        public DbSet<User> Users {get; set;}
        public DbSet<Party> Party {get; set;}
        public DbSet<PartyAttendee> PartyAttendee {get; set;}
    }
}