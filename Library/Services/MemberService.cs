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
    class MemberService : IService
    {
        MemberRepository memberRepository;

        public event EventHandler Updated;

        public MemberService(RepositoryFactory rFactory)
        {
            this.memberRepository = rFactory.CreateMemberRepository();
        }

        public IEnumerable<Member> All()
        {
            return memberRepository.All();
        }

        public void Add(string name, string sso)
        {
            if (String.IsNullOrEmpty(name))
            {
                throw new FormatException("Name can't be empty");
            }
            memberRepository.Add(new Member { Name = name, SSO = sso, MemberShip = DateTime.Now });
            OnUpdate();
        }

        public void Remove(Member m)
        {
            // TODO Can't remove if currently has a loan.
            memberRepository.Remove(m);
            OnUpdate();
        }

        public IEnumerable<BookCopy> GetBookCopysByMemberName(Member member)
        {
            return member.Loans.Where(b => b.BookCopy.State == BookCopy.Status.NOT_AVAILABLE).Select(b => b.BookCopy);
        }

        public IEnumerable<Member> AllAscendingOnProperty(string property)
        {
            return All().OrderBy(BuildQuery(property));
        }

        internal IEnumerable<Member> AllDescendingOnProperty(string property)
        {
            return All().OrderByDescending(BuildQuery(property));
        }

        private Func<Member, int> BuildQuery(string property)
        {
            var x = Expression.Parameter(typeof(Member), "x");
            var body = Expression.PropertyOrField(x, property);
            return Expression.Lambda<Func<Member, int>>(body, x).Compile();
        }

        public void UpdateMemberLoans(Member member, Loan loan)
        {
            member.Loans.Add(loan);
        }

        public Member GetMemberBySSN(string SSN)
        {
            return memberRepository.All().Where(m => m.SSO == SSN).FirstOrDefault();
        }

        public void OnUpdate()
        {
            Updated?.Invoke(this, new EventArgs());
        }

    }
}
