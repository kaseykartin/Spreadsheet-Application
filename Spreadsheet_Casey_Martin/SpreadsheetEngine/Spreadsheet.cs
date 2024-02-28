namespace SpreadsheetEngine
{
    /// <summary>
    /// Spreadsheet class.
    /// </summary>
    public class Spreadsheet
    {
        /// <summary>
        /// 2D Array of cells.
        /// </summary>
        private Cell[,] cells;

        /// <summary>
        /// Initializes a new instance of the <see cref="Spreadsheet"/> class.
        /// </summary>
        /// <param name="numRows">Cell row index.</param>
        /// <param name="numColumns">Cell column index.</param>
        public Spreadsheet(int numRows, int numColumns)
        {
            this.cells = new Cell[numRows, numColumns];
            this.InitializeCells();
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
            return this.cells.GetLength(1);
        }
        private void InitializeCells()
        {
            for (int i = 0; i < this.cells.GetLength(0); i++) // Iterate through rows
            {
                for (int j = 0; j < this.cells.GetLength(1); j++) // Iterate through columns
                {
                    SpreadsheetCell newCell = new SpreadsheetCell(i, j); // Create new cell
                    this.cells[i, j] = newCell; // Insert the cell into the spreadsheet
                }
            }
        }

        private class SpreadsheetCell : Cell
        {
            public SpreadsheetCell(int newRows, int newColumns)
                : base(newRows, newColumns)
            {
            }
        }
    }
}
