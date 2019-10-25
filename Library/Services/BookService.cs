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
    /// Class responsible for operations triggered in the GUI about Books.
    /// </summary>
    class BookService : IService
    { 
        private BookRepository bookRepository;

        /// <summary>
        /// Event representing a change in the current book collection.
        /// </summary>
        public event EventHandler Updated;

        /// <summary>
        /// Creates and assigns a repository,
        /// </summary>
        /// <param name="rFactory"></param>
        public BookService(RepositoryFactory rFactory)
        {
            this.bookRepository = rFactory.CreateBookRepository();
        }

        /// <summary>
        /// Returns all books.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Book> All()
        {
            return bookRepository.All();
        }

        /// <summary>
        /// Deletes a book.
        /// </summary>
        /// <param name="b"></param>
        public void Delete(Book b)
        {
            bookRepository.Remove(b);
            OnUpdate();
        }

        /// <summary>
        /// Returns all book that have any copy that is RETURNED.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Book> AllAvailable()
        {
            return All().Where(b => b.Copies.Any(c => c.State == BookCopy.Status.RETURNED));
        }

        /// <summary>
        /// Return all books by an author.
        /// </summary>
        /// <param name="author"></param>
        /// <returns></returns>
        public IEnumerable<Book> GetBooksByAuthor(Author author)
        {
            return All().Where(b => b.AuthorOfBook == author);
        }

        /// <summary>
        /// Returns all books with a specific string value in the title.
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public IEnumerable<Book> GetAllThatContainsInTitle(string a)
        {
            return bookRepository.All().Where(b => b.Title.Contains(a));
        }

        /// <summary>
        /// Returns true if all copies of a book is RETURNED.
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public bool AllCopiesAvailable(Book b)
        {
            foreach(BookCopy bs in b.Copies)
            {
                if(bs.State == BookCopy.Status.ON_LOAN || bs.State == BookCopy.Status.OVERDUE)
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

        /// <summary>
        /// Adds a book to the database.
        /// </summary>
        /// <param name="b"></param>
        public void Add(Book b)
        {
            bookRepository.Add(b);
            OnUpdate();
        }

        /// <summary>
        /// Adds a copy to a book and saves the changes in the database.
        /// </summary>
        /// <param name="bc"></param>
        public void AddCopy(BookCopy bc)
        {
            Book b = bookRepository.Find(bc.Book.BookID);
            b.Copies.Add(bc);
            bookRepository.Edit(b);
        }

        /// <summary>
        /// Removes a book.
        /// </summary>
        /// <param name="b"></param>
        public void Remove(Book b)
        {
            bookRepository.Remove(b);
            OnUpdate();
        }

        /// <summary>
        /// Returns all books sorted ascending on a property.
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public IEnumerable<Book> AllAscendingOnProperty(string property)
        {
            return All().OrderBy(BuildQuery(property));
        }

        /// <summary>
        /// Returns all books sorted descending on a property.
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        internal IEnumerable<Book> AllDescendingOnProperty(string property)
        {
            return All().OrderByDescending(x => x.ToString() == property);
        }

        // Builds a query on a property name. Only works with integer properties.
        // Credit to Balazs Tihanyi on https://stackoverflow.com/questions/9505189/dynamically-generate-linq-queries for inspiration.
        private Func<Book, int> BuildQuery(string property)
        {
            var x = Expression.Parameter(typeof(Book), "x");
            var body = Expression.PropertyOrField(x, property);
            return Expression.Lambda<Func<Book, int>>(body, x).Compile();
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
