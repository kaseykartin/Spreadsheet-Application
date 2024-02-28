namespace Spreadsheet_Casey_Martin
{
    /// <summary>
    /// Form1 class.
    /// </summary>
    public partial class Form1 : Form
    {
        /// <summary>
        /// Initializes a new instane of Form1 class.
        /// </summary>
        public Form1()
#pragma warning restore SA1642 // Constructor summary documentation should begin with standard text
        {
            this.InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Will add code later
        }

        private void AddColumns()
        {
            char[] alphabet = new char[26];

            for (int i = 0; i < 26; i++)
            {
                alphabet[i] = (char)('A' + i); // Populate the array with each capital letter
            }

            foreach (char letter in alphabet)
            {
                this.dataGridView1.Columns.Add(letter.ToString(), letter.ToString()); // Add a column for each letter
            }
        }

        private void AddRows()
        {
            for (int i = 0; i <= 50; i++)
            {
                this.dataGridView1.Rows.Add(i.ToString(), i.ToString()); // Add 50 rows
            }
        }
    }
}
