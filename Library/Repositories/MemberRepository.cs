using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Repositories
{
    /// <summary>
    /// Class containing operations communicating with the Member table in the database.
    /// </summary>
    class MemberRepository : IRepository<Member, int>
    {
        private LibraryContext context;

        /// <summary>
        /// Assigns the database context.
        /// </summary>
        /// <param name="c"></param>
        public MemberRepository(LibraryContext c)
        {
            this.context = c;
        }

        /// <summary>
        /// Adds a member to the database.
        /// </summary>
        /// <param name="item"></param>
        public void Add(Member item)
        {
            context.Members.Add(item);
            context.SaveChanges();
        }

        /// <summary>
        /// Returns all members rfom the database.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Member> All()
        {
            return context.Members;
        }

        /// <summary>
        /// Edits a member and save the changes to the database.
        /// </summary>
        /// <param name="item"></param>
        public void Edit(Member item)
        {
            context.SaveChanges();
        }

        /// <summary>
        /// Returns a member on ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Member Find(int id)
        {
            return context.Members.Find(id);
        }

        /// <summary>
        /// Remvoes the selected member from the database.
        /// </summary>
        /// <param name="item"></param>
        public void Remove(Member item)
        {
            context.Members.Remove(item);
            context.SaveChanges();
        }
    }
}
