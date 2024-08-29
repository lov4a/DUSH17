using System.ComponentModel.DataAnnotations;

namespace DUSH17.Models
{
    public class t_Competition
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; } = null!;
        public DateOnly startDate { get; set; }
        public DateOnly endDate { get; set; }
        public int countOfTeams { get; set; }
        public int? rounds { get; set; }
        public int? previousChamp { get; set; }
        public int? minutes { get; set; }
        public Picture picture { get; set; } = null!;
        public int? pictureId { get; set; }
        public int? playoffs { get; set; }

    }
}
