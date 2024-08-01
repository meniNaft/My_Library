using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace My_Library.Models
{
    public class Genre
    {
        public int Id { get; set; }

        [Display(Name ="ז'אנר")]
        public string Name { get; set; }

        public List<Library>? Libraries { get; set; }
    }
}
