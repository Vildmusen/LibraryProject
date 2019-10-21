using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Repositories
{
    class BookCopyRepository : IRepository<BookCopy, int>
    {
        LibraryContext context;

        public BookCopyRepository(LibraryContext c)
        {
            this.context = c;
        }
        public void Add(BookCopy item)
        {
            context.BookCopies.Add(item);
            context.SaveChanges();
        }

        public IEnumerable<BookCopy> All()
        {
            return context.BookCopies;
        }

        public void Edit(BookCopy item)
        {
            throw new NotImplementedException();
        }

        public BookCopy Find(int id)
        {
            return context.BookCopies.Find(id);
        }

        public void Remove(BookCopy item)
        {
            throw new NotImplementedException();
        }
    }
}
