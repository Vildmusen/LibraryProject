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

        public void Add(BookCopy bc)
        {
            bookCopyRepository.Add(bc);
        }

        public BookCopy SetLoaned(ICollection<BookCopy> copies)
        {
            foreach (BookCopy bc in copies)
            {
                if (bc.State == BookCopy.Status.AVAILABLE)
                {
                    bc.State = BookCopy.Status.LOANED;
                    return bc;
                }
            }
            throw new Exception("No copies are available!");
        }
    }
}
