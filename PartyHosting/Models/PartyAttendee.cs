namespace PartyHosting.Models
{
    public class PartyAttendee
    {
        public int Id {get; set;}
        public int PartyId {get; set;}
        public int UserId {get; set;}

        public Party Party{get; set;}
        public User User {get; set;}
    }
}