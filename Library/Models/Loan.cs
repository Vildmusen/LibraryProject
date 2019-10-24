using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models
{
    public class Loan
    {
        [Key]
        public int LoanID { get; set; }
        public DateTime TimeOfLoan { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? TimeOfReturn { get; set; }
        public BookCopy BookCopy { get; set; }
        public Member Member { get; set; }

        public Loan() { }

        public override string ToString()
        {
            return String.Format("[{0}] {1} : {2}", LoanID, TimeOfLoan.ToShortDateString(), BookCopy.Book.Title);
        }
    }
}
