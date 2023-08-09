using System.ComponentModel.DataAnnotations;

namespace DUSH17.Models
{
    public class Replace
    {
        [Key]
        public int Id { get; set; }
        public Match Match { get; set; } = null!;
        public int MatchID { get; set; }
        public Footballer FootballerIn { get; set; } = null!;
        public int FootballerInId { get; set; }
        public Footballer FootballerOut { get; set; } = null!;
        public int FootballerOutId { get; set; }
        public int Time { get; set; }


    }
}
