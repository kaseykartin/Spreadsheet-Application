// <copyright file="Form1.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace Spreadsheet_Casey_Martin
{

    using System.ComponentModel;
    using SpreadsheetEngine;

    /// <summary>
    /// Form1 class.
    /// </summary>
    public partial class Form1 : Form
    {
        /// <summary>
        /// Main spreadsheet object for form class.
        /// </summary>
        private Spreadsheet spreadsheet;

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
            this.InitializeDataGrid();
        }

        private void InitializeDataGrid()
        {
            this.dataGridView1.Columns.Clear();
            this.dataGridView1.Rows.Clear();

            this.AddColumns();
            this.AddRows();

            this.spreadsheet = new Spreadsheet(50, 26);
            this.spreadsheet.CellPropertyChanged += this.UpdateCell;
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
            for (int i = 0; i < 50; i++)
            {
                this.dataGridView1.Rows.Add();
                this.dataGridView1.Rows[i].HeaderCell.Value = (i + 1).ToString();
            }
        }

        private void UpdateCell(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("Value"))
            {
                SpreadsheetCell curCell = sender as SpreadsheetCell;
                this.dataGridView1.Rows[curCell.RowIndex].Cells[curCell.ColumnIndex].Value = curCell.Value;
            }
        }

        private void button1_Click(object sender, EventArgs e) // Run Demo
        {
            Random random = new Random();

            // Choosing 50 random cells to insert a string
            for (int i = 0; i < 50; i++)
            {
                int randRow = random.Next(0, 50);
                int randColumn = random.Next(0, 26);

                this.spreadsheet.GetCell(randRow, randColumn).Text = "Hello World!";
            }

            // Setting the text for every cell in Column B (index 1)
            for (int i = 0; i < 50; i++)
            {
                Cell curCell = this.spreadsheet.GetCell(i, 1);
                curCell.Text = "This is cell B" + (i + 1).ToString();
            }

            // Setting every cell in Column A (index 0) to its corresponding cell in Column B (index 1)
            for (int i = 0; i < 50; i++)
            {
                Cell curCell = this.spreadsheet.GetCell(i, 0);
                curCell.Text = "=B" + i.ToString();
            }
        }
    }
}
