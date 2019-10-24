using Library.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services
{
    class Service<T> : IService
    {
        IRepository<T, int> repository;

        public event EventHandler Updated;
        
    }
}
