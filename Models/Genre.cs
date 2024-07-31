using System.ComponentModel.DataAnnotations;

namespace My_Library.Models
{
    public class Genre
    {
        public int Id { get; set; }

        [Display(Name ="ז'אנר")]
        public string Name { get; set; }
    }
}
