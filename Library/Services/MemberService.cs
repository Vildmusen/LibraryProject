using Library.Models;
using Library.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services
{
    /// <summary>
    /// Class responsible for operations triggered in the GUI about Members.
    /// </summary>
    class MemberService : IService
    {
        private MemberRepository memberRepository;

        /// <summary>
        /// Event representing a change in the current Member collection.
        /// </summary>
        public event EventHandler Updated;

        /// <summary>
        /// Creates and assigns a repository,
        /// </summary>
        public MemberService(RepositoryFactory rFactory)
        {
            this.memberRepository = rFactory.CreateMemberRepository();
        }

        /// <summary>
        /// Returns all members
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Member> All()
        {
            return memberRepository.All();
        }

        /// <summary>
        /// Adds a member. Checks if a name is given and if ssn already exists.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="sso"></param>
        public void Add(string name, string ssn)
        {
            if (String.IsNullOrEmpty(name))
            {
                throw new FormatException("Name can't be empty");
            }
            else if(All().Any(x => x.SSO == ssn))
            {
                throw new Exception("User already exists");
            }
            else
            {
                memberRepository.Add(new Member { Name = name, SSO = ssn, MemberShip = DateTime.Now });
                OnUpdate();
            }
        }

        /// <summary>
        /// Removes a member.
        /// </summary>
        /// <param name="m"></param>
        public void Remove(Member m)
        {
            if(m.Loans.Count != 0)
            {
                memberRepository.Remove(m);
                OnUpdate();
            } else
            {
                throw new Exception("Member has active loans.");
            }
        }

        /// <summary>
        /// Returns all book copies a user is connected to via loans.
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public IEnumerable<BookCopy> GetBookCopysByMemberName(Member member)
        {
            return member.Loans.Where(b => b.BookCopy.State == BookCopy.Status.ON_LOAN).Select(b => b.BookCopy);
        }

        /// <summary>
        /// Returns all members sorted ascending on a property.
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public IEnumerable<Member> AllAscendingOnProperty(string property)
        {
            return All().OrderBy(BuildQuery(property));
        }

        /// <summary>
        /// Returns all members sorted descending on a property.
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        internal IEnumerable<Member> AllDescendingOnProperty(string property)
        {
            return All().OrderByDescending(BuildQuery(property));
        }

        // Builds a query on a property name. Only works with integer properties.
        // Credit to Balazs Tihanyi on https://stackoverflow.com/questions/9505189/dynamically-generate-linq-queries for inspiration.
        private Func<Member, int> BuildQuery(string property)
        {
            var x = Expression.Parameter(typeof(Member), "x");
            var body = Expression.PropertyOrField(x, property);
            return Expression.Lambda<Func<Member, int>>(body, x).Compile();
        }

        /// <summary>
        /// Adds a loan to a member.
        /// </summary>
        /// <param name="member"></param>
        /// <param name="loan"></param>
        public void UpdateMemberLoans(Member member, Loan loan)
        {
            member.Loans.Add(loan);
        }

        /// <summary>
        /// Returns a member on social security number.
        /// </summary>
        /// <param name="SSN"></param>
        /// <returns></returns>
        public Member GetMemberBySSN(string SSN)
        {
            return memberRepository.All().Where(m => m.SSO == SSN).FirstOrDefault();
        }

        /// <summary>
        /// Invokes the event "Updated".
        /// </summary>
        public void OnUpdate()
        {
            Updated?.Invoke(this, new EventArgs());
        }

    }
}
