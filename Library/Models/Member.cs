using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models
{
    public class Member
    {
        [Key]
        public int MemberID { get; set; }
        public string SSO { get; set; }
        public string Name { get; set; }
        public DateTime MemberShip { get; set; }
        public ICollection<Loan> Loans { get; set; }
        public Member()
        {
            Loans = new List<Loan>();
        }
        public override string ToString()
        {
            return SSO + " : " + Name;
        }
    }
}
