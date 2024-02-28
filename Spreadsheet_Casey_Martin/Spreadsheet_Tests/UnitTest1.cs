namespace SpreadsheetEngine
{
    public class Tests
    {
        /// <summary>
        /// Normal case for Spreadsheet ColumnCount method.
        /// </summary>
        [Test]
        public void TestColumnCountNormal()
        {
            Spreadsheet s = new Spreadsheet(6, 5);
            Assert.That(s.ColumnCount(), Is.EqualTo(5), "Spreadsheet returned unexpected column count.");
        }

        /// <summary>
        /// Zero case for Spreadsheet ColumnCount method.
        /// </summary>
        [Test]
        public void TestColumnCountZero()
        {
            Spreadsheet s = new Spreadsheet(6, 0);
            Assert.That(s.ColumnCount(), Is.EqualTo(0), "Spreadsheet returned unexpected column count.");
        }

        /// <summary>
        /// Normal case for Spreadsheet RowCount method.
        /// </summary>
        [Test]
        public void TestRowCountNormal()
        {
            Spreadsheet s = new Spreadsheet(6, 5);
            Assert.That(s.RowCount(), Is.EqualTo(6), "Spreadsheet returned unexpected column count.");
        }

        /// <summary>
        /// Zero case for Spreadsheet RowCount method.
        /// </summary>
        [Test]
        public void TestRowCountZero()
        {
            Spreadsheet s = new Spreadsheet(0, 5);
            Assert.That(s.RowCount(), Is.EqualTo(0), "Spreadsheet returned unexpected column count.");
        }
    }
}