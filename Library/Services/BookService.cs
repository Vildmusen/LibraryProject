using Library.Models;
using Library.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public void Remove(Book b)
        {
            bookRepository.Remove(b);
            OnUpdate();
        }

        public void AddCopy(Book b)
        {
            
            OnUpdate();
        }

        public void OnUpdate()
        {
            Updated?.Invoke(this, new EventArgs());
        }
    }
}
