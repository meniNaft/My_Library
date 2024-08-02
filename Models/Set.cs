using System.ComponentModel.DataAnnotations.Schema;

namespace My_Library.Models
{
    public class Set
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
        public List<Book>? Books { get; set; }

        [NotMapped]
        public int ShelfId 
        { 
            get {
                int res = Books != null && Books.Count > 0 ? Books.First().ShelfId : 0;
                return res;
            }
        }
    }
}