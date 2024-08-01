using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace My_Library.Models
{
    public class Book
    {
        public int Id { get; set; }
        [Display(Name = "שם ספר")]
        public string Name { get; set; }
        [Display(Name = "רוחב")]
        public double Width { get; set; }
        [Display(Name = "גובה")]
        public double Height { get; set; }
        public int ShelfId { get; set; }
        [Display(Name = "מדף")]
        public Shelf Shelf { get; set; }
        public int? SetId { get; set; }
        [Display(Name = "סט")]
        public Set? Set { get; set; }
        [NotMapped, Display(Name ="ז'אנר")]
        public Genre? Genre { get => Shelf?.Library?.Genre; }
    }
}
