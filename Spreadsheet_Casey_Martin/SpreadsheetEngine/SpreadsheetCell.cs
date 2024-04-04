// <copyright file="SpreadsheetCell.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System.ComponentModel;

namespace SpreadsheetEngine
{
    /// <summary>
    /// SpreadsheetCell class which inherits from Cell.
    /// </summary>
    public class SpreadsheetCell : Cell
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SpreadsheetCell"/> class.
        /// </summary>
        /// <param name="newRows">Row index within the spreadsheet.</param>
        /// <param name="newColumns">Column index within the spreadsheet.</param>
        public SpreadsheetCell(int newRows, int newColumns)
            : base(newRows, newColumns)
        {
        }

        public event EventHandler DependentCellValueChanged;

        public void SubToCellChange(Cell cell)
        {
            cell.PropertyChanged += this.UpdateOnDependentCellValueChanged;
        }

        private void UpdateOnDependentCellValueChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("Value"))
            {
                this.DependentCellValueChanged?.Invoke(this, new EventArgs());
            }
        }
    }
}
