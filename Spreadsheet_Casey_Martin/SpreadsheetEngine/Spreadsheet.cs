﻿// <copyright file="Spreadsheet.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SpreadsheetEngine
{
    using System.ComponentModel;
    using System.Reflection.Emit;
    using System.Reflection.Metadata.Ecma335;
    using System.Xml;
    using System.Xml.Linq;

    /// <summary>
    /// Spreadsheet class.
    /// </summary>
    public class Spreadsheet
    {
        /// <summary>
        /// 2D Array of cells.
        /// </summary>
        private SpreadsheetCell[,] cells;

        private Stack<Command> undos;

        private Stack<Command> redos;

        /// <summary>
        /// Initializes a new instance of the <see cref="Spreadsheet"/> class.
        /// </summary>
        /// <param name="numRows">Cell row index.</param>
        /// <param name="numColumns">Cell column index.</param>
        public Spreadsheet(int numRows, int numColumns)
        {
            this.cells = new SpreadsheetCell[numRows, numColumns];
            this.InitializeCells();
            this.undos = new Stack<Command>();
            this.redos = new Stack<Command>();
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
            if (rowIndex > this.RowCount() - 1 || rowIndex < 0 || columnIndex > this.ColumnCount() - 1 || columnIndex < 0) // The desired cell doesnt exist
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
        /// Executes and adds a command to the undo stack, resets redo stack.
        /// </summary>
        /// <param name="undo"> Command. </param>
        public void AddUndo(Command undo)
        {
            undo.Execute();

            this.undos.Push(undo);

            while (this.redos.Count > 0)
            {
                this.redos.Pop();
            }
        }

        /// <summary>
        /// Pops and executes the next command in the undo stack, does nothing if stack is empty.
        /// </summary>
        public void ExecuteUndo()
        {
            if (this.undos.Count > 0)
            {
                Command undo = this.undos.Pop();
                undo.Unexecute();
                this.redos.Push(undo);
            }
        }

        /// <summary>
        /// Pops and executes the next command in the redo stack, does nothing if stack is empty.
        /// </summary>
        public void ExecuteRedo()
        {
            if (this.redos.Count > 0)
            {
                Command redo = this.redos.Pop();
                redo.Execute();
                this.undos.Push(redo);
            }
        }

        /// <summary>
        /// Gets the description for the next undo command.
        /// </summary>
        /// <returns> Undo command description. </returns>
        public string GetUndoDesc()
        {
            return this.undos.Peek().Description;
        }

        /// <summary>
        /// Gets the description for the next redo command.
        /// </summary>
        /// <returns> Redo descriptin. </returns>
        public string GetRedoDesc()
        {
            return this.redos.Peek().Description;
        }

        /// <summary>
        /// Checks if undo stack is empty.
        /// </summary>
        /// <returns> true or false value. </returns>
        public bool UndosIsEmpty()
        {
            return this.undos.Count <= 0;
        }

        /// <summary>
        /// Checks if redo stack is empty.
        /// </summary>
        /// <returns> true or false value.</returns>
        public bool RedosIsEmpty()
        {
            return this.redos.Count <= 0;
        }

        /// <summary>
        /// Saves the spreadsheet in XML format.
        /// </summary>
        /// <param name="dataStream"> Data stream. </param>
        public void Save(Stream dataStream)
        {
            XmlWriter xWriter = XmlWriter.Create(dataStream);
            xWriter.WriteStartDocument();
            xWriter.WriteStartElement("spreadsheet");

            foreach (SpreadsheetCell cell in this.cells)
            {
                uint bgColor = cell.BGColor;
                string text = cell.Text;

                // If the cell has any alterations
                if (bgColor != 0xFFFFFFFF || text != string.Empty)
                {
                    string name = string.Empty + (cell.ColumnIndex + 65) + (cell.RowIndex + 1); // CI + 65 results in 0->A, 1->B, and so on

                    xWriter.WriteStartElement("cell");
                    xWriter.WriteAttributeString("name", name);

                    if (bgColor != 0xFFFFFFFF) // Cell color was edited
                    {
                        xWriter.WriteStartElement("bgColor");
                        xWriter.WriteString(bgColor.ToString("X"));
                        xWriter.WriteEndElement();
                    }

                    if (text != string.Empty)
                    {
                        xWriter.WriteStartElement("text");
                        xWriter.WriteString(text);
                        xWriter.WriteEndElement();
                    }

                    xWriter.WriteEndElement();
                }
            }

            xWriter.WriteEndElement();
            xWriter.WriteEndDocument();
            xWriter.Close();
        }

        public void Load(Stream dataStream)
        {
            XDocument document = XDocument.Load(dataStream);

            if (document.Elements().ToArray()[0].Name == "spreadsheet")
            {

                // Create an array of cellements (cell elements)
                XElement[] elements = document.Elements().Elements().ToArray();

                for (int i = 0; i < elements.Length; i++)
                {
                    XElement element = elements[i];

                    if (element.Name == "cell")
                    {
                        XAttribute name = element.Attribute(XName.Get("name"));
                        SpreadsheetCell curCell = this.GetCellXML(name.Value);

                        // Loops over each cell checking text and bgcolor
                        foreach (XElement child in element.Elements())
                        {
                            if (child.Name == "text")
                            {
                                curCell.Text = child.Value;
                            }

                            else if (child.Name == "bgColor")
                            {
                                curCell.BGColor = Convert.ToUInt32(child.Value, 16);
                            }
                        }
                    }
                }
            }
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
                    newCell.DependentCellValueChanged += this.UpdateOnDependentCellValueChanged;
                }
            }
        }

        private void UpdateOnCellChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("Text"))
            {
                this.UpdateCellValue(sender as SpreadsheetCell);
            }

            if (e.PropertyName.Equals("bgColor"))
            {
                this.CellPropertyChanged?.Invoke(sender as SpreadsheetCell, new PropertyChangedEventArgs("bgColor"));
            }
        }

        private void UpdateOnDependentCellValueChanged(object sender, EventArgs e)
        {
            this.UpdateCellValue(sender as SpreadsheetCell);
        }

        private void UpdateCellValue(SpreadsheetCell cell)
        {
            string newValue = cell.Text;

            if (newValue.StartsWith("=")) // newValue is an expression
            {
                newValue = this.EvaluateCell(cell).ToString();
            }

            cell.SetValue(newValue);

            this.CellPropertyChanged?.Invoke(cell, new PropertyChangedEventArgs("Value"));
        }

        private double EvaluateCell(SpreadsheetCell cell)
        {
            string newText = cell.Text.Substring(1).Replace(" ", string.Empty); // Remove the "=" for the expression to be inserted into treeeeeeeee

            ExpressionTree newTree = new ExpressionTree(newText);

            this.CheckVariableCells(newTree, cell);

            return newTree.Evaluate();
        }

        private void CheckVariableCells(ExpressionTree newTree, SpreadsheetCell newCell)
        {
            List<string> newVariables = newTree.GetVariables();
            foreach (string variable in newVariables)
            {
                SpreadsheetCell varCell = this.GetVariableCell(variable);
                double value = 0.0;
                try
                {
                    value = double.Parse(varCell.Value);
                }
                catch (FormatException)
                {
                }

                newTree.SetVariable(variable, value);

                newCell.SubToCellChange(varCell);
            }
        }

        private SpreadsheetCell GetVariableCell(string variable)
        {
            int rowIndex = -1, columnIndex = -1;

            char[] alphabet =
            {
                'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M',
                'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
            };

            try
            {
                char column = variable.ToCharArray()[0];
                rowIndex = int.Parse(variable.Split(column)[1]);
                columnIndex = Array.IndexOf(alphabet, column);
            }
            catch (FormatException)
            {
                throw new InvalidCellException("Invalid Cell");
            }

            return this.GetCell(rowIndex, columnIndex);
        }

        private SpreadsheetCell GetCellXML(string variable)
        {
            int rowIndex = -1, columnIndex = -1;

            char[] alphabet =
            {
                'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M',
                'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
            };

            try
            {
                char column = variable.ToCharArray()[0];
                rowIndex = int.Parse(variable.Split(column)[1]) - 1;
                columnIndex = Array.IndexOf(alphabet, column);
            }
            catch (FormatException)
            {
                throw new InvalidCellException("Invalid Cell");
            }

            return this.GetCell(rowIndex, columnIndex);
        }
    }
}