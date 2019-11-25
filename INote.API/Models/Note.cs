using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace INote.API.Models
{
    [Table("Notes")]
    public class Note
    {
        public int Id { get; set; }

        [Required]
        [ForeignKey("Author")]
        public string AuthorId { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        public string Content { get; set; }

        [Required]
        public DateTime? CreatedAt { get; set; }

        public DateTime? ModifiedAt { get; set; }

        public ApplicationUser Author { get; set; }

    }
}