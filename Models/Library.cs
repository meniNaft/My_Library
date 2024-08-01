using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace My_Library.Models
{
    public class Library
    {
        public int Id { get; set; }
        [Display(Name = "שם ספריה")]
        public string Name { get; set; }
        [Display(Name = "רוחב")]
        public double Width { get; set; }
        [Display(Name = "גובה")]
        public double Height { get; set; }
        [Display(Name = "ז'אנר")]
        public int GenreId { get; set; }
        [Display(Name = "ז'אנר")]
        public Genre Genre { get; set; }
        public List<Shelf>? ShelfList { get; set; }

        [NotMapped, Display(Name = "מקום פנוי בארון")]
        public double EmptyPlace { 
            get=> ShelfList != null ? Height - ShelfList.Sum(x => x.Height) : Height; }
    }
}
