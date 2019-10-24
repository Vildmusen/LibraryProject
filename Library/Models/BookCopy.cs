using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models
{
    public class BookCopy
    {
        [Key]
        public int CopyID { get; set; }
        public Book Book { get; set; }
        public int Condition { get; set; }
        public Status State { get; set; }

        public enum Status
        {
            AVAILABLE,
            NOT_AVAILABLE
        }

        public BookCopy()
        {
            State = Status.AVAILABLE;
        }

        public override string ToString()
        {
            return String.Format("[{0}] - {1} : {2}", CopyID, Book.Title, State);
        }
    }
}
