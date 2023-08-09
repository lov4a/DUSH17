using System.ComponentModel.DataAnnotations;

namespace DUSH17.Models
{
    public class ActionType
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = null!; //Гол|ГП|ЖК|КК|АГ

    }
}
