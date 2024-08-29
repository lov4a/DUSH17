using System.ComponentModel.DataAnnotations;

namespace DUSH17.Models
{
    public class t_Team
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; } = null!;
        public string city { get; set; } = null!;
        public Picture Picture { get; set; } = null!;
        public int? pictureId { get; set; }
    }
}
