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
    class BookService : IService
    {
        /// <summary>
        /// service doesn't need a context but it needs a repository.
        /// </summary>
        BookRepository bookRepository;

        public event EventHandler Updated;

        /// <param name="rFactory">A repository factory, so the service can create its own repository.</param>
        public BookService(RepositoryFactory rFactory)
        {
            this.bookRepository = rFactory.CreateBookRepository();
        }

        public IEnumerable<Book> All()
        {
            return bookRepository.All();
        }

        public void Delete(Book b)
        {
            bookRepository.Remove(b);
            OnUpdate();
        }

        public IEnumerable<Book> AllAvailable()
        {
            return All().Where(b => b.Copies.Any(c => c.State == BookCopy.Status.AVAILABLE));
        }

        public IEnumerable<Book> GetBooksByAuthor(Author author)
        {
            return All().Where(b => b.AuthorOfBook == author);
        }

        public IEnumerable<Book> GetAllThatContainsInTitle(string a)
        {
            return bookRepository.All().Where(b => b.Title.Contains(a));
        }

        public bool AllCopiesAvailable(Book b)
        {
            foreach(BookCopy bs in b.Copies)
            {
                if(bs.State == BookCopy.Status.NOT_AVAILABLE)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// The Edit method makes sure that the given Book object is saved to the database and raises the Updated() event.
        /// </summary>
        /// <param name="b"></param>
        public void Edit(Book b)
        {
            bookRepository.Edit(b);
            OnUpdate();
        }

        public void Add(Book b)
        {
            bookRepository.Add(b);
            OnUpdate();
        }

        public void AddCopy(BookCopy bc)
        {
            Book b = bookRepository.Find(bc.Book.BookID);
            b.Copies.Add(bc);
            bookRepository.Edit(b);
        }

        public void Remove(Book b)
        {
            bookRepository.Remove(b);
            OnUpdate();
        }

        public IEnumerable<Book> AllAscendingOnProperty(string property)
        {
            return All().OrderBy(BuildQuery(property));
        }

        internal IEnumerable<Book> AllDescendingOnProperty(string property)
        {
            return All().OrderByDescending(x => x.ToString() == property);
        }

        private Func<Book, int> BuildQuery(string property)
        {
            var x = Expression.Parameter(typeof(Book), "x");
            var body = Expression.PropertyOrField(x, property);
            return Expression.Lambda<Func<Book, int>>(body, x).Compile();
        }

        public void OnUpdate()
        {
            Updated?.Invoke(this, new EventArgs());
        }

    }
}
