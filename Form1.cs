using System.Drawing.Printing;
using System.Windows.Forms;

namespace Notepad
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }




        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Cut();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Copy();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
        }


        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter writer = new StreamWriter(saveFileDialog.FileName))
                {
                    writer.Write(richTextBox1.Text);
                }
            }
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (StreamReader reader = new StreamReader(openFileDialog.FileName))
                {
                    richTextBox1.Text = reader.ReadToEnd();
                }
            }
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.CanUndo)
            {
                richTextBox1.Undo();
            }
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.CanRedo)
            {
                richTextBox1.Redo();
            }
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintDocument printDocument = new PrintDocument();
            printDocument.PrintPage += (s, ev) =>
            {
                ev.Graphics.DrawString(richTextBox1.Text, new Font("Arial", 12), Brushes.Black, ev.MarginBounds);
            };

            PrintDialog printDialog = new PrintDialog();
            printDialog.Document = printDocument;

            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                printDocument.Print();
            }

        }

        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string searchText = Microsoft.VisualBasic.Interaction.InputBox("Enter text to find:", "Find");
            int index = richTextBox1.Text.IndexOf(searchText, StringComparison.OrdinalIgnoreCase);

            if (index != -1)
            {
                richTextBox1.Select(index, searchText.Length);
                richTextBox1.ScrollToCaret();
            }
            else
            {
                MessageBox.Show("Text not found!", "Find");
            }
        }

        private void replaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string searchText = Microsoft.VisualBasic.Interaction.InputBox("Enter text to replace:", "Replace");
            string replaceText = Microsoft.VisualBasic.Interaction.InputBox("Replace with:", "Replace");

            if (richTextBox1.Text.Contains(searchText))
            {
                richTextBox1.Text = richTextBox1.Text.Replace(searchText, replaceText);
            }
            else
            {
                MessageBox.Show("Text not found!", "Replace");
            }
        }

        // Shortvuts
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.N) // New
            {
                richTextBox1.Clear();
            }
            else if (e.Control && e.KeyCode == Keys.S) // Save
            {
                saveToolStripMenuItem_Click(sender, e);
            }
            else if (e.Control && e.KeyCode == Keys.O) // Open
            {
                loadToolStripMenuItem_Click(sender, e);
            }
            else if (e.Control && e.KeyCode == Keys.Z) // Undo
            {
                undoToolStripMenuItem_Click(sender, e);
            }
            else if (e.Control && e.KeyCode == Keys.Y) // Redo
            {
                redoToolStripMenuItem_Click(sender, e);
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            // Split text by spaces, newlines, and carriage returns
            int wordCount = richTextBox1.Text
                .Split(new char[] { ' ', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries)
                .Length;

            // Update the ToolStripStatusLabel
            toolStripStatusLabel1.Text = $"Words: {wordCount}";
        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void fontDialog1_Apply(object sender, EventArgs e)
        {
            using (FontDialog fontDialog = new FontDialog())
            {
                if (fontDialog.ShowDialog() == DialogResult.OK)
                {
                    richTextBox1.Font = fontDialog.Font;
                }
            }
        }
    }
}
