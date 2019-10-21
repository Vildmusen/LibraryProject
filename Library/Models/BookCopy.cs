using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
            LOANED
        }
        public BookCopy()
        {
            State = Status.AVAILABLE;
        }
    }
}
