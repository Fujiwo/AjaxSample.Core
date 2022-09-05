using System;
using System.Collections.Generic;

#nullable disable

namespace AjaxSampleCore.Models
{
    public partial class Book
    {
        public Book() => Authors = new HashSet<Author>();

        public string Code { get; set; }
        public string Title { get; set; }
        public int? Price { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string PublisherCode { get; set; }
        public virtual Publisher Publisher { get; set; }
        public virtual ICollection<Author> Authors { get; set; }
    }
}
