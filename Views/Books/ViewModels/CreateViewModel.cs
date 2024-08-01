using My_Library.Models;

namespace My_Library.Views.Books.ViewModels
{
    public class CreateViewModel
    {
        public Book Book { get; set; }
        public List<Set> Sets { get; set; }
        public List<Shelf> Shelves { get; set; }
        public ErrorModel ErrorModel { get; set; }
    }
}
