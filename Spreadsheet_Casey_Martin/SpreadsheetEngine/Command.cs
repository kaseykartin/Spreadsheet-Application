// <copyright file="Command.cs" company="PlaceholderCompany">
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
    /// Command function abstract class for other command classes to inherit from.
    /// </summary>
    public abstract class Command
    {
        /// <summary>
        /// Gets description for the command function.
        /// </summary>
        public abstract string Description { get; }

        /// <summary>
        /// Executes the command function.
        /// </summary>
        public abstract void Execute();

        /// <summary>
        /// Unexecutes the command function.
        /// </summary>
        public abstract void Unexecute();
    }
}
