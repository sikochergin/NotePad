using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace NotePad
{
    public partial class Form1 : Form
    {
        public string Name = "";
        public bool IsChanged = false;

        public void Init()
        {
            Name = "";
            IsChanged = false;
            TitleUpdate();
        }

        public void NewFile(object sender, EventArgs e)
        {
            SaveWhenClose();
            textBox1.Text = "";
            Name = "";
            TitleUpdate();
            IsChanged = false;
        }

        public void OpenFile(object sender, EventArgs e)
        {
            SaveWhenClose();
            openFileDialog1.FileName = "";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try 
                {
                    StreamReader streamReader = new StreamReader(openFileDialog1.FileName);
                    textBox1.Text = streamReader.ReadToEnd();
                    streamReader.Close();
                    Name = openFileDialog1.FileName;
                    TitleUpdate();
                }
                catch 
                {
                    MessageBox.Show("Не удалось открыть файл");
                }
            }
        }

        public void SaveFile(string fileName)
        {
            if (fileName == "")
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    fileName = saveFileDialog1.FileName;
                }
            }
            try
            {
                StreamWriter streamWriter = new StreamWriter(fileName + ".txt");
                streamWriter.Write(textBox1.Text);
                streamWriter.Close();
                Name = fileName;
                IsChanged = false;
                TitleUpdate();
            }
            catch
            {
                MessageBox.Show("Не удалось сохранить файл");
            }
        }

        public void Save(object sender, EventArgs e)
        {
            if (IsChanged)
            {
                this.Text = this.Text.Substring(1, this.Text.Length - 1);
            }
            SaveFile(Name);
        }
        public void SaveAs(object sender, EventArgs e)
        {
            if (IsChanged)
            {
                this.Text = this.Text.Substring(1, this.Text.Length - 1);
            }
            SaveFile("");
        }

        public Form1()
        {
            InitializeComponent();
            Init();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (!IsChanged)
            {
                this.Text = "*" + this.Text;
            }
            IsChanged = true;
        }

        public void TitleUpdate()
        {
            if (Name != "") this.Text = Name;
            else this.Text = "Безымянный";
        }

        public void SaveWhenClose()
        {
            if (IsChanged)
            {
                DialogResult result = MessageBox.Show("Сохранить изменеия?", "Сохранение файла", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (result == DialogResult.Yes) { SaveFile(Name); }
            }
        }


        public void CopyT()
        {
            if (textBox1.SelectedText != null)
            {
                Clipboard.SetText(textBox1.SelectedText);
            }
        }
        public void CutT()
        {
            if (textBox1.SelectedText != null)
            {
                Clipboard.SetText(textBox1.SelectedText);
                textBox1.Text = textBox1.Text.Remove(textBox1.SelectionStart, textBox1.SelectionLength);
            }
        }
        public void PasteT()
        {

            textBox1.Text = textBox1.Text.Insert(textBox1.SelectionStart, Clipboard.GetText());

        }

        public void Copy(object sender, EventArgs e)
        {
            CopyT();
        }
        public void Cut(object sender, EventArgs e)
        {
            CutT();
        }
        public void Paste(object sender, EventArgs e)
        {
            PasteT();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            SaveWhenClose();
        }
    }
}
