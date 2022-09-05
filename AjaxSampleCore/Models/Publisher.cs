using System;
using System.Collections.Generic;

#nullable disable

namespace AjaxSampleCore.Models
{
    public partial class Publisher
    {
        public Publisher() => Books = new HashSet<Book>();

        public string Code { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
