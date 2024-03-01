﻿// <copyright file="Cell.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SpreadsheetEngine
{
    using System.ComponentModel;

    /// <summary>
    /// Spreadsheet cell class.
    /// </summary>
    public abstract class Cell : INotifyPropertyChanged
    {
        /// <summary>
        /// Cells text.
        /// </summary>
        protected string text;

        /// <summary>
        /// Cells evaluated value.
        /// </summary>
        protected string value;

        private int rowIndex;
        private int columnIndex;

        /// <summary>
        /// Initializes a new instance of the <see cref="Cell"/> class.
        /// </summary>
        /// <param name="rowIndex">Cell row index.</param>
        /// <param name="columnIndex">Cell column index.</param>
        public Cell(int rowIndex, int columnIndex)
        {
            this.text = string.Empty;
            this.value = string.Empty;
            this.rowIndex = rowIndex;
            this.columnIndex = columnIndex;
        }

        /// <summary>
        /// PropertyChanged event.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets or sets the cells text value.
        /// </summary>
        public string Text
        {
            get
            {
                return this.text;
            }

            set
            {
                if (value == this.text)
                {
                    return;
                }
                else
                {
                    this.text = value;
                    this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Text"));
                }
            }
        }

        /// <summary>
        /// Gets or sets the cells evaluated text value.
        /// </summary>
        public string Value
        {
            get
            {
                return this.value;
            }

            protected internal set // so that only the spreadsheet class can set value
            {
                if (value == this.value)
                {
                    return;
                }
                else
                {
                    this.value = value;
                    this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Value"));
                }
            }
        }

        /// <summary>
        /// Gets the cells row index.
        /// </summary>
        public int RowIndex
        {
            get
            {
                return this.rowIndex;
            }
        }

        /// <summary>
        /// Gets the cells column index.
        /// </summary>
        public int ColumnIndex
        {
            get
            {
                return this.columnIndex;
            }
        }
    }
}