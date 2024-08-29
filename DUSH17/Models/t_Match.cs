using System.ComponentModel.DataAnnotations;

namespace DUSH17.Models
{
    public class t_Match
    {
        [Key]
        public int id { get; set; }
        public t_Team home { get; set; } = null!;
        public t_Team visitor { get; set; } = null!;
        public int homeId { get; set; }
        public int visitorId { get; set; }
        public DateOnly date { get; set; }
        public TimeOnly? time { get; set; }
        public int? tour { get; set; }
        public int stage { get; set; }
        public string? stadium { get; set; } = null!;
        public int? h_goals { get; set; }
        public int? v_goals { get; set; }
        public t_Competition competition { get; set; } = null!;
        public int competitionId { get; set; }

    }
}
