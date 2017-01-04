using System;
using System.Windows.Forms;

namespace H7Zip
{
    public partial class FormDialog : Form
    {
        public Options Options { get; set; }

        public FormDialog()
        {
            InitializeComponent();
            Options = new Options();
            comboBox1.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Options.Type == ZipType.Zip)
            {
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.DefaultExt = "7z";
                saveDialog.Filter = "7-Zip Files (*.7z)|*.7z";
                var resultSave = saveDialog.ShowDialog();
                if (resultSave == DialogResult.OK)
                {
                    Options.Input = saveDialog.FileName;
                    textBox1.Text = Options.Input;
                }
            }
            else
            {
                OpenFileDialog openDialog = new OpenFileDialog();
                openDialog.DefaultExt = "001";
                openDialog.Filter = "7-Zip Split Files (*.001)|*.001";
                var resultOpen = openDialog.ShowDialog();
                if (resultOpen == DialogResult.OK)
                {
                    Options.Input = openDialog.FileName;
                    textBox1.Text = Options.Input;
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Options.Type = (ZipType)comboBox1.SelectedIndex + 1;
            if (Options.Type == ZipType.Zip)
                numericUpDown1.Enabled = true;
            else
                numericUpDown1.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            folderDialog.Description = "Choose folder to Zip/Unzip";
            var resultFolder = folderDialog.ShowDialog();
            if (resultFolder == System.Windows.Forms.DialogResult.OK)
            {
                Options.OutputDiretory = folderDialog.SelectedPath;
                textBox2.Text = Options.OutputDiretory;
                Clipboard.SetText(Options.OutputDiretory);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Options.SplitInSize = (int)numericUpDown1.Value;
            Options.Password = TextBoxPassword.Text;
            this.Close();
        }
    }
}
