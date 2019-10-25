using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Library.Models {

    /// <summary>
    /// Class representing a book.
    /// </summary>
    public class Book {

        /// <summary>
        /// Id of the book.
        /// </summary>
        [Key]
        public int BookID { get; set; }
        /// <summary>
        /// Title of the book.
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Description of the book.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Author of the book.
        /// </summary>
        public Author AuthorOfBook { get; set; }
        /// <summary>
        /// Collection of copies of the book.
        /// </summary>
        public ICollection<BookCopy> Copies { get; set; }

        /// <summary>
        /// Constructor to initialize a list for the collection of copies.
        /// </summary>
        public Book()
        {
            Copies = new List<BookCopy>();
        }

        /// <summary>
        /// Display the books ID and title.
        /// </summary>
        public override string ToString() {
            return String.Format("[{0}] -- {1}", this.BookID, this.Title);
        }
    }
}