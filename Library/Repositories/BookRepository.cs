using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Library.Models;

namespace Library.Repositories
{
    /// <summary>
    /// Class conatining operations communicating with the Book table in the database.
    /// </summary>
    public class BookRepository : IRepository<Book, int>
    {
        private LibraryContext context;

        /// <summary>
        /// Assigns the database context.
        /// </summary>
        /// <param name="c"></param>
        public BookRepository(LibraryContext c)
        {
            this.context = c;
        }

        /// <summary>
        /// Add a book to the databse.
        /// </summary>
        /// <param name="item"></param>
        public void Add(Book item)
        {
            context.Books.Add(item);
            context.SaveChanges();
        }

        /// <summary>
        /// Returns all books from the database.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Book> All()
        {
            return context.Books;
        }

        /// <summary>
        /// Edits a book and saves the changes to the database.
        /// </summary>
        /// <param name="b"></param>
        public void Edit(Book b)
        {
            // Because the object b was retrieved through the same context, we don't need to do a lookup. 
            // We can just tell the context to save any changes that happened.
            context.SaveChanges();
            // Then why do we still pass the Book object all the way to the repository? Because the service
            // layer doesn't know we use EF. If in the future we decide to switch EF to something else, 
            // we won't have to change the service layer.
        }

        /// <summary>
        /// Returns a book on ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Book Find(int id)
        {
            return context.Books.Find(id);
        }

        /// <summary>
        /// Removes the selected item.
        /// </summary>
        /// <param name="item"></param>
        public void Remove(Book item)
        {
            context.Books.Remove(item);
            context.SaveChanges();
        }
    }
}