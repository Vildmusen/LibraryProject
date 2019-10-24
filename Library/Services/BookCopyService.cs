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
    class BookCopyService : IService
    {
        BookCopyRepository bookCopyRepository;

        public event EventHandler Updated;

        /// <param name="rFactory">A repository factory, so the service can create its own repository.</param>
        public BookCopyService(RepositoryFactory rFactory)
        {
            this.bookCopyRepository = rFactory.CreateBookCopyRepository();
        }

        public IEnumerable<BookCopy> All()
        {
            return bookCopyRepository.All();
        }

        public void Add(BookCopy bc)
        {
            bookCopyRepository.Add(bc);
            OnUpdate();
        }

        public void Remove(BookCopy bc)
        {
            // TODO Can't remove if connected to a loan
            bookCopyRepository.Remove(bc);
            OnUpdate();
        }

        public BookCopy SetLoaned(BookCopy bc)
        {
            if (bc.State == BookCopy.Status.AVAILABLE)
            {
                bc.State = BookCopy.Status.NOT_AVAILABLE;
                return bc;
            }
            else
            {
                throw new Exception("The copy is not available");
            }
        }

        public List<BookCopy> GetAllAvailableCopies(IEnumerable<Book> books)
        {
            List<BookCopy> availableCopies = new List<BookCopy>();
            foreach (Book b in books)
            {
                foreach (BookCopy bc in b.Copies)
                {
                    if (bc.State == BookCopy.Status.AVAILABLE)
                    {
                        availableCopies.Add(bc);
                    }
                }
            }
            return availableCopies;
        }

        public BookCopy GetCopyOnId(int Id)
        {
            return bookCopyRepository.Find(Id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public IEnumerable<BookCopy> AllAscendingOnProperty(string property)
        {
            return All().OrderBy(BuildQuery(property));
        }

        internal IEnumerable<BookCopy> AllDescendingOnProperty(string property)
        {
            return All().OrderByDescending(BuildQuery(property));
        }

        private Func<BookCopy, int> BuildQuery(string property)
        {
            var x = Expression.Parameter(typeof(BookCopy), "x");
            var body = Expression.PropertyOrField(x, property);
            return Expression.Lambda<Func<BookCopy, int>>(body, x).Compile();
        }

        private void OnUpdate()
        {
            Updated?.Invoke(this, new EventArgs());
        }
    }
}
