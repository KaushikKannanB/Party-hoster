namespace PartyHosting.Models
{
    public class Party
    {
        public int Id {get; set;}
        public string Title {get; set;}
        public string Description {get; set;}

        public DateTime PartyDate {get; set;}
        public int Seats {get; set;}
        
        public int Created_by{get; set;}
    }
}