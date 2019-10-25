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
    /// Class responsible for operations triggered in the GUI about Loans.
    /// </summary>
    class LoanService : IService
    {
        private LoanRepository loanRepository;

        /// <summary>
        /// Event representing a change in the current book collection.
        /// </summary>
        public event EventHandler Updated;

        /// <summary>
        /// Creates and assigns a repository,
        /// </summary>
        public LoanService(RepositoryFactory factory)
        {
            loanRepository = factory.CreateLoanRepository();
        }

        /// <summary>
        /// Returns all loans.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Loan> All()
        {
            return loanRepository.All();
        }

        /// <summary>
        /// Adds a loan.
        /// </summary>
        /// <param name="l"></param>
        public void Add(Loan l)
        {
            loanRepository.Add(l);
            OnUpdate();
        }

        /// <summary>
        /// Removes a loan.
        /// </summary>
        /// <param name="l"></param>
        public void Remove(Loan l)
        {
            loanRepository.Remove(l);
            OnUpdate();
        }

        /// <summary>
        /// Edits a loan.
        /// </summary>
        /// <param name="l"></param>
        public void Edit(Loan l)
        {
            loanRepository.Edit(l);
            OnUpdate();
        }

        /// <summary>
        /// Returns all loans connected to a member.
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public IEnumerable<Loan> GetLoansByMember(Member m)
        {
            return All().Where(x => x.Member == m);
        }

        /// <summary>
        /// Returns a member from a Copy Id by checking if any loan is connecting them.
        /// </summary>
        /// <param name="copyID"></param>
        /// <returns></returns>
        public Member GetMemberFromCopyID(int copyID)
        {
            return All().Where(x => x.Member.Loans.Any(c => c.BookCopy.CopyID == copyID)).FirstOrDefault().Member;
        }

        /// <summary>
        /// Returns all loans sorted ascending on a property.
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public IEnumerable<Loan> AllAscendingOnProperty(string property)
        {
            return All().OrderBy(BuildQuery(property));
        }

        /// <summary>
        /// Returns all loans sorted descending on a property.
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        internal IEnumerable<Loan> AllDescendingOnProperty(string property)
        {
            return All().OrderByDescending(BuildQuery(property));
        }

        // Builds a query on a property name. Only works with integer properties.
        // Credit to Balazs Tihanyi on https://stackoverflow.com/questions/9505189/dynamically-generate-linq-queries for inspiration.
        private Func<Loan, int> BuildQuery(string property)
        {
            var x = Expression.Parameter(typeof(Loan), "x");
            var body = Expression.PropertyOrField(x, property);
            return Expression.Lambda<Func<Loan, int>>(body, x).Compile();
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
