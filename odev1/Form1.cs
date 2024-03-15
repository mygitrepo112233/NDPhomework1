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

            // Mevcut fontu alarak FontDialog'a varsayılan olarak atanması
            fontDialog.Font = richTextBox1.SelectionFont;

            if (fontDialog.ShowDialog() == DialogResult.OK) {
                Font yeniFont = fontDialog.Font;

                // Değiştirilecek fontu değiştirme fonksiyonuna gönder
                ChangeFont(yeniFont);
            }
        }

        private void ChangeFont(Font font) {
            // Eğer metin seçili değilse, font değişikliği yapma
            if (richTextBox1.SelectionLength == 0)
                return;

            // Seçili metnin fontunu değiştir
            richTextBox1.SelectionFont = font;

            // Yeni yazılacak metnin fontunu da ayarla
            richTextBox1.SelectionLength = 0; // Seçili metni temizle
            richTextBox1.SelectionFont = font; // Yeni yazılacak metnin fontunu ayarla
        }

        private void dtToolStripMenuItem_Click(object sender, EventArgs e) {
            richTextBox1.Text += "\n" + System.DateTime.Now.ToString();
        }

       /*private void fontToolStripMenuItem_Click(object sender, EventArgs e) {
            FontDialog font = new FontDialog();

            if (font.ShowDialog() == DialogResult.OK) {
                ChangeFont(font);
            }
        }*/

        private void colorToolStripMenuItem_Click(object sender, EventArgs e) {
            ColorDialog font = new ColorDialog();

            if (font.ShowDialog() == DialogResult.OK) {
                richTextBox1.ForeColor = font.Color;
            }
        }



        /*private void saveToolStripMenuItem_Click(object sender, EventArgs e) {

            SaveFileDialog open = new SaveFileDialog();
            open.Title = "Save";
            open.Filter = "Text Document(*.txt|*.txt|All Files(*.*)|*.*";

            if (open.ShowDialog() == DialogResult.OK) {
                richTextBox1.SaveFile(open.FileName, RichTextBoxStreamType.PlainText);
                this.Text = open.FileName;
            }
        }*/
    }
}
