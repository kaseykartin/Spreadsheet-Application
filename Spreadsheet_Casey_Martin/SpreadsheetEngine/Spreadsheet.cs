// <copyright file="Spreadsheet.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SpreadsheetEngine
{
    using System.ComponentModel;

    /// <summary>
    /// Spreadsheet class.
    /// </summary>
    public class Spreadsheet
    {
        /// <summary>
        /// 2D Array of cells.
        /// </summary>
        private SpreadsheetCell[,] cells;

        /// <summary>
        /// Initializes a new instance of the <see cref="Spreadsheet"/> class.
        /// </summary>
        /// <param name="numRows">Cell row index.</param>
        /// <param name="numColumns">Cell column index.</param>
        public Spreadsheet(int numRows, int numColumns)
        {
            this.cells = new SpreadsheetCell[numRows, numColumns];
            this.InitializeCells();
        }

        /// <summary>
        /// CellPropertyChanged event.
        /// </summary>
        public event PropertyChangedEventHandler CellPropertyChanged;

        /// <summary>
        /// Returns a cell at the given row and column index within the Spreadsheet.
        /// </summary>
        /// <param name="rowIndex">Cell row index.</param>
        /// <param name="columnIndex">Cell column index.</param>
        /// /// <returns>The cell at the specified index.</returns>
        public SpreadsheetCell GetCell(int rowIndex, int columnIndex)
        {
            if (rowIndex > this.RowCount() || rowIndex < 0 || columnIndex > this.ColumnCount() || columnIndex < 0) // The desired cell doesnt exist
            {
                return null;
            }

            return this.cells[rowIndex, columnIndex];
        }

        /// <summary>
        /// Returns the number of columns in the spreadsheet.
        /// </summary>
        /// <returns>Number of columns in the spreadsheet.</returns>
        public int ColumnCount()
        {
            return this.cells.GetLength(1);
        }

        /// <summary>
        /// Returns the number of rows in the spreadsheet.
        /// </summary>
        /// <returns>Number of rows in the spreadsheet.</returns>
        public int RowCount()
        {
            return this.cells.GetLength(0);
        }

        /// <summary>
        /// Initializes the array of cells within the Spreadsheet.
        /// </summary>
        private void InitializeCells()
        {
            for (int i = 0; i < this.cells.GetLength(0); i++) // Iterate through rows
            {
                for (int j = 0; j < this.cells.GetLength(1); j++) // Iterate through columns
                {
                    SpreadsheetCell newCell = new SpreadsheetCell(i, j); // Create new cell
                    this.cells[i, j] = newCell; // Insert the cell into the spreadsheet
                    newCell.PropertyChanged += this.UpdateOnCellChanged;
                }
            }
        }

        private void UpdateOnCellChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("Text"))
            {
                SpreadsheetCell curCell = sender as SpreadsheetCell;
                string newText = curCell.Text;

                if (!newText.StartsWith("="))
                {
                    curCell.Value = newText;
                }
                else // "=B4" or "=<ColumnIndex><RowIndex>"
                {
                    char tempColumn = curCell.Text[1];
                    int tempColumnIndex = tempColumn - 65; // https://stackoverflow.com/questions/1951517/convert-a-to-1-b-to-2-z-to-26-and-then-aa-to-27-ab-to-28-column-indexes-to

                    string tempRow = curCell.Text.Substring(2);
                    int tempRowIndex = int.Parse(tempRow);

                    // Retrieve value from the cell at the specified index
                    string newValue = this.cells[tempRowIndex, tempColumnIndex].Value;
                    curCell.Value = newValue;
                }

                // Notify subscribers that the cell value has changed
                this.CellPropertyChanged?.Invoke(sender, new PropertyChangedEventArgs("Value"));
            }
        }
    }
}
