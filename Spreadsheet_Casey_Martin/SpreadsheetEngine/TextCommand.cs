// <copyright file="TextCommand.cs" company="PlaceholderCompany">
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
    public class TextCommand : Command
    {
        private SpreadsheetCell cell;

        private string oldText;

        private string newText;

        /// <summary>
        /// Initializes a new instance of the <see cref="TextCommand"/> class.
        /// </summary>
        /// <param name="cell"> Receiving cell. </param>
        /// <param name="newText"> New text. </param>
        public TextCommand(SpreadsheetCell cell, string newText)
        {
            this.cell = cell;
            this.newText = newText;
            this.oldText = cell.Text;
            this.Description = "text change";
        }

        /// <inheritdoc/>
        public override string Description { get; }

        /// <inheritdoc/>
        public override void Execute()
        {
            this.cell.Text = this.newText;
        }

        /// <inheritdoc/>
        public override void Unexecute()
        {
            this.cell.Text = this.oldText;
        }
    }
}
