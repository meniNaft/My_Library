using Microsoft.AspNetCore.Cors;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace My_Library.Models
{
    public class Shelf
    {
        public int Id { get; set; }

        [Display(Name ="שם מדף")]
        public string Name { get; set; }
        [Display(Name = "גובה")]
        public int Height { get; set; }
        [Display(Name = "ספריה")]
        public int LibraryId { get; set; }
        [Display(Name = "ספריה")]
        public Library Library { get; set; }
        public List<Book>? Books { get; set; }
        [NotMapped]
        public double? EmptyPlace { get => Library != null ? (Books != null ? Library.Width - Books.Sum(x => x.Width) : Library.Width) : null; }


    }
}
