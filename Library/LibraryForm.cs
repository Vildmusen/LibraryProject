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
            Init();
        }

        /// <summary>
        /// Initializes all services and subscribe to all events. Refreshes content once to get it all in the system.
        /// </summary>
        private void Init()
        {
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
            
            bookService.Updated += RefreshBook;
            authorService.Updated += RefreshAuthor;
            memberService.Updated += RefreshMember;
            loanService.Updated += RefreshLoans;
            bookCopyService.Updated += RefreshBookCopies;

            RefreshAuthors();
            RefreshBooks();
            RefreshMembers();
            RefreshCopies();
            RefreshLoans();
        }

        /// <summary>
        /// Displays all loans in "lbResult"
        /// </summary>
        private void RefreshLoans() { Show(loanService.All()); }
        /// <summary>
        /// Displays all members in "lbResult"
        /// </summary>
        private void RefreshMembers() { Show(memberService.All()); }
        /// <summary>
        /// Displays all Book Copies in "lbResult"
        /// </summary>
        private void RefreshCopies() { Show(bookCopyService.All()); }
        /// <summary>
        /// Displays all Authors in "lbResult" and "author_select_combo_box"
        /// </summary>
        private void RefreshAuthors()
        {
            Show(authorService.All());
            author_select_combo_box.Items.Clear();
            foreach (Author a in authorService.All())
            {
                author_select_combo_box.Items.Add(a.ToString());
            }
        }
        /// <summary>
        /// Displays all books in "all_books_list"
        /// </summary>
        private void RefreshBooks()
        { 
            all_books_list.Items.Clear();
            foreach (Book b in bookService.All())
            {
                all_books_list.Items.Add(b);
            }
        }

        /// <summary>
        /// Method to trigger on memberService's event "Updated
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RefreshMember(object sender, EventArgs e) { RefreshMembers(); }
        /// <summary>
        /// Method to trigger on authorService's event "Updated
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RefreshAuthor(object sender, EventArgs e) { RefreshAuthors(); }
        /// <summary>
        /// Method to trigger on bookService's event "Updated
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RefreshBook(object sender, EventArgs e) { RefreshBooks(); }
        /// <summary>
        /// Method to trigger on loanService's event "Updated
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RefreshLoans(object sender, EventArgs e) { RefreshBooks(); }
        /// <summary>
        /// Method to trigger on bookcopyService's event "Updated
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RefreshBookCopies(object sender, EventArgs e) { RefreshCopies(); }

        #region DISPLAY FUNCTIONS

        /// <summary>
        /// Display a source of items in the list "lbResult".
        /// </summary>
        /// <typeparam name="T">Type of item to be displayed.</typeparam>
        /// <param name="source">List of items</param>
        private void Show<T>(IEnumerable<T> source)
        {
            resullt_lbl.Text = String.Format("Current result: ({0}s)", typeof(T).Name);
            lbResult.Items.Clear();
            foreach (T item in source)
            {
                lbResult.Items.Add(item);
            }
        }

        /// <summary>
        /// Displays details about a Book Copy in "lbDeatils".
        /// </summary>
        /// <param name="bc"></param>
        private void showBookCopyDetails(BookCopy bc)
        {
            lbDetails.Items.Add(String.Format("\"{0}\" by {1}", bc.Book.Title, bc.Book.AuthorOfBook));
            lbDetails.Items.Add(String.Format("Description: {0}", bc.Book.Description));
            lbDetails.Items.Add("");
            if (bc.State == BookCopy.Status.NOT_AVAILABLE)
            {
                lbDetails.Items.Add(String.Format("Currently occupied by: {0}", loanService.GetMemberFromCopyID(bc.CopyID).Name));
            }
            else
            {
                lbDetails.Items.Add("This book is available.");
            }
        }

        /// <summary>
        /// Displays details about a Loan in "lbDeatils".
        /// </summary>
        /// <param name="b"></param>
        private void showLoanDeatils(Loan loan)
        {
            lbDetails.Items.Add(String.Format("Loaned by user: {0}", loan.Member.Name));
            lbDetails.Items.Add(String.Format("Book: {0}", loan.BookCopy.Book.Title));
            lbDetails.Items.Add(String.Format("Starting date: {0}", loan.TimeOfLoan));
            lbDetails.Items.Add(String.Format("Due date: {0}", loan.DueDate));
            if (loan.TimeOfReturn.HasValue)
            {
                lbDetails.Items.Add(String.Format("Returned: {0}", loan.TimeOfReturn));
            }
            else
            {
                lbDetails.Items.Add("User has not returned this book.");
            }
        }

        /// <summary>
        /// Displays details about a Member in "lbDeatils".
        /// </summary>
        /// <param name="b"></param>
        private void showMemberDetails(Member member)
        {
            lbDetails.Items.Add(String.Format("Name: {0}", member.Name));
            lbDetails.Items.Add(String.Format("SSN: {0}", member.SSO));
            lbDetails.Items.Add(String.Format("Joined date: {0}", member.MemberShip.ToString()));
            lbDetails.Items.Add("");
            lbDetails.Items.Add("Currently loaned books: ");
            foreach (Loan l in member.Loans)
            {
                showBookDetailsShort(l.BookCopy.Book);
            }
        }

        /// <summary>
        /// Displays details about a Book in "lbDeatils".
        /// </summary>
        /// <param name="b"></param>
        private void showBookDetailsShort(Book b)
        {
            lbDetails.Items.Add(String.Format("\"{0}\" by {1}", b.Title, b.AuthorOfBook));
        }

        /// <summary>
        /// Displays details about a Book in "lbDeatils".
        /// </summary>
        /// <param name="b"></param>
        private void showBookDetails(Book b)
        {
            lbDetails.Items.Add(String.Format("\"{0}\" by {1}", b.Title, b.AuthorOfBook));
            lbDetails.Items.Add(String.Format("Description: {0}", b.Description));
            lbDetails.Items.Add("");
            lbDetails.Items.Add("Copies of book:");
            foreach (BookCopy bc in b.Copies)
            {
                lbDetails.Items.Add(String.Format("Copy: [{0}] {1}, Condition: {2}", bc.CopyID, bc.State, bc.Condition));
            }
        }

        /// <summary>
        /// Displays details about an Author in "lbDeatils".
        /// </summary>
        /// <param name="athor"></param>
        private void showAuthorDetails(Author author)
        {
            lbDetails.Items.Add(author.Name);
            lbDetails.Items.Add("Written books: ");
            foreach (Book b in author.WrittenBooks)
            {
                lbDetails.Items.Add(b.Title);
            }
        }

        /// <summary>
        /// Displays a message box with an error message.
        /// </summary>
        /// <param name="ex"></param>
        private void ErrorMessage(Exception ex)
        {
            MessageBox.Show("Something went wrong:\n\n" + ex.Message, "Error");
        }

        /// <summary>
        /// Disaplys a message box with a message
        /// </summary>
        /// <param name="message"></param>
        private void ErrorMessage(string message)
        {
            MessageBox.Show(message, "Error");
        }
        #endregion
    
        #region BUTTONS

        /// <summary>
        /// Calls bookService to add a book.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                ErrorMessage(ex);
            }
        }

        /// <summary>
        /// Calls bookcopyService to att a copy. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void add_copy_btn_Click(object sender, EventArgs e)
        {
            try
            {
                Book original = all_books_list.SelectedItem as Book;
                BookCopy copy = new BookCopy { Book = original, Condition = 10 };
                bookCopyService.Add(copy);
                lbDetails.Items.Clear();
                showBookDetails(original);
            }
            catch (Exception ex)
            {
                ErrorMessage(ex);
            }
        }

        /// <summary>
        /// Calls authorService to add an author.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void add_author_btn_Click(object sender, EventArgs e)
        {
            try
            {
                authorService.Add(author_name_txtbox.Text);
            }
            catch (Exception ex)
            {
                ErrorMessage(ex);
            }
        }

        /// <summary>
        /// Calls memberService to get the selected member and the loanService to save a new loan
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void loan_with_member_Click(object sender, EventArgs e)
        {
            try
            {
                Book b = all_books_list.SelectedItem as Book;
                string Memberinfo = lbResult.SelectedItem.ToString();
                Member m = memberService.GetMemberBySSN(Memberinfo.Split(':')[0].Trim());
                Loan l = new Loan
                {
                    TimeOfLoan = DateTime.Now,
                    DueDate = DateTime.Now.AddDays(15),
                    BookCopy = bookCopyService.SetLoaned(b.Copies),
                    Member = m
                };

                loanService.Add(l);
                MessageBox.Show(String.Format("Book succesfully added to {0}s loans. The due date for this return is {1}", m.Name, l.DueDate), "SUCCESS");
            }
            catch (Exception ex)
            {
                ErrorMessage(ex);
            }
        }

        /// <summary>
        /// Calls memberService to add a member.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void add_member_btn_Click(object sender, EventArgs e)
        {
            try
            {
                memberService.Add(member_name_text_box.Text, member_sso_text_box.Text);
            }
            catch (Exception ex)
            {
                ErrorMessage(ex);
            }
        }

        /// <summary>
        /// Shows all authors in the list.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void show_all_authors_Click(object sender, EventArgs e)
        {
            Show(authorService.All());
        }

        /// <summary>
        /// Shows all members in the list.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void show_all_members_Click(object sender, EventArgs e)
        {
            Show(memberService.All());
        }

        /// <summary>
        /// Shows all books in the list.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void show_all_books_btn_Click(object sender, EventArgs e)
        {
            RefreshBooks();
        }

        /// <summary>
        /// Shows all book copies in the list.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void show_all_available_btn_Click(object sender, EventArgs e)
        {
            Show(bookCopyService.All());
        }

        /// <summary>
        /// Shows all books by the selected author.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void books_by_author_btn_Click(object sender, EventArgs e)
        {
            try
            {
                all_books_list.Items.Clear();
                foreach (Book b in bookService.GetBooksByAuthor(lbResult.SelectedItem as Author))
                {
                    all_books_list.Items.Add(b);
                }
            }
            catch (Exception ex)
            {
                ErrorMessage(ex);
            }
        }

        /// <summary>
        /// Shows the books a user is in possesion of.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void books_by_member_btn_Click(object sender, EventArgs e)
        {
            try
            {
                all_books_list.Items.Clear();
                foreach (Book b in memberService.GetBooksByMemberName(lbResult.SelectedItem as Member))
                {
                    all_books_list.Items.Add(b);
                }
            }
            catch (Exception ex)
            {
                ErrorMessage(ex);
            }
        }

        /// <summary>
        /// Shows all loans in the list.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void show_loans_btn_Click(object sender, EventArgs e)
        {
            Show(loanService.All());

        }

        /// <summary>
        /// Shows all members in the list.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void show_all_members_btn_Click(object sender, EventArgs e)
        {
            Show(memberService.All());
        }

        /// <summary>
        /// Calls bookService to delete the selected book. Checks if the book has all copies AVAILABLE.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void delete_book_btn_Click(object sender, EventArgs e)
        {
            Book current = all_books_list.SelectedItem != null ? all_books_list.SelectedItem as Book : null;

            if (current != null)
            {
                DialogResult UserPrompt = MessageBox.Show("Are you sure? This will delete the current book and all of its copies.", "Warning", MessageBoxButtons.YesNo);
                if (bookService.AllCopiesAvailable(current) && UserPrompt == DialogResult.Yes)
                {
                    try
                    {
                        bookService.Delete(current);
                    }
                    catch (Exception ex)
                    {
                        ErrorMessage(ex);
                    }
                }
                else
                {
                    ErrorMessage("Chosen book has copies that are unavailable");
                }
            }
            else
            {
                ErrorMessage("Please choose a book");
            }
        }

        #endregion

        #region LIST INTERACTION

        /// <summary>
        /// Listen to a change of selected index and decode what type the item is. Runs the corresponding show-deatil-function.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbBooks_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbDetails.Items.Clear();
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
                else if (lbResult.SelectedItem.GetType() == typeof(BookCopy))
                {
                    showBookCopyDetails(lbResult.SelectedItem as BookCopy);
                }
                else if (lbResult.SelectedItem.GetType() == typeof(Loan))
                {
                    showLoanDeatils(lbResult.SelectedItem as Loan);
                }
            }
        }

        /// <summary>
        /// Listens to a change of index in the book list. Display details of selected item.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void all_books_list_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (all_books_list.SelectedItem != null)
            {
                lbDetails.Items.Clear();
                showBookDetails(all_books_list.SelectedItem as Book);
            }
        }
        #endregion
    }
}
