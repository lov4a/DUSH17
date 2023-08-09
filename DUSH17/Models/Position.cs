using System.ComponentModel.DataAnnotations;

namespace DUSH17.Models
{
    public class Position
    {
        [Key]
        public int Id { get; set; }
        public string NameOfPosition { get; set; } = null!;
    }
}
