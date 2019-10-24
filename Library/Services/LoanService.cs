﻿using Library.Models;
using Library.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services
{
    class LoanService : IService
    {
        LoanRepository loanRepository;

        public event EventHandler Updated;

        public LoanService(RepositoryFactory factory)
        {
            loanRepository = factory.CreateLoanRepository();
        }

        public IEnumerable<Loan> All()
        {
            return loanRepository.All();
        }

        public void Add(Loan l)
        {
            loanRepository.Add(l);
            OnUpdate();
        }

        public void Remove(Loan l)
        {
            loanRepository.Remove(l);
            OnUpdate();
        }

        public void Edit(Loan l)
        {
            loanRepository.Edit(l);
            OnUpdate();
        }

        public Member GetMemberFromCopyID(int copyID)
        {
            return All().Where(x => x.Member.Loans.Any(c => c.BookCopy.CopyID == copyID)).FirstOrDefault().Member;
        }
        public IEnumerable<Loan> AllAscendingOnProperty(string property)
        {
            return All().OrderBy(BuildQuery(property));
        }

        internal IEnumerable<Loan> AllDescendingOnProperty(string property)
        {
            return All().OrderByDescending(BuildQuery(property));
        }

        private Func<Loan, int> BuildQuery(string property)
        {
            var x = Expression.Parameter(typeof(Loan), "x");
            var body = Expression.PropertyOrField(x, property);
            return Expression.Lambda<Func<Loan, int>>(body, x).Compile();
        }

        public void OnUpdate()
        {
            Updated?.Invoke(this, new EventArgs());
        }
    }
}
