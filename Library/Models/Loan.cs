using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models
{
    /// <summary>
    /// CLass representing a loan.
    /// </summary>
    public class Loan
    {
        /// <summary>
        /// ID of the loan.
        /// </summary>
        [Key]
        public int LoanID { get; set; }
        /// <summary>
        /// Starting time of the loan.
        /// </summary>
        public DateTime TimeOfLoan { get; set; }
        /// <summary>
        /// Due date of the loan.
        /// </summary>
        public DateTime DueDate { get; set; }
        /// <summary>
        /// Time of return of the loan,
        /// </summary>
        public DateTime? TimeOfReturn { get; set; }
        /// <summary>
        /// Reference to a copy that is loaned.
        /// </summary>
        public BookCopy BookCopy { get; set; }
        /// <summary>
        /// Refrence to the member that loaned the copy.
        /// </summary>
        public Member Member { get; set; }

        /// <summary>
        /// Empty constructor.
        /// </summary>
        public Loan() { }

        /// <summary>
        /// Displays ID, starting time and the title of the book.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return String.Format("[{0}] {1} : {2}", LoanID, TimeOfLoan.ToShortDateString(), BookCopy.Book.Title);
        }
    }
}
