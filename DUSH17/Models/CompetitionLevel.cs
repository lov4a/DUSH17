using System.ComponentModel.DataAnnotations;

namespace DUSH17.Models
{
    public class CompetitionLevel
    {
        [Key]
        public int Id { get; set; }
        public string Level { get; set; } = null!;
    }
}
