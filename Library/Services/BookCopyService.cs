using Library.Models;
using Library.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services
{
    /// <summary>
    /// Class responsible for operations triggered in the GUI about Book Copies.
    /// </summary>
    class BookCopyService : IService
    {
        private BookCopyRepository bookCopyRepository;

        /// <summary>
        /// Event representing a change in the current book copy collection.
        /// </summary>
        public event EventHandler Updated;
        
        /// <summary>
        /// Creates and assigns a repository for Book Copies.
        /// </summary>
        /// <param name="rFactory"></param>
        public BookCopyService(RepositoryFactory rFactory)
        {
            this.bookCopyRepository = rFactory.CreateBookCopyRepository();
        }

        /// <summary>
        /// Returns all book copies.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BookCopy> All()
        {
            return bookCopyRepository.All();
        }

        /// <summary>
        /// Adds a Book Copy to the database by calling its add function.
        /// </summary>
        /// <param name="bc"></param>
        public void Add(BookCopy bc)
        {
            bookCopyRepository.Add(bc);
            OnUpdate();
        }

        /// <summary>
        /// Removes a Book Copy from the database by calling its remove funciton.
        /// </summary>
        /// <param name="bc"></param>
        public void Remove(BookCopy bc)
        {
            if(bc.State == BookCopy.Status.RETURNED)
            {
                bookCopyRepository.Remove(bc);
                OnUpdate();
            } else
            {
                throw new Exception("Copy is connected to a loan");
            }
        }

        /// <summary>
        /// Edits a Book Copy.
        /// </summary>
        /// <param name="bc"></param>
        public void Edit(BookCopy bc)
        {
            bookCopyRepository.Edit(bc);
            OnUpdate();
        }

        /// <summary>
        /// Changes a Book Copys stataus to ON_LOAN if it is available.
        /// </summary>
        /// <param name="bc"></param>
        /// <returns></returns>
        public BookCopy SetLoaned(BookCopy bc)
        {
            if (bc.State == BookCopy.Status.RETURNED)
            {
                bc.State = BookCopy.Status.ON_LOAN;
                return bc;
            }
            else
            {
                throw new Exception("The copy is not available");
            }
        }

        /// <summary>
        /// Returns a Book Copy with a status of RETURNED.
        /// </summary>
        /// <param name="books"></param>
        /// <returns></returns>
        public List<BookCopy> GetAllAvailableCopies(IEnumerable<Book> books)
        {
            List<BookCopy> availableCopies = new List<BookCopy>();
            foreach (Book b in books)
            {
                foreach (BookCopy bc in b.Copies)
                {
                    if (bc.State == BookCopy.Status.RETURNED)
                    {
                        availableCopies.Add(bc);
                    }
                }
            }
            return availableCopies;
        }

        /// <summary>
        /// Returns a Book Copy on ID.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public BookCopy GetCopyOnId(int Id)
        {
            return bookCopyRepository.Find(Id);
        }

        /// <summary>
        /// Reuturns all Book Copies sorted ascending on a property.
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public IEnumerable<BookCopy> AllAscendingOnProperty(string property)
        {
            return All().OrderBy(BuildQuery(property));
        }

        /// <summary>
        /// Returns all Book Copies sorted descending on a property.
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        internal IEnumerable<BookCopy> AllDescendingOnProperty(string property)
        {
            return All().OrderByDescending(BuildQuery(property));
        }

        // Builds a query on a property name. Only works with integer properties.
        // Credit to Balazs Tihanyi on https://stackoverflow.com/questions/9505189/dynamically-generate-linq-queries for inspiration.
        private Func<BookCopy, int> BuildQuery(string property)
        {
            var x = Expression.Parameter(typeof(BookCopy), "x");
            var body = Expression.PropertyOrField(x, property);
            return Expression.Lambda<Func<BookCopy, int>>(body, x).Compile();
        }

        /// <summary>
        /// Invokes the updated event.
        /// </summary>
        private void OnUpdate()
        {
            Updated?.Invoke(this, new EventArgs());
        }
    }
}
