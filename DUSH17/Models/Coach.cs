namespace DUSH17.Models
{
    public class Coach
    {
        public int Id { get; set; }
        public string Surname { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Patronymic { get; set; } = null!;
        public DateOnly DateOfBirthday { get; set; }
        public Category Category { get; set; } = null!;
        public int CategoryId { get; set; }
        public string PhoneNumber { get; set; } = null!;
		public Picture Picture { get; set; } = null!;
		public int PictureId { get; set; }

	}
}
