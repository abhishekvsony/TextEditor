// Text Editor
// Basic text editor with cut, copy, paste, and saving functions.
// April 5, 2013
// Daniel Jason Purcell

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TextEditor
{
    public partial class frmTextEditor : Form
    {
        private String filename;
        private bool changesMade;

        public frmTextEditor()
        {
            InitializeComponent();
            filename = null;
            this.Text = "New Document";
            changesMade = false;
        }


        private void cutText()
        {
            if (txtBox.SelectedText != "")
            {
                Clipboard.SetText(txtBox.SelectedText);
                txtBox.SelectedText = "";
            }
        }

        private void copyText()
        {
            if (txtBox.SelectedText != "")
                Clipboard.SetText(txtBox.SelectedText);
        }

        private void pasteText()
        {
            if (Clipboard.GetText() != "")
                txtBox.SelectedText = Clipboard.GetText();
            
        }

        private void deleteText()
        {
            if (txtBox.SelectedText != "")
                txtBox.SelectedText = "";
            
        }

        private void selectAllText()
        {
            txtBox.SelectAll();
        }

        private void newDocument()
        {
            if (changesMade)
            {
                DialogResult result = MessageBox.Show("You have made changes to the document. \n" + "Would you like to save?",
                    "Save?", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                    saveDocument();
            }

            filename = null;
            this.Text = "New Document";
            txtBox.Clear();
            changesMade = false;

        }

        private void openDocument()
        {

            if (changesMade)
            {
                DialogResult result = MessageBox.Show("You have made changes to the document. \n" + "Would you like to save?", 
                    "Save?", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                    saveDocument();
            }

            if (dlgOpen.ShowDialog() == DialogResult.OK)
            {
                filename = dlgOpen.FileName;
                this.Text = System.IO.Path.GetFileName(filename);
                System.IO.Stream stream = dlgOpen.OpenFile();
                System.IO.StreamReader read = new System.IO.StreamReader(stream);
                txtBox.Clear();
                while (!read.EndOfStream)
                    txtBox.Text += read.ReadLine() + "\n";
                changesMade = false;

            }

        }

        private void saveDocument()
        {
            if (filename == null)
                saveAs();
            else
            {
                System.IO.File.WriteAllLines(filename, txtBox.Lines);
                changesMade = false;
            }
        }

        private void saveAs()
        {
            if (dlgSave.ShowDialog() == DialogResult.OK)
            {
                filename = dlgSave.FileName;
                this.Text = System.IO.Path.GetFileName(filename);
                System.IO.File.WriteAllLines(filename, txtBox.Lines);
                changesMade = false;
            }
            
        }

        private void exitDocument()
        {
            if (changesMade)
            {
                DialogResult result = MessageBox.Show("You have made changes to the document. \n" + "Would you like to save?",
                    "Save?", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                    saveDocument();
            }

            this.Close();
        }

        private void aboutPrompt()
        {
            MessageBox.Show("Text Editor by Daniel Jason Purcell");
        }

        private void mnuCut_Click(object sender, EventArgs e)
        {
            cutText();
        }

        private void mnuCopy_Click(object sender, EventArgs e)
        {
            copyText();
        }

        private void mnuPaste_Click(object sender, EventArgs e)
        {
            pasteText();
        }

        private void mnuDelete_Click(object sender, EventArgs e)
        {
            deleteText();
        }

        private void mnuSelectAll_Click(object sender, EventArgs e)
        {
            selectAllText();
        }

        private void cmCut_Click(object sender, EventArgs e)
        {
            cutText();
        }

        private void cmCopy_Click(object sender, EventArgs e)
        {
            copyText();
        }

        private void cmPaste_Click(object sender, EventArgs e)
        {
            pasteText();
        }

        private void cmDelete_Click(object sender, EventArgs e)
        {
            deleteText();
        }

        private void cmSelectAll_Click(object sender, EventArgs e)
        {
            selectAllText();
        }

        private void txtBox_TextChanged(object sender, EventArgs e)
        {
            changesMade = true;
        }

        private void mnuNew_Click(object sender, EventArgs e)
        {
            newDocument();
        }

        private void mnuSaveAs_Click(object sender, EventArgs e)
        {
            saveAs();
        }

        private void mnuOpen_Click(object sender, EventArgs e)
        {
            openDocument();
        }

        private void mnuSave_Click(object sender, EventArgs e)
        {
            saveDocument();
        }

        private void mnuExit_Click(object sender, EventArgs e)
        {
            exitDocument();
        }

        private void mnuAbout_Click(object sender, EventArgs e)
        {
            aboutPrompt();
        }

        private void txtBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control)
                switch (e.KeyCode)
                {
                    case Keys.N:
                        newDocument();
                        break;
                    case Keys.O:
                        openDocument();
                        break;
                    case Keys.S:
                        saveDocument();
                        break;
                    case Keys.Q:
                        exitDocument();
                        break;
                    case Keys.X:
                        cutText();
                        break;
                    case Keys.C:
                        copyText();
                        break;
                    case Keys.V:
                        pasteText();
                        break;
                    case Keys.A:
                        selectAllText();
                        break;
                }
        }

        

       
    }
}
