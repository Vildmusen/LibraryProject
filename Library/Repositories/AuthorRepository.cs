using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Repositories
{
    /// <summary>
    /// Class conatining operations communicating with the author table in the database.
    /// </summary>
    class AuthorRepository : IRepository<Author, int>
    {   
        private LibraryContext context;

        /// <summary>
        /// Assigns the database context.
        /// </summary>
        /// <param name="c"></param>
        public AuthorRepository(LibraryContext c)
        {
            this.context = c;
        }

        /// <summary>
        /// Adds an author to the database.
        /// </summary>
        /// <param name="item"></param>
        public void Add(Author item)
        {
            context.Authors.Add(item);
            context.SaveChanges();
        }

        /// <summary>
        /// returns all athours in the database.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Author> All()
        {
            return context.Authors;
        }

        /// <summary>
        /// Edit the current item and updates the database.
        /// </summary>
        /// <param name="item"></param>
        public void Edit(Author item)
        {
            context.SaveChanges();
        }

        /// <summary>
        /// return an author on ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Author Find(int id)
        {
            return context.Authors.Find(id);
        }

        /// <summary>
        /// Removes the selected author.
        /// </summary>
        /// <param name="item"></param>
        public void Remove(Author item)
        {
            context.Authors.Remove(item);
            context.SaveChanges();
        }
    }
}
