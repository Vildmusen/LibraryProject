using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Repositories
{
    /// <summary>
    /// Class containing operations communicating with the Loan table in the database.
    /// </summary>
    class LoanRepository : IRepository<Loan, int>
    {
        private LibraryContext context;

        /// <summary>
        /// Assigns the database context.
        /// </summary>
        /// <param name="c"></param>
        public LoanRepository(LibraryContext c)
        {
            this.context = c;
        }

        /// <summary>
        /// Adds a loan to the database.
        /// </summary>
        /// <param name="item"></param>
        public void Add(Loan item)
        {
            context.Loans.Add(item);
            context.SaveChanges();
        }

        /// <summary>
        /// returns all loans from the database.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Loan> All()
        {
            return context.Loans;
        }

        /// <summary>
        /// Edits the item and saves it to the dataabse.
        /// </summary>
        /// <param name="item"></param>
        public void Edit(Loan item)
        {
            context.SaveChanges();
        }

        /// <summary>
        /// Returns a loan on ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Loan Find(int id)
        {
            return context.Loans.Find(id);
        }

        /// <summary>
        /// Removes the selected item.
        /// </summary>
        /// <param name="item"></param>
        public void Remove(Loan item)
        {
            context.Loans.Remove(item);
            context.SaveChanges();
        }
    }
}
