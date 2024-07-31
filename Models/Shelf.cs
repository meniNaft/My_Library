namespace My_Library.Models
{
    public class Shelf
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Height { get; set; }
        public Library Library { get; set; }
    }
}
