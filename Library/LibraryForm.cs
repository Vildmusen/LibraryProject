using Library.Models;
using Library.Repositories;
using Library.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Library
{
    public partial class LibraryForm : Form
    {
        AuthorService authorService;
        BookService bookService;
        MemberService memberService;
        LoanService loanService;
        BookCopyService bookCopyService;

        public LibraryForm()
        {
            InitializeComponent();

            // we create only one context in our application, which gets shared among repositories
            LibraryContext context = new LibraryContext();
            // we use a factory object that will create the repositories as they are needed, it also makes
            // sure all the repositories created use the same context.
            RepositoryFactory repFactory = new RepositoryFactory(context);

            this.bookService = new BookService(repFactory);
            this.authorService = new AuthorService(repFactory);
            this.memberService = new MemberService(repFactory);
            this.loanService = new LoanService(repFactory);
            this.bookCopyService = new BookCopyService(repFactory);

            Show(bookService.All());
            bookService.Updated += RefreshBook;
            authorService.Updated += RefreshAuthor;
            memberService.Updated += RefreshMember;
        }

        private void RefreshMember(object sender, EventArgs e)
        {
            Show(memberService.All());
            members_combo_box.Items.Clear();
            foreach (Member m in memberService.All())
            {
                members_combo_box.Items.Add(m.ToString());
            }
        }

        private void RefreshAuthor(object sender, EventArgs e)
        {
            Show(authorService.All());
            author_select_combo_box.Items.Clear();
            foreach (Author a in authorService.All())
            {
                author_select_combo_box.Items.Add(a.ToString());
            }
        }

        private void RefreshBook(object sender, EventArgs e)
        {
            Show(bookService.All());
        }

        private void Show<T>(IEnumerable<T> source)
        {
            lbResult.Items.Clear();
            foreach (T item in source)
            {
                lbResult.Items.Add(item);
            }
        }

        private void add_book_btn_click(object sender, EventArgs e)
        {
            try
            {
                string name = author_select_combo_box.SelectedItem.ToString();
                Author current = authorService.GetAuthorOnName(name);
                Book b = new Book
                {
                    Title = book_title_txt_box.Text,
                    Description = book_description_txt_box.Text,
                    AuthorOfBook = authorService.Find(current.AuthorID)
                };
                bookService.Add(b);
            }
            catch (Exception ex)
            {
                UserError(ex);
            }
        }

        private void add_copy_btn_Click(object sender, EventArgs e)
        {
            try
            {
                Book original = lbResult.SelectedItem as Book;
                BookCopy copy = new BookCopy { Book = original, Condition = 10 };
                original.Copies.Add(copy);
                bookCopyService.Add(copy);
            }
            catch (Exception ex)
            {
                UserError(ex);
            }

        }

        private void lbBooks_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbDetails.Items.Clear();
            // TODO SIWTH
            if (lbResult.SelectedItem != null)
            {
                if (lbResult.SelectedItem.GetType() == typeof(Book))
                {
                    showBookDetails(lbResult.SelectedItem as Book);
                }
                else if (lbResult.SelectedItem.GetType() == typeof(Author))
                {
                    showAuthorDetails(lbResult.SelectedItem as Author);
                }
                else if (lbResult.SelectedItem.GetType() == typeof(Member))
                {
                    showMemberDetails(lbResult.SelectedItem as Member);
                }
            }
        }

        private void showMemberDetails(Member member)
        {
            lbDetails.Items.Add(String.Format("Name: {0}", member.Name));
            lbDetails.Items.Add(String.Format("SSN: {0}", member.SSO));
            lbDetails.Items.Add(String.Format("Joined date: {0}", member.MemberShip.ToString()));
            lbDetails.Items.Add("Loaned books: ");
            foreach (Loan l in member.Loans)
            {
                showBookDetails(l.Book.Book);
            }
        }

        private void showBookDetails(Book b)
        {
            lbDetails.Items.Add(String.Format("\"{0}\" by {1}", b.Title, b.AuthorOfBook));
            lbDetails.Items.Add(String.Format("Description: {0}", b.Description));
            foreach (BookCopy bc in b.Copies)
            {
                lbDetails.Items.Add(String.Format("Copy: {0}, Condition: {1}", bc.State, bc.Condition));
            }
        }

        private void showAuthorDetails(Author author)
        {
            lbDetails.Items.Add(author.Name);
            lbDetails.Items.Add("Written books: ");
            foreach (Book b in author.WrittenBooks)
            {
                lbDetails.Items.Add(b.Title);
            }
        }

        private void show_all_authors_Click(object sender, EventArgs e)
        {
            Show(authorService.All());
        }

        private void add_author_btn_Click(object sender, EventArgs e)
        {
            try
            {
                authorService.Add(author_name_txtbox.Text);
            }
            catch (Exception ex)
            {
                UserError(ex);
            }
        }

        private void add_member_btn_Click(object sender, EventArgs e)
        {
            try
            {
                memberService.Add(member_name_text_box.Text, member_sso_text_box.Text);
            }
            catch (Exception ex)
            {
                UserError(ex);
            }
        }

        private void UserError(Exception ex)
        {
            MessageBox.Show("Something went wrong:\n\n" + ex.Message, "Error");
        }

        private void show_all_members_Click(object sender, EventArgs e)
        {
            Show(memberService.All());
        }

        private void show_all_books_btn_Click(object sender, EventArgs e)
        {
            Show(bookService.All());
        }

        private void show_all_available_btn_Click(object sender, EventArgs e)
        {
            Show(bookService.AllAvailable());
        }

        private void books_by_author_btn_Click(object sender, EventArgs e)
        {
            try
            {
                Show(bookService.GetBooksByAuthor(lbResult.SelectedItem as Author));
            }
            catch (Exception ex)
            {
                UserError(ex);
            }
        }

        private void books_by_member_btn_Click(object sender, EventArgs e)
        {
            try
            {
                Show(memberService.GetBooksByMemberName(lbResult.SelectedItem as Member));
            }
            catch (Exception ex)
            {
                UserError(ex);
            }
        }

        private void loan_with_member_Click(object sender, EventArgs e)
        {
            try
            {
                Book b = lbResult.SelectedItem as Book;
                string Memberinfo = members_combo_box.SelectedItem.ToString();
                Member m = memberService.GetMemberBySSN(Memberinfo.Split(':')[0].Trim());
                Loan l = new Loan
                {
                    TimeOfLoan = DateTime.Now,
                    DueDate = DateTime.Now.AddDays(15),
                    Book = bookCopyService.SetLoaned(b.Copies),
                    Member = m
                };

                memberService.UpdateMemberLoans(m, l);

            }
            catch (Exception ex)
            {
                UserError(ex);
            }
        }

        private void show_loans_btn_Click(object sender, EventArgs e)
        {
            Show(loanService.All());

        }
    }
}
