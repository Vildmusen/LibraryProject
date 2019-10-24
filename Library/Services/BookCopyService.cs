using Library.Models;
using Library.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public BookCopy SetLoaned(ICollection<BookCopy> copies)
        {
            foreach (BookCopy bc in copies)
            {
                if (bc.State == BookCopy.Status.AVAILABLE)
                {
                    bc.State = BookCopy.Status.NOT_AVAILABLE;
                    return bc;
                }
            }
            throw new Exception("No copies are available!");
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

        private void OnUpdate()
        {
            Updated?.Invoke(this, new EventArgs());
        }
    }
}
