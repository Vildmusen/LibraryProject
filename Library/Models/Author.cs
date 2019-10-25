using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models
{
    /// <summary>
    /// Class representing an author. Every author has a collection of books.
    /// </summary>
    public class Author
    {
        /// <summary>
        /// Id of author.
        /// </summary>
        [Key]
        public int AuthorID { get; set; }
        /// <summary>
        /// Name of author.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Collection of written books.
        /// </summary>
        public ICollection<Book> WrittenBooks { get; set; }

        /// <summary>
        /// Constructor to initialize a list for the collection.
        /// </summary>
        public Author()
        {
            WrittenBooks = new List<Book>();
        }

        /// <summary>
        /// Displays the authors name.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Name;
        }
    }
}
