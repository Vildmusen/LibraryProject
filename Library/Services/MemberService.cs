using Library.Models;
using Library.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public IEnumerable<Book> GetBooksByMemberName(Member member)
        {
            return member.Loans.Select(b => b.Book.Book);
        }

        public void OnUpdate()
        {
            Updated?.Invoke(this, new EventArgs());
        }

        public void UpdateMemberLoans(Member member, Loan loan)
        {
            member.Loans.Add(loan);
        }

        public Member GetMemberBySSN(string SSN)
        {
            return memberRepository.All().Where(m => m.SSO == SSN).FirstOrDefault();
        }
    }
}
