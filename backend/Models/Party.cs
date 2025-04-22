using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PartyHosting.Models
{
   public class Party
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime PartyDate { get; set; }
    public int Seats { get; set; }
    public int Created_by { get; set; }
}

}