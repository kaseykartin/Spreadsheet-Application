// <copyright file="Form1.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Spreadsheet_Casey_Martin
{
    /// <summary>
    /// Form1 class.
    /// </summary>
    public partial class Form1 : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Form1"/> class.
        /// </summary>
        public Form1()
        {
            this.InitializeComponent();
            this.InitializeDataGrid();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // this.InitializeDataGrid();
        }

        private void InitializeDataGrid()
        {
            this.dataGridView1.Columns.Clear();
            this.dataGridView1.Rows.Clear();

            this.AddColumns();
            this.AddRows();
        }

        private void AddColumns()
        {
            char[] alphabet = new char[26];

            for (int i = 0; i < 26; i++)
            {
                alphabet[i] = (char)('A' + i); // Populate the array with each capital letter
            }

            foreach (char letter in alphabet)
            {
                this.dataGridView1.Columns.Add(letter.ToString(), letter.ToString()); // Add a column for each letter
            }
        }

        private void AddRows()
        {
            for (int i = 0; i <= 50; i++)
            {
                this.dataGridView1.Rows.Add();
                this.dataGridView1.Rows[i].HeaderCell.Value = i.ToString();
            }
        }
    }
}
