using Library.Models;
using Library.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services
{
    class AuthorService : IService
    {
        AuthorRepository authorRepository;

        public event EventHandler Updated;

        public AuthorService(RepositoryFactory rFactory)
        {
            this.authorRepository = rFactory.CreateAuthorRepository();
        }

        public IEnumerable<Author> All()
        {
            return authorRepository.All();
        }

        public void Add(string name)
        {
            if (String.IsNullOrEmpty(name))
            {
                throw new FormatException("Name can't be empty");
            }
            authorRepository.Add(new Author { Name = name });
            OnUpdate();
        }

        public Author Find(int id)
        {
            return authorRepository.Find(id);
        }
        public Author GetAuthorOnName(string name)
        {
            return authorRepository.All().Where(x => x.Name == name).FirstOrDefault();
        }

        public void OnUpdate()
        {
            Updated?.Invoke(this, new EventArgs());
        }

    }
}
