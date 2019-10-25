using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models
{
    /// <summary>
    /// Clss representing a copy of a book.
    /// </summary>
    public class BookCopy
    {
        /// <summary>
        /// ID of the copy.
        /// </summary>
        [Key]
        public int CopyID { get; set; }
        /// <summary>
        /// Reference to the Book it is a copy of
        /// </summary>
        public Book Book { get; set; }
        /// <summary>
        /// Condition of the copy.
        /// </summary>
        public int Condition { get; set; }
        /// <summary>
        /// Status of the copy, displays AVAILABLE or NOT_AVAILABLE
        /// </summary>
        public Status State { get; set; }

        /// <summary>
        /// enum describing status of a copy
        /// </summary>
        public enum Status
        {
            ON_LOAN,
            RETURNED,
            OVERDUE
        }

        /// <summary>
        /// Initializes a copy and sets its status to AVAILABLE.
        /// </summary>
        public BookCopy()
        {
            State = Status.RETURNED;
        }

        /// <summary>
        /// Displays status, id and title.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return String.Format("[{0}]-[{1}] {2}", State, CopyID, Book.Title);
        }
    }
}
