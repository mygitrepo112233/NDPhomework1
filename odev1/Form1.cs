using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace odev1 {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {

        }

        private string filePath = ""; // File path will be stored in this variable

        private void newToolStripMenuItem_Click(object sender, EventArgs e) {
            if (TextControl()) { //if current document is not null, an pop up occurs
                DialogResult result = MessageBox.Show("Do you want to save current document?", "Warning", MessageBoxButtons.YesNoCancel);

                if (result == DialogResult.Yes) {
                    saveToolStripMenuItem_Click(sender, e); //if user clicks yes, save func opens
                }
                else if (result == DialogResult.Cancel) {
                    return; //cancels the new file creating process
                }
            }

            richTextBox1.Clear();
            filePath = ""; //because of the creating of a new file we also have to make filepath empty
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e) {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Text Files|*.txt|All Files|*.*"; //text document filter

            if (openFileDialog1.ShowDialog() == DialogResult.OK) { //opening of the document that we choose
                filePath = openFileDialog1.FileName;
                string text = File.ReadAllText(filePath);
                richTextBox1.Text = text;
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e) {
            if (filePath == "") {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "Text Files|*.txt|All Files|*.*"; //text document filter

                if (saveFileDialog1.ShowDialog() == DialogResult.OK) {
                    filePath = saveFileDialog1.FileName;
                    File.WriteAllText(filePath, richTextBox1.Text);
                }
            }
            else {
                File.WriteAllText(filePath, richTextBox1.Text);
            }
        }

        private void exitToolStripMenuItem_Click_1(object sender, EventArgs e) {
            Application.Exit();
        }

        private bool TextControl() {
            return (richTextBox1.Text != "" && (filePath == "" || richTextBox1.Text != File.ReadAllText(filePath)));
            //Control that if anything is changed or not in text box
            //Control that if text box empty or no file path assigned(means that a new text file created)
            //Control that if text box is different than the saved one
        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e) {
            FontDialog fontDialog = new FontDialog();

            //Making the new font to default
            fontDialog.Font = richTextBox1.SelectionFont;

            if (fontDialog.ShowDialog() == DialogResult.OK) {
                Font newFont = fontDialog.Font;
                ChangeFont(newFont);
            }
        }

        private void ChangeFont(Font font) {

            //Change selected texts font
            richTextBox1.SelectionFont = font;
            richTextBox1.SelectionLength = 0;
            richTextBox1.SelectionFont = font;
        }

        private void colorToolStripMenuItem_Click(object sender, EventArgs e) {
            ColorDialog color = new ColorDialog();

            //Making the new color to default
            color.Color = richTextBox1.SelectionColor;

            if (color.ShowDialog() == DialogResult.OK) {
                Color newColor = color.Color;
                ChangeColor(newColor);
            }
        }

        private void ChangeColor(Color color) {

            //Change selected texts color
            richTextBox1.SelectionColor = color;
            richTextBox1.SelectionLength = 0;
            richTextBox1.SelectionColor = color;
        }

        private void Search(string requestedText) {
            if (requestedText.Length == 0) {
                return;
            }

            int start = 0;
            int end = richTextBox1.Text.LastIndexOf(requestedText); //this variable stores text boxes total amount of character

            while (start <= end) { //this loop finds all texts that matches with requested text and changes their color to yellow
                richTextBox1.Find(requestedText, start, richTextBox1.TextLength, RichTextBoxFinds.None);
                richTextBox1.SelectionBackColor = Color.Yellow;

                start = richTextBox1.Text.IndexOf(requestedText, start) + 1;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e) {
            string requestedText = textBox1.Text;
            richTextBox1.SelectAll();
            richTextBox1.SelectionBackColor = Color.White;
            Search(requestedText);
        }
        private void copyToolStripMenuItem_Click(object sender, EventArgs e) {
            richTextBox1.Copy();

        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e) {
            richTextBox1.Cut();

        }
        private void pasteToolStripMenuItem_Click(object sender, EventArgs e) {
            richTextBox1.Paste();
        }
    }
}