using System.ComponentModel.DataAnnotations;

namespace DUSH17.Models
{
    public class Opponent
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string City { get; set; } = null!;
        public int Year { get; set; } 
        public Picture Picture { get; set; } = null!;
        public int PictureId { get; set; }
    }
}
