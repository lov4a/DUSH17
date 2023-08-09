using System.ComponentModel.DataAnnotations;

namespace DUSH17.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string NameOfCategory { get; set; } = null!;
    }
}
