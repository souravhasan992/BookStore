using System.ComponentModel.DataAnnotations;

namespace BookStore.Data.Models
{
    public class BookModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Please add title property")]
        [StringLength(50)]
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
