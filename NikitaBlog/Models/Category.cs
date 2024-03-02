using System.ComponentModel.DataAnnotations;

namespace NikitaBlog.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(25)]
        public string Title { get; set; }

        public int DisplayOrder { get; set; }

    }
}
