﻿namespace Library {
    partial class LibraryForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.lbResult = new System.Windows.Forms.ListBox();
            this.add_book_btn = new System.Windows.Forms.Button();
            this.library_tab_ctrl = new System.Windows.Forms.TabControl();
            this.book_page_tab = new System.Windows.Forms.TabPage();
            this.delete_book_btn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lbl1 = new System.Windows.Forms.Label();
            this.show_all_available_btn = new System.Windows.Forms.Button();
            this.show_all_books_btn = new System.Windows.Forms.Button();
            this.author_select_combo_box = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.book_description_txt_box = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.book_title_txt_box = new System.Windows.Forms.TextBox();
            this.add_copy_btn = new System.Windows.Forms.Button();
            this.Loan_page_tab = new System.Windows.Forms.TabPage();
            this.loan_with_member = new System.Windows.Forms.Button();
            this.show_all_members_btn = new System.Windows.Forms.Button();
            this.show_loans_btn = new System.Windows.Forms.Button();
            this.mem_auth_page_tab = new System.Windows.Forms.TabPage();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.books_by_member_btn = new System.Windows.Forms.Button();
            this.books_by_author_btn = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.add_author_btn = new System.Windows.Forms.Button();
            this.author_name_txtbox = new System.Windows.Forms.TextBox();
            this.Member = new System.Windows.Forms.TabPage();
            this.label5 = new System.Windows.Forms.Label();
            this.member_sso_text_box = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.add_member_btn = new System.Windows.Forms.Button();
            this.member_name_text_box = new System.Windows.Forms.TextBox();
            this.show_all_authors = new System.Windows.Forms.Button();
            this.show_all_members = new System.Windows.Forms.Button();
            this.resullt_lbl = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lbDetails = new System.Windows.Forms.ListBox();
            this.Error = new System.Windows.Forms.Label();
            this.all_books_lbl = new System.Windows.Forms.Label();
            this.all_books_list = new System.Windows.Forms.ListBox();
            this.show_all_btn2 = new System.Windows.Forms.Button();
            this.library_tab_ctrl.SuspendLayout();
            this.book_page_tab.SuspendLayout();
            this.Loan_page_tab.SuspendLayout();
            this.mem_auth_page_tab.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.Member.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbResult
            // 
            this.lbResult.FormattingEnabled = true;
            this.lbResult.Location = new System.Drawing.Point(342, 31);
            this.lbResult.Name = "lbResult";
            this.lbResult.Size = new System.Drawing.Size(300, 342);
            this.lbResult.TabIndex = 0;
            this.lbResult.SelectedIndexChanged += new System.EventHandler(this.lbBooks_SelectedIndexChanged);
            // 
            // add_book_btn
            // 
            this.add_book_btn.Location = new System.Drawing.Point(15, 486);
            this.add_book_btn.Name = "add_book_btn";
            this.add_book_btn.Size = new System.Drawing.Size(75, 23);
            this.add_book_btn.TabIndex = 2;
            this.add_book_btn.Text = "Add";
            this.add_book_btn.UseVisualStyleBackColor = true;
            this.add_book_btn.Click += new System.EventHandler(this.add_book_btn_click);
            // 
            // library_tab_ctrl
            // 
            this.library_tab_ctrl.Controls.Add(this.book_page_tab);
            this.library_tab_ctrl.Controls.Add(this.Loan_page_tab);
            this.library_tab_ctrl.Controls.Add(this.mem_auth_page_tab);
            this.library_tab_ctrl.Location = new System.Drawing.Point(9, 11);
            this.library_tab_ctrl.Name = "library_tab_ctrl";
            this.library_tab_ctrl.SelectedIndex = 0;
            this.library_tab_ctrl.Size = new System.Drawing.Size(324, 556);
            this.library_tab_ctrl.TabIndex = 3;
            // 
            // book_page_tab
            // 
            this.book_page_tab.Controls.Add(this.delete_book_btn);
            this.book_page_tab.Controls.Add(this.label1);
            this.book_page_tab.Controls.Add(this.lbl1);
            this.book_page_tab.Controls.Add(this.show_all_available_btn);
            this.book_page_tab.Controls.Add(this.show_all_books_btn);
            this.book_page_tab.Controls.Add(this.author_select_combo_box);
            this.book_page_tab.Controls.Add(this.label8);
            this.book_page_tab.Controls.Add(this.label6);
            this.book_page_tab.Controls.Add(this.book_description_txt_box);
            this.book_page_tab.Controls.Add(this.label7);
            this.book_page_tab.Controls.Add(this.book_title_txt_box);
            this.book_page_tab.Controls.Add(this.add_copy_btn);
            this.book_page_tab.Controls.Add(this.add_book_btn);
            this.book_page_tab.Location = new System.Drawing.Point(4, 22);
            this.book_page_tab.Name = "book_page_tab";
            this.book_page_tab.Padding = new System.Windows.Forms.Padding(3);
            this.book_page_tab.Size = new System.Drawing.Size(316, 530);
            this.book_page_tab.TabIndex = 0;
            this.book_page_tab.Text = "Books";
            this.book_page_tab.UseVisualStyleBackColor = true;
            // 
            // delete_book_btn
            // 
            this.delete_book_btn.Location = new System.Drawing.Point(6, 156);
            this.delete_book_btn.Name = "delete_book_btn";
            this.delete_book_btn.Size = new System.Drawing.Size(69, 69);
            this.delete_book_btn.TabIndex = 20;
            this.delete_book_btn.Text = "Delete Selected";
            this.delete_book_btn.UseVisualStyleBackColor = true;
            this.delete_book_btn.Click += new System.EventHandler(this.delete_book_btn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.label1.Location = new System.Drawing.Point(13, 302);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 24);
            this.label1.TabIndex = 19;
            this.label1.Text = "New book";
            // 
            // lbl1
            // 
            this.lbl1.AutoSize = true;
            this.lbl1.Location = new System.Drawing.Point(-7, 318);
            this.lbl1.Name = "lbl1";
            this.lbl1.Size = new System.Drawing.Size(349, 13);
            this.lbl1.TabIndex = 18;
            this.lbl1.Text = "________________________________________________________--";
            // 
            // show_all_available_btn
            // 
            this.show_all_available_btn.Location = new System.Drawing.Point(241, 6);
            this.show_all_available_btn.Name = "show_all_available_btn";
            this.show_all_available_btn.Size = new System.Drawing.Size(69, 69);
            this.show_all_available_btn.TabIndex = 17;
            this.show_all_available_btn.Text = "Show all copies";
            this.show_all_available_btn.UseVisualStyleBackColor = true;
            this.show_all_available_btn.Click += new System.EventHandler(this.show_all_available_btn_Click);
            // 
            // show_all_books_btn
            // 
            this.show_all_books_btn.Location = new System.Drawing.Point(5, 6);
            this.show_all_books_btn.Name = "show_all_books_btn";
            this.show_all_books_btn.Size = new System.Drawing.Size(69, 69);
            this.show_all_books_btn.TabIndex = 16;
            this.show_all_books_btn.Text = "Refresh All Books";
            this.show_all_books_btn.UseVisualStyleBackColor = true;
            this.show_all_books_btn.Click += new System.EventHandler(this.show_all_books_btn_Click);
            // 
            // author_select_combo_box
            // 
            this.author_select_combo_box.FormattingEnabled = true;
            this.author_select_combo_box.Location = new System.Drawing.Point(17, 451);
            this.author_select_combo_box.Name = "author_select_combo_box";
            this.author_select_combo_box.Size = new System.Drawing.Size(227, 21);
            this.author_select_combo_box.TabIndex = 15;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(18, 434);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(38, 13);
            this.label8.TabIndex = 14;
            this.label8.Text = "Author";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(18, 391);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(60, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Description";
            // 
            // book_description_txt_box
            // 
            this.book_description_txt_box.Location = new System.Drawing.Point(17, 411);
            this.book_description_txt_box.Name = "book_description_txt_box";
            this.book_description_txt_box.Size = new System.Drawing.Size(227, 20);
            this.book_description_txt_box.TabIndex = 11;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(18, 348);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(27, 13);
            this.label7.TabIndex = 10;
            this.label7.Text = "Title";
            // 
            // book_title_txt_box
            // 
            this.book_title_txt_box.Location = new System.Drawing.Point(17, 368);
            this.book_title_txt_box.Name = "book_title_txt_box";
            this.book_title_txt_box.Size = new System.Drawing.Size(227, 20);
            this.book_title_txt_box.TabIndex = 9;
            // 
            // add_copy_btn
            // 
            this.add_copy_btn.Location = new System.Drawing.Point(6, 81);
            this.add_copy_btn.Name = "add_copy_btn";
            this.add_copy_btn.Size = new System.Drawing.Size(69, 69);
            this.add_copy_btn.TabIndex = 3;
            this.add_copy_btn.Text = "Add copy of selected";
            this.add_copy_btn.UseVisualStyleBackColor = true;
            this.add_copy_btn.Click += new System.EventHandler(this.add_copy_btn_Click);
            // 
            // Loan_page_tab
            // 
            this.Loan_page_tab.Controls.Add(this.show_all_btn2);
            this.Loan_page_tab.Controls.Add(this.loan_with_member);
            this.Loan_page_tab.Controls.Add(this.show_all_members_btn);
            this.Loan_page_tab.Controls.Add(this.show_loans_btn);
            this.Loan_page_tab.Location = new System.Drawing.Point(4, 22);
            this.Loan_page_tab.Name = "Loan_page_tab";
            this.Loan_page_tab.Padding = new System.Windows.Forms.Padding(3);
            this.Loan_page_tab.Size = new System.Drawing.Size(316, 530);
            this.Loan_page_tab.TabIndex = 1;
            this.Loan_page_tab.Text = "Loans";
            this.Loan_page_tab.UseVisualStyleBackColor = true;
            // 
            // loan_with_member
            // 
            this.loan_with_member.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.25F);
            this.loan_with_member.Location = new System.Drawing.Point(241, 156);
            this.loan_with_member.Name = "loan_with_member";
            this.loan_with_member.Size = new System.Drawing.Size(69, 69);
            this.loan_with_member.TabIndex = 20;
            this.loan_with_member.Text = "Loan Selected Book to Selected Member";
            this.loan_with_member.UseVisualStyleBackColor = true;
            this.loan_with_member.Click += new System.EventHandler(this.loan_with_member_Click);
            // 
            // show_all_members_btn
            // 
            this.show_all_members_btn.Location = new System.Drawing.Point(241, 6);
            this.show_all_members_btn.Name = "show_all_members_btn";
            this.show_all_members_btn.Size = new System.Drawing.Size(69, 69);
            this.show_all_members_btn.TabIndex = 18;
            this.show_all_members_btn.Text = "Show all members";
            this.show_all_members_btn.UseVisualStyleBackColor = true;
            this.show_all_members_btn.Click += new System.EventHandler(this.show_all_members_btn_Click);
            // 
            // show_loans_btn
            // 
            this.show_loans_btn.Location = new System.Drawing.Point(241, 81);
            this.show_loans_btn.Name = "show_loans_btn";
            this.show_loans_btn.Size = new System.Drawing.Size(69, 69);
            this.show_loans_btn.TabIndex = 17;
            this.show_loans_btn.Text = "Show loans by all members";
            this.show_loans_btn.UseVisualStyleBackColor = true;
            this.show_loans_btn.Click += new System.EventHandler(this.show_loans_btn_Click);
            // 
            // mem_auth_page_tab
            // 
            this.mem_auth_page_tab.Controls.Add(this.label9);
            this.mem_auth_page_tab.Controls.Add(this.label10);
            this.mem_auth_page_tab.Controls.Add(this.books_by_member_btn);
            this.mem_auth_page_tab.Controls.Add(this.books_by_author_btn);
            this.mem_auth_page_tab.Controls.Add(this.tabControl1);
            this.mem_auth_page_tab.Controls.Add(this.show_all_authors);
            this.mem_auth_page_tab.Controls.Add(this.show_all_members);
            this.mem_auth_page_tab.Location = new System.Drawing.Point(4, 22);
            this.mem_auth_page_tab.Name = "mem_auth_page_tab";
            this.mem_auth_page_tab.Size = new System.Drawing.Size(316, 530);
            this.mem_auth_page_tab.TabIndex = 2;
            this.mem_auth_page_tab.Text = "Members and Authors";
            this.mem_auth_page_tab.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.label9.Location = new System.Drawing.Point(5, 298);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(196, 24);
            this.label9.TabIndex = 21;
            this.label9.Text = "New Author / Member";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(-15, 314);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(349, 13);
            this.label10.TabIndex = 20;
            this.label10.Text = "________________________________________________________--";
            // 
            // books_by_member_btn
            // 
            this.books_by_member_btn.Location = new System.Drawing.Point(5, 78);
            this.books_by_member_btn.Name = "books_by_member_btn";
            this.books_by_member_btn.Size = new System.Drawing.Size(69, 69);
            this.books_by_member_btn.TabIndex = 5;
            this.books_by_member_btn.Text = "Show books in user possesion";
            this.books_by_member_btn.UseVisualStyleBackColor = true;
            this.books_by_member_btn.Click += new System.EventHandler(this.books_by_member_btn_Click);
            // 
            // books_by_author_btn
            // 
            this.books_by_author_btn.Location = new System.Drawing.Point(240, 78);
            this.books_by_author_btn.Name = "books_by_author_btn";
            this.books_by_author_btn.Size = new System.Drawing.Size(69, 69);
            this.books_by_author_btn.TabIndex = 4;
            this.books_by_author_btn.Text = "Show books by selected author";
            this.books_by_author_btn.UseVisualStyleBackColor = true;
            this.books_by_author_btn.Click += new System.EventHandler(this.books_by_author_btn_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.Member);
            this.tabControl1.Location = new System.Drawing.Point(5, 339);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(308, 188);
            this.tabControl1.TabIndex = 3;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.add_author_btn);
            this.tabPage1.Controls.Add(this.author_name_txtbox);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(300, 162);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Author";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Name";
            // 
            // add_author_btn
            // 
            this.add_author_btn.Location = new System.Drawing.Point(15, 124);
            this.add_author_btn.Name = "add_author_btn";
            this.add_author_btn.Size = new System.Drawing.Size(75, 23);
            this.add_author_btn.TabIndex = 2;
            this.add_author_btn.Text = "Add";
            this.add_author_btn.UseVisualStyleBackColor = true;
            this.add_author_btn.Click += new System.EventHandler(this.add_author_btn_Click);
            // 
            // author_name_txtbox
            // 
            this.author_name_txtbox.Location = new System.Drawing.Point(15, 34);
            this.author_name_txtbox.Name = "author_name_txtbox";
            this.author_name_txtbox.Size = new System.Drawing.Size(188, 20);
            this.author_name_txtbox.TabIndex = 0;
            // 
            // Member
            // 
            this.Member.Controls.Add(this.label5);
            this.Member.Controls.Add(this.member_sso_text_box);
            this.Member.Controls.Add(this.label4);
            this.Member.Controls.Add(this.add_member_btn);
            this.Member.Controls.Add(this.member_name_text_box);
            this.Member.Location = new System.Drawing.Point(4, 22);
            this.Member.Name = "Member";
            this.Member.Padding = new System.Windows.Forms.Padding(3);
            this.Member.Size = new System.Drawing.Size(300, 162);
            this.Member.TabIndex = 1;
            this.Member.Text = "Member";
            this.Member.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 61);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "SSN";
            // 
            // member_sso_text_box
            // 
            this.member_sso_text_box.Location = new System.Drawing.Point(15, 81);
            this.member_sso_text_box.Name = "member_sso_text_box";
            this.member_sso_text_box.Size = new System.Drawing.Size(188, 20);
            this.member_sso_text_box.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 14);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Name";
            // 
            // add_member_btn
            // 
            this.add_member_btn.Location = new System.Drawing.Point(15, 124);
            this.add_member_btn.Name = "add_member_btn";
            this.add_member_btn.Size = new System.Drawing.Size(75, 23);
            this.add_member_btn.TabIndex = 5;
            this.add_member_btn.Text = "Add";
            this.add_member_btn.UseVisualStyleBackColor = true;
            this.add_member_btn.Click += new System.EventHandler(this.add_member_btn_Click);
            // 
            // member_name_text_box
            // 
            this.member_name_text_box.Location = new System.Drawing.Point(15, 34);
            this.member_name_text_box.Name = "member_name_text_box";
            this.member_name_text_box.Size = new System.Drawing.Size(188, 20);
            this.member_name_text_box.TabIndex = 4;
            // 
            // show_all_authors
            // 
            this.show_all_authors.Location = new System.Drawing.Point(240, 3);
            this.show_all_authors.Name = "show_all_authors";
            this.show_all_authors.Size = new System.Drawing.Size(69, 69);
            this.show_all_authors.TabIndex = 2;
            this.show_all_authors.Text = "Show all authors";
            this.show_all_authors.UseVisualStyleBackColor = true;
            this.show_all_authors.Click += new System.EventHandler(this.show_all_authors_Click);
            // 
            // show_all_members
            // 
            this.show_all_members.Location = new System.Drawing.Point(5, 3);
            this.show_all_members.Name = "show_all_members";
            this.show_all_members.Size = new System.Drawing.Size(69, 69);
            this.show_all_members.TabIndex = 1;
            this.show_all_members.Text = "Show all members";
            this.show_all_members.UseVisualStyleBackColor = true;
            this.show_all_members.Click += new System.EventHandler(this.show_all_members_Click);
            // 
            // resullt_lbl
            // 
            this.resullt_lbl.AutoSize = true;
            this.resullt_lbl.Location = new System.Drawing.Point(339, 12);
            this.resullt_lbl.Name = "resullt_lbl";
            this.resullt_lbl.Size = new System.Drawing.Size(74, 13);
            this.resullt_lbl.TabIndex = 1;
            this.resullt_lbl.Text = "Current Result";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(339, 381);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Details";
            // 
            // lbDetails
            // 
            this.lbDetails.FormattingEnabled = true;
            this.lbDetails.Location = new System.Drawing.Point(342, 397);
            this.lbDetails.Name = "lbDetails";
            this.lbDetails.Size = new System.Drawing.Size(300, 173);
            this.lbDetails.TabIndex = 6;
            // 
            // Error
            // 
            this.Error.AutoSize = true;
            this.Error.Location = new System.Drawing.Point(12, 425);
            this.Error.Name = "Error";
            this.Error.Size = new System.Drawing.Size(0, 13);
            this.Error.TabIndex = 7;
            // 
            // all_books_lbl
            // 
            this.all_books_lbl.AutoSize = true;
            this.all_books_lbl.Location = new System.Drawing.Point(641, 12);
            this.all_books_lbl.Name = "all_books_lbl";
            this.all_books_lbl.Size = new System.Drawing.Size(109, 13);
            this.all_books_lbl.TabIndex = 9;
            this.all_books_lbl.Text = "Complete Biblography";
            // 
            // all_books_list
            // 
            this.all_books_list.FormattingEnabled = true;
            this.all_books_list.Location = new System.Drawing.Point(648, 31);
            this.all_books_list.Name = "all_books_list";
            this.all_books_list.Size = new System.Drawing.Size(372, 537);
            this.all_books_list.TabIndex = 8;
            this.all_books_list.SelectedIndexChanged += new System.EventHandler(this.all_books_list_SelectedIndexChanged);
            // 
            // show_all_btn2
            // 
            this.show_all_btn2.Location = new System.Drawing.Point(6, 6);
            this.show_all_btn2.Name = "show_all_btn2";
            this.show_all_btn2.Size = new System.Drawing.Size(69, 69);
            this.show_all_btn2.TabIndex = 21;
            this.show_all_btn2.Text = "Refresh All Books";
            this.show_all_btn2.UseVisualStyleBackColor = true;
            this.show_all_btn2.Click += new System.EventHandler(this.RefreshBook);
            // 
            // LibraryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1032, 579);
            this.Controls.Add(this.all_books_lbl);
            this.Controls.Add(this.all_books_list);
            this.Controls.Add(this.Error);
            this.Controls.Add(this.lbDetails);
            this.Controls.Add(this.library_tab_ctrl);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.resullt_lbl);
            this.Controls.Add(this.lbResult);
            this.Name = "LibraryForm";
            this.Text = "Library";
            this.library_tab_ctrl.ResumeLayout(false);
            this.book_page_tab.ResumeLayout(false);
            this.book_page_tab.PerformLayout();
            this.Loan_page_tab.ResumeLayout(false);
            this.mem_auth_page_tab.ResumeLayout(false);
            this.mem_auth_page_tab.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.Member.ResumeLayout(false);
            this.Member.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lbResult;
        private System.Windows.Forms.Button add_book_btn;
        private System.Windows.Forms.TabControl library_tab_ctrl;
        private System.Windows.Forms.TabPage book_page_tab;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label resullt_lbl;
        private System.Windows.Forms.TabPage Loan_page_tab;
        private System.Windows.Forms.TabPage mem_auth_page_tab;
        private System.Windows.Forms.ListBox lbDetails;
        private System.Windows.Forms.Button add_copy_btn;
        private System.Windows.Forms.Label Error;
        private System.Windows.Forms.Button show_all_authors;
        private System.Windows.Forms.Button show_all_members;
        private System.Windows.Forms.TextBox author_name_txtbox;
        private System.Windows.Forms.Button add_author_btn;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TabPage Member;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox member_sso_text_box;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button add_member_btn;
        private System.Windows.Forms.TextBox member_name_text_box;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox book_description_txt_box;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox book_title_txt_box;
        private System.Windows.Forms.ComboBox author_select_combo_box;
        private System.Windows.Forms.Button show_all_available_btn;
        private System.Windows.Forms.Button show_all_books_btn;
        private System.Windows.Forms.Button show_loans_btn;
        private System.Windows.Forms.Button books_by_author_btn;
        private System.Windows.Forms.Button books_by_member_btn;
        private System.Windows.Forms.Button show_all_members_btn;
        private System.Windows.Forms.Button loan_with_member;
        private System.Windows.Forms.Label all_books_lbl;
        private System.Windows.Forms.ListBox all_books_list;
        private System.Windows.Forms.Button delete_book_btn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbl1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button show_all_btn2;
    }
}

