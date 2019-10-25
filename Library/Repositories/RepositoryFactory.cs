using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Repositories
{
    /// <summary>
    /// Class responsible for creating each individual tables repository-classes.
    /// </summary>
    class RepositoryFactory
    {
        private LibraryContext context;

        /// <summary>
        /// Assigns the context.
        /// </summary>
        /// <param name="c">Context shared between repositories.</param>
        public RepositoryFactory(LibraryContext c)
        {
            this.context = c;
        }

        /// <summary>
        /// Creates a book repository. 
        /// </summary>
        /// <returns></returns>
        public BookRepository CreateBookRepository()
        {
            return new BookRepository(context);
        }

        /// <summary>
        /// Creates a Book Copy repository.
        /// </summary>
        /// <returns></returns>
        public BookCopyRepository CreateBookCopyRepository()
        {
            return new BookCopyRepository(context);
        }

        /// <summary>
        /// Creates an Author repository.
        /// </summary>
        /// <returns></returns>
        public AuthorRepository CreateAuthorRepository()
        {
            return new AuthorRepository(context);
        }

        /// <summary>
        /// Creates a Member repository.
        /// </summary>
        /// <returns></returns>
        public MemberRepository CreateMemberRepository()
        {
            return new MemberRepository(context);
        }

        /// <summary>
        /// Creates a Loan repository.
        /// </summary>
        /// <returns></returns>
        public LoanRepository CreateLoanRepository()
        {
            return new LoanRepository(context);
        }
    }
}
