using Library.Models;
using Library.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services
{
    /// <summary>
    /// Class responsible for operations triggered in the GUI about Authors.
    /// </summary>
    class AuthorService : IService
    {
        private AuthorRepository authorRepository;

        /// <summary>
        /// Event representing a change in the current author collection.
        /// </summary>
        public event EventHandler Updated;

        /// <summary>
        /// Creates and assigns a repository for Authors.
        /// </summary>
        /// <param name="rFactory"></param>
        public AuthorService(RepositoryFactory rFactory)
        {
            this.authorRepository = rFactory.CreateAuthorRepository();
        }

        /// <summary>
        /// Returns all authors
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Author> All()
        {
            return authorRepository.All();
        }

        /// <summary>
        /// Adds an author by calling the repositorys add function.
        /// </summary>
        /// <param name="name"></param>
        public void Add(string name)
        {
            if (String.IsNullOrEmpty(name))
            {
                throw new FormatException("Name can't be empty");
            }
            authorRepository.Add(new Author { Name = name });
            OnUpdate();
        }

        /// <summary>
        /// Removes an author by calling the repositorys remove function.
        /// </summary>
        /// <param name="a"></param>
        public void Remove(Author a)
        {
            authorRepository.Remove(a);
            OnUpdate();
        }

        /// <summary>
        /// Returns an author on ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Author Find(int id)
        {
            return authorRepository.Find(id);
        }

        /// <summary>
        /// Returns an author on Name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Author GetAuthorOnName(string name)
        {
            return authorRepository.All().Where(x => x.Name == name).FirstOrDefault();
        }

        /// <summary>
        /// Returns all authors, sorted ascending on a property
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public IEnumerable<Author> AllAscendingOnProperty(string property)
        {
            return All().OrderBy(BuildQuery(property));
        }

        /// <summary>
        /// Returns all authors, sorted descending on a property.
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        internal IEnumerable<Author> AllDescendingOnProperty(string property)
        {
            return All().OrderByDescending(BuildQuery(property));
        }

        // Builds a query on a property name. Only works with integer properties.
        // Credit to Balazs Tihanyi on https://stackoverflow.com/questions/9505189/dynamically-generate-linq-queries for inspiration.
        private Func<Author, int> BuildQuery(string property)
        {
            var x = Expression.Parameter(typeof(Author), "x");
            var body = Expression.PropertyOrField(x, property);
            return Expression.Lambda<Func<Author, int>>(body, x).Compile();
        }

        /// <summary>
        /// Invokes update event.
        /// </summary>
        public void OnUpdate()
        {
            Updated?.Invoke(this, new EventArgs());
        }

    }
}
