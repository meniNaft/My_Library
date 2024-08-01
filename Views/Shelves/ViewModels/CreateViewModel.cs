using My_Library.Models;

namespace My_Library.Views.Shelves.ViewModels
{
    public class CreateViewModel
    {
        public Shelf Shelf{ get; set; }
        public List<Library> Libraries { get; set; }
        public ErrorModel ErrorModel { get; set; }
    }
}
