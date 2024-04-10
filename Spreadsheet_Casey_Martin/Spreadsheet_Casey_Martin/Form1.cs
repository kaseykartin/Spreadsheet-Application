// <copyright file="Form1.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace Spreadsheet_Casey_Martin
{
    using System.ComponentModel;
    using System.Windows.Forms.VisualStyles;
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
        /// Dialog box for selecting cell color.
        /// </summary>
        private ColorDialog colorDialog;

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

            this.dataGridView1.CellBeginEdit += this.DataGridView1_CellBeginEdit;
            this.dataGridView1.CellEndEdit += this.DataGridView1_CellEndEdit;

            this.colorDialog = new ColorDialog();
            this.UpdateUndoRedo();
        }

        private void DataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            DataGridViewCell dataGridViewCell = this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
            SpreadsheetCell spreadsheetCell = this.spreadsheet.GetCell(e.RowIndex, e.ColumnIndex);

            dataGridViewCell.Value = spreadsheetCell.Text;
        }

        private void DataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewCell dataGridViewCell = this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
            SpreadsheetCell spreadsheetCell = this.spreadsheet.GetCell(e.RowIndex, e.ColumnIndex);

            string newText = dataGridViewCell.Value.ToString();

            TextCommand command = new TextCommand(spreadsheetCell, newText);
            this.spreadsheet.AddUndo(command);
            this.UpdateUndoRedo();

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
            SpreadsheetCell curCell = sender as SpreadsheetCell;
            DataGridViewCell dCell = this.dataGridView1.Rows[curCell.RowIndex].Cells[curCell.ColumnIndex];

            if (e.PropertyName.Equals("Value"))
            {
                dCell.Value = curCell.Value;
            }
            else if (e.PropertyName.Equals("bgColor"))
            {
                Color newColor = Color.FromArgb((int)curCell.BGColor);
                dCell.Style.BackColor = newColor;
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

        private void ChangeBackgroundColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.colorDialog.ShowDialog() == DialogResult.OK)
            {
                uint newColor = (uint)this.colorDialog.Color.ToArgb();
                List<SpreadsheetCell> cells = new List<SpreadsheetCell>();

                foreach (DataGridViewCell dgCell in this.dataGridView1.SelectedCells)
                {
                    SpreadsheetCell ssCell = this.spreadsheet.GetCell(dgCell.RowIndex, dgCell.ColumnIndex);

                    cells.Add(ssCell);
                }

                BGColorCommand command = new BGColorCommand(cells, newColor);
                this.spreadsheet.AddUndo(command);

                this.UpdateUndoRedo();
            }
        }

        private void UndoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.spreadsheet.ExecuteUndo();
            this.UpdateUndoRedo();
        }

        private void RedoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.spreadsheet.ExecuteRedo();
            this.UpdateUndoRedo();
        }

        private void UpdateUndoRedo()
        {
            if (this.spreadsheet.UndosIsEmpty()) // Undo stack is empty, only display "Undo"
            {
                this.undoToolStripMenuItem.Enabled = false;
                this.undoToolStripMenuItem.Text = "Undo";
            }
            else // Display "Undo" + next command description
            {
                this.undoToolStripMenuItem.Enabled = true;
                this.undoToolStripMenuItem.Text = "Undo " + this.spreadsheet.GetUndoDesc();
            }

            if (this.spreadsheet.RedosIsEmpty()) // Repeat process for redo stack
            {
                this.redoToolStripMenuItem.Enabled = false;
                this.redoToolStripMenuItem.Text = "Redo";
            }
            else
            {
                this.redoToolStripMenuItem.Enabled = true;
                this.redoToolStripMenuItem.Text = "Redo " + this.spreadsheet.GetRedoDesc();
            }
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void LoadToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
