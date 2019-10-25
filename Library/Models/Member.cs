using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models
{
    /// <summary>
    /// Class representing a member.
    /// </summary>
    public class Member
    {
        /// <summary>
        /// ID of the member.
        /// </summary>
        [Key]
        public int MemberID { get; set; }
        /// <summary>
        /// Social security number of the member.
        /// </summary>
        public string SSO { get; set; }
        /// <summary>
        /// Name of the member.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Starting time of the membership.
        /// </summary>
        public DateTime MemberShip { get; set; }
        /// <summary>
        /// COllection of loans by the member.
        /// </summary>
        public ICollection<Loan> Loans { get; set; }

        /// <summary>
        /// Constructor to initialize and empty list forthe collection of loans.
        /// </summary>
        public Member()
        {
            Loans = new List<Loan>();
        }

        /// <summary>
        /// Displays social security number and name.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return SSO + " : " + Name;
        }
    }
}
