namespace My_Library.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public Shelf Shelf { get; set; }
        public Set  Set { get; set; }
    }
}
