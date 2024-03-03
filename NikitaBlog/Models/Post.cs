using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NikitaBlog.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Title { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        public string Author { get; set; } = null!;

        [Required]
        [MaxLength(5000)]
        public string Text { get; set; } = null!;

        [ForeignKey("CategoryId")]
        [ValidateNever]
        public Category Category { get; set; }
        public int CategoryId { get; set; }

        public string? ImageUrl { get; set; }



    }
}
