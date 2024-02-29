using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpreadsheetEngine
{
    public class SpreadsheetCell : Cell
    {
        public SpreadsheetCell(int newRows, int newColumns)
            : base(newRows, newColumns)
        {
        }
    }
}
