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
            //this.InitializeDataGridCells();

            this.dataGridView1.CellBeginEdit += this.dataGridView1_CellBeginEdit;
            this.dataGridView1.CellEndEdit += this.dataGridView1_CellEndEdit;
        }

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            DataGridViewCell dataGridViewCell = this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
            SpreadsheetCell spreadsheetCell = this.spreadsheet.GetCell(e.RowIndex, e.ColumnIndex);

            dataGridViewCell.Value = spreadsheetCell.Text;
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewCell dataGridViewCell = this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
            SpreadsheetCell spreadsheetCell = this.spreadsheet.GetCell(e.RowIndex, e.ColumnIndex);

            spreadsheetCell.Text = dataGridViewCell.Value.ToString();
            dataGridViewCell.Value = spreadsheetCell.Value;
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

        //private void InitializeDataGridCells()
        //{
        //    for (int i = 0; i < this.dataGridView1.Rows.Count; i++)
        //    {
        //        for (int j = 0; j < this.dataGridView1.Columns.Count; j++)
        //        {
        //            DataGridViewCell dataGridViewCell = this.dataGridView1.Rows[i].Cells[j];
        //            SpreadsheetCell spreadsheetCell = this.spreadsheet.GetCell(i, j);

        //            dataGridViewCell.Value = spreadsheetCell.Value;
        //        }
        //    }
        //}

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
                DataGridViewCell dCell = this.dataGridView1.Rows[curCell.RowIndex].Cells[curCell.ColumnIndex];

                dCell.Value = curCell.Value;
            }
        }

        private void Button1_Click(object sender, EventArgs e) // Run Demo
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
