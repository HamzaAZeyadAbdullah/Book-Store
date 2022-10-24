using BookStore2.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookStore2.View_Model
{
    public class BookAuthorViewModel
    {
        public int BookId { get; set; }



        [Required]
        [MaxLength(20)]
        [MinLength(5)]
        public string Title { get; set; }



        [Required]
        [StringLength(120,MinimumLength =5 ,ErrorMessage ="Between 5-120")]
        public string Discription { get; set; }

        public int AuthorId { get; set; }

        public List<Author> Authors { get; set; }

        public IFormFile File { get; set; }
        public string imageurl { get; set; }
    }
}
