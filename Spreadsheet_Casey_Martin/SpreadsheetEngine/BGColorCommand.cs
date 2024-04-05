// <copyright file="BGColorCommand.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SpreadsheetEngine
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Command object for changing a cells background color.
    /// </summary>
    public class BGColorCommand : Command
    {
        private List<SpreadsheetCell> cells;

        private uint color;

        private Dictionary<SpreadsheetCell, uint> usedColors;

        /// <summary>
        /// Initializes a new instance of the <see cref="BGColorCommand"/> class.
        /// </summary>
        /// <param name="cells"> Receiving cells. </param>
        /// <param name="color"> New background color. </param>
        public BGColorCommand(List<SpreadsheetCell> cells, uint color)
        {
            this.cells = cells;
            this.color = color;
            this.usedColors = this.GetUsedColors();
            this.Description = "background color change";
        }

        /// <inheritdoc/>
        public override string Description { get; }

        /// <inheritdoc/>
        public override void Execute()
        {
            foreach (SpreadsheetCell cell in this.cells)
            {
                cell.BGColor = this.color;
            }
        }

        /// <inheritdoc/>
        public override void Unexecute()
        {
            foreach (SpreadsheetCell cell in this.cells)
            {
                cell.BGColor = this.usedColors[cell];
            }
        }

        private Dictionary<SpreadsheetCell, uint> GetUsedColors()
        {
            Dictionary<SpreadsheetCell, uint> usedColors = new Dictionary<SpreadsheetCell, uint>();
            foreach (SpreadsheetCell cell in this.cells)
            {
                usedColors.Add(cell, cell.BGColor);
            }

            return usedColors;
        }
    }
}
