using Library.Models;
using Library.Repositories;
using Library.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Library
{
    public partial class LibraryForm : Form
    {
        private AuthorService authorService;
        private BookService bookService;
        private MemberService memberService;
        private LoanService loanService;
        private BookCopyService bookCopyService;

        private Type lastShowed;

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
            
            RefreshBooks();
            RefreshAuthors();
            RefreshCopies();
            RefreshLoans();
            RefreshMembers();

            property_combobox.Text = "SELECT PROPERTY";
        }

        #region REFRESH

        /// <summary>
        /// Displays all loans in "lbResult". Checks if a loan is overdue and changes the status of the copy accordingly.
        /// </summary>
        private void RefreshLoans()
        {
            List<Loan> allLoans = loanService.All().ToList();
            foreach (Loan l in allLoans)
            {
                //if(l.BookCopy.State == BookCopy.Status.ON_LOAN && DateTime.Compare(DateTime.Now, l.DueDate) > 0)
                //{
                //    l.BookCopy.State = BookCopy.Status.OVERDUE;
                //    bookCopyService.Edit(l.BookCopy);
                //}
            }
            Show(allLoans);
        }
        /// <summary>
        /// Displays all Book Copies in "lbResult"
        /// </summary>
        private void RefreshCopies() { Show(bookCopyService.All()); }
        /// <summary>
        /// Displays all members in "lbResult" and "member_combobox"
        /// </summary>
        private void RefreshMembers()
        {
            Show(memberService.All());
            members_combobox.Items.Clear();
            foreach (Member m in memberService.All())
            {
                 members_combobox.Items.Add(m.ToString());
            }
        }
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
        private void RefreshLoans(object sender, EventArgs e) { RefreshLoans(); }
        /// <summary>
        /// Method to trigger on bookcopyService's event "Updated
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RefreshBookCopies(object sender, EventArgs e) { RefreshCopies(); }

        #endregion

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
            if(source.ToList().Count() > 0)
            {
                foreach (T item in source)
                {
                    lbResult.Items.Add(item);    
                }
                lastShowed = source.FirstOrDefault().GetType();
                setPropertyBox(lastShowed);
            }
        }

        /// <summary>
        /// Fills the combobox "property_combobox" with the current items properties.
        /// </summary>
        /// <param name="type"></param>
        private void setPropertyBox(Type type)
        {
            PropertyInfo[] properties = type.GetProperties();
            property_combobox.Items.Clear();
            property_combobox.Text = "SELECT PROPERTY";
            foreach(PropertyInfo p in properties)
            {
                if(p.PropertyType == typeof(int))
                {
                    property_combobox.Items.Add(p.Name);
                }
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
            lbDetails.Items.Add(String.Format("Condition: {0}", bc.Condition));
            lbDetails.Items.Add("");
            if (bc.State == BookCopy.Status.ON_LOAN || bc.State == BookCopy.Status.OVERDUE)
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
            lbDetails.Items.Add("");
            lbDetails.Items.Add(String.Format("Starting date: {0}", loan.TimeOfLoan));
            lbDetails.Items.Add(String.Format("Due date: {0}", loan.DueDate));
            lbDetails.Items.Add("");
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
            lbDetails.Items.Add("");
            lbDetails.Items.Add(String.Format("Joined date: {0}", member.MemberShip.ToString()));
            lbDetails.Items.Add("");
            lbDetails.Items.Add("History: ");
            foreach (Loan l in member.Loans)
            {
                lbDetails.Items.Add("");
                if(l.TimeOfReturn != null)
                {
                    lbDetails.Items.Add(String.Format("Between: {0} - {1}", l.TimeOfLoan.ToShortDateString(), l.TimeOfReturn?.ToShortDateString()));
                } else
                {
                    lbDetails.Items.Add(String.Format("Between: {0} - NOW", l.TimeOfLoan.ToShortDateString()));
                }
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
        /// Displays details about a Book in "book_details_listbox".
        /// </summary>
        /// <param name="b"></param>
        private void showBookDetailsBooksTab(Book b)
        {
            books_details_lstbox.Items.Add(String.Format("\"{0}\" by {1}", b.Title, b.AuthorOfBook));
            books_details_lstbox.Items.Add(String.Format("Description: {0}", b.Description));
            books_details_lstbox.Items.Add("");
            books_details_lstbox.Items.Add("Copies of book:");
            foreach (BookCopy bc in b.Copies)
            {
                books_details_lstbox.Items.Add(String.Format("Copy: [{0}] {1}, Condition: {2}", bc.CopyID, bc.State, bc.Condition));
            }
        }

        /// <summary>
        /// Displays details about an Author in "lbDeatils".
        /// </summary>
        /// <param name="athor"></param>
        private void showAuthorDetails(Author author)
        {
            lbDetails.Items.Add(String.Format("Author name: {0}", author.Name));
            lbDetails.Items.Add("");
            lbDetails.Items.Add("This author has written these books: ");
            foreach (Book b in author.WrittenBooks)
            {
                lbDetails.Items.Add(String.Format("[{0}] - {1}", b.BookID ,b.Title));
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

        /// <summary>
        /// Displays a dialog that prompts the user to answer yes/no.
        /// </summary>
        /// <returns></returns>
        private bool UserVerification()
        {
            DialogResult UserPrompt = MessageBox.Show("Are you sure? This will delete the selected item.", "Warning", MessageBoxButtons.YesNo);
            return UserPrompt == DialogResult.Yes;
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
                if(!String.IsNullOrEmpty(book_description_txt_box.Text) && !(String.IsNullOrEmpty(book_title_txt_box.Text))){

                    string name = author_select_combo_box.SelectedItem.ToString();
                    Author current = authorService.GetAuthorOnName(name);
                    Book b = new Book
                    {
                        Title = book_title_txt_box.Text,
                        Description = book_description_txt_box.Text,
                        AuthorOfBook = authorService.Find(current.AuthorID)
                    };
                    bookService.Add(b);
                } else
                {
                    ErrorMessage("Please fill in all fields");
                }
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
                if(all_books_list.SelectedItem is Book original)
                {
                BookCopy copy = new BookCopy { Book = original, Condition = 10 };
                bookCopyService.Add(copy);
                books_details_lstbox.Items.Clear();
                showBookDetailsBooksTab(original);
                } else
                {
                    ErrorMessage("Please choose a book to copy");
                }
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
                MessageBox.Show("Succesfully added author", "Success");
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
                BookCopy bc = lbResult.SelectedItem as BookCopy;
                if (members_combobox.SelectedItem != null)
                {
                    string Memberinfo = members_combobox.SelectedItem.ToString();
                    if (memberService.GetMemberBySSN(Memberinfo.Split(':')[0].Trim()) is Member m)
                    {
                        Loan l = new Loan
                        {
                            TimeOfLoan = DateTime.Now,
                            DueDate = DateTime.Now.AddDays(15),
                            BookCopy = bookCopyService.SetLoaned(bc),
                            Member = m
                        };

                        loanService.Add(l);
                        MessageBox.Show(String.Format("Book succesfully added to {0}s loans. The due date for this return is {1}", m.Name, l.DueDate), "SUCCESS");
                    }
                }
                else
                {
                    ErrorMessage("Could not find user, please try again");
                }
                
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
                MessageBox.Show("Succesfully added member", "Sucess");
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
                if(lbResult.SelectedItem is Author a)
                {
                    Show(bookService.GetBooksByAuthor(a));
                } else
                {
                    ErrorMessage("Select an author from the list.");
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
                lbResult.Items.Clear();
                if(members_combobox.SelectedItem != null)
                {
                    string Memberinfo = members_combobox.SelectedItem.ToString();
                    if (memberService.GetMemberBySSN(Memberinfo.Split(':')[0].Trim()) is Member m)
                    {
                        Show(loanService.GetLoansByMember(m));
                    }
                } else
                {
                    ErrorMessage("Could not find user");
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
                if (bookService.AllCopiesAvailable(current) && UserVerification())
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
                    ErrorMessage("Chosen book currently has one or more copies occupied by users.");
                }
            }
            else
            {
                ErrorMessage("Please choose a book");
            }
        }

        /// <summary>
        /// Calls loanService to edit a loan as returned.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void return_book_btn_Click(object sender, EventArgs e)
        {
            if (lbResult.SelectedItem is Loan l)
            {
                try
                {
                    l.TimeOfReturn = DateTime.Now;
                    double diff = (l.DueDate - DateTime.Now).TotalDays;
                    if (diff < 0) { ErrorMessage(String.Format("This book is late, you need to pay {0}kr", Math.Abs((int)diff * 10))); }
                    l.BookCopy.State = BookCopy.Status.RETURNED;
                    Random rand = new Random();
                    l.BookCopy.Condition = l.BookCopy.Condition -= rand.Next(3) >= 0 ? l.BookCopy.Condition -= rand.Next(3) : 0;
                    loanService.Edit(l);
                } catch (Exception ex)
                {
                    ErrorMessage(ex);
                }
            }
            else
            {
                ErrorMessage("Please select a loan");
            }
        }

        /// <summary>
        /// Deletes the selected item.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void delete_itam_btn_Click(object sender, EventArgs e)
        {
            if(lbResult.SelectedItem != null && UserVerification())
            {
                try
                {
                    if (lbResult.SelectedItem.GetType() == typeof(Book))
                    {
                        bookService.Remove(lbResult.SelectedItem as Book);
                    }
                    else if (lbResult.SelectedItem.GetType() == typeof(Author))
                    {
                        authorService.Remove(lbResult.SelectedItem as Author);
                    }
                    else if (lbResult.SelectedItem.GetType() == typeof(Member))
                    {
                        memberService.Remove(lbResult.SelectedItem as Member);
                    }
                    else if (lbResult.SelectedItem.GetType() == typeof(BookCopy))
                    {
                        bookCopyService.Remove(lbResult.SelectedItem as BookCopy);
                    }
                    else if (lbResult.SelectedItem.GetType() == typeof(Loan))
                    {
                        loanService.Remove(lbResult.SelectedItem as Loan);
                    }
                } catch (Exception ex)
                {
                    ErrorMessage(ex);
                }
            }
        }


        /// <summary>
        /// Toggles the sorting of the list from asencding/descending.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ascend_descend_btn(object sender, EventArgs e)
        {
            sort_btn_click(sender, e);
            button1.Text = button1.Text == "^" ? "v" : "^";
        }

        /// <summary>
        /// Sorts the current list on a selected property from a combo box according to the set ascending/descending value.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sort_btn_click(object sender, EventArgs e)
        {
            bool Ascending = button1.Text == "^" ? true : false;
            if(property_combobox.Items.Count == 0) { ErrorMessage("Nothing to sort"); return; }
            string propertyText = property_combobox.Text == "SELECT PROPERTY" ? property_combobox.Items[0].ToString() : property_combobox.Text;

            if (lastShowed == typeof(Book))
            {
                if (Ascending) Show(bookService.AllAscendingOnProperty(propertyText));
                else Show(bookService.AllDescendingOnProperty(propertyText));
            }
            else if (lastShowed == typeof(BookCopy))
            {
                if (Ascending) Show(bookCopyService.AllAscendingOnProperty(propertyText));
                else Show(bookCopyService.AllDescendingOnProperty(propertyText));
            }
            else if (lastShowed == typeof(Member))
            {
                if (Ascending) Show(memberService.AllAscendingOnProperty(propertyText));
                else Show(memberService.AllDescendingOnProperty(propertyText));
            }
            else if (lastShowed == typeof(Author))
            {
                if (Ascending) Show(authorService.AllAscendingOnProperty(propertyText));
                else Show(authorService.AllDescendingOnProperty(propertyText));
            }
            else if (lastShowed == typeof(Loan))
            {
                if (Ascending) Show(loanService.AllAscendingOnProperty(propertyText));
                else Show(loanService.AllDescendingOnProperty(propertyText));
            }
            setPropertyBox(lastShowed);
        }

        /// <summary>
        /// Shows all books with atleast 1 copy that is available.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void show_available_books_Click(object sender, EventArgs e)
        {
            all_books_list.Items.Clear();
            foreach (Book b in bookService.AllAvailable())
            {
                all_books_list.Items.Add(b);
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
                try
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
                } catch (Exception ex)
                {
                    ErrorMessage(ex.Message);
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
                books_details_lstbox.Items.Clear();
                showBookDetailsBooksTab(all_books_list.SelectedItem as Book);
            }
        }
        #endregion
    }
}
