using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Repositories
{
    /// <summary>
    /// Class containing operations communicateing with the BookCopy table of the database.
    /// </summary>
    class BookCopyRepository : IRepository<BookCopy, int>
    {
        private LibraryContext context;

        /// <summary>
        /// Assigns the database context.
        /// </summary>
        /// <param name="c"></param>
        public BookCopyRepository(LibraryContext c)
        {
            this.context = c;
        }

        /// <summary>
        /// Adds a book copy to the database.
        /// </summary>
        /// <param name="item"></param>
        public void Add(BookCopy item)
        {
            context.BookCopies.Add(item);
            context.SaveChanges();
        }

        /// <summary>
        /// Returns all book copies from the database.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BookCopy> All()
        {
            return context.BookCopies;
        }

        /// <summary>
        /// Edits the selected item.
        /// </summary>
        /// <param name="item"></param>
        public void Edit(BookCopy item)
        {
            context.SaveChanges();
        }

        /// <summary>
        /// Returns a book copy on ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BookCopy Find(int id)
        {
            return context.BookCopies.Find(id);
        }

        /// <summary>
        /// Removes the selected item.
        /// </summary>
        /// <param name="item"></param>
        public void Remove(BookCopy item)
        {
            context.BookCopies.Remove(item);
            context.SaveChanges();
        }
    }
}
