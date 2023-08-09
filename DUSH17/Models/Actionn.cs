using System.ComponentModel.DataAnnotations;

namespace DUSH17.Models
{
    public class Actionn
    {
        [Key]
        public int Id { get; set; }
        public Match Match { get; set; } = null!;
        public int MatchId { get; set; }
        public Footballer Footballer { get; set; } = null!;
        public int FootballerId { get; set; }
        public int Time { get; set; }
        public ActionType ActionType { get; set; } = null!;
        public int ActionTypeId { get; set; }
    }
}
