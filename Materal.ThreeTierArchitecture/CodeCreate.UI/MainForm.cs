using CodeCreate.Model;
using Materal.ConvertHelper;
using Materal.FileHelper;
using Materal.WindowsHelper;
using System;
using System.IO;
using System.Windows.Forms;

namespace CodeCreate.UI
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private readonly OpenFileDialog _openFileDialog = new OpenFileDialog();
        private readonly FolderBrowserDialog _folderBrowserDialog = new FolderBrowserDialog();

        private void MainForm_Load(object sender, EventArgs e)
        {
            _openFileDialog.Filter = @"*.json|*.json";
            textConfig.Text = $@"{AppDomain.CurrentDomain.BaseDirectory}Init.json";
        }

        private void BtnCreate_Click(object sender, EventArgs e)
        {
            btnCreate.Enabled = false;
            if (!File.Exists(textConfig.Text))
            {
                MessageBox.Show(@"配置文件不存在");
            }
            else
            {
                DirectoryInfo directoryInfo = !Directory.Exists(textTarget.Text) ? Directory.CreateDirectory(textTarget.Text) : new DirectoryInfo(textTarget.Text);
                if (checkClear.Checked) directoryInfo.Clear();
                if (directoryInfo.GetFiles().Length != 0 || directoryInfo.GetDirectories().Length != 0)
                {
                    MessageBox.Show(@"目标文件夹不为空");
                }
                else
                {
                    using (StreamReader stream = File.OpenText(textConfig.Text))
                    {
                        string jsonData = stream.ReadToEnd();
                        try
                        {
                            var systemModel = jsonData.JsonToObject<SubSystemModel>();
                            systemModel.CreateFile(textTarget.Text);
                            ExplorerManager.OpenExplorer(textTarget.Text);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            directoryInfo.Clear();
                        }
                    }
                }
            }
            btnCreate.Enabled = true;
        }

        private void BtnBrowseConfig_Click(object sender, EventArgs e)
        {
            if (_openFileDialog.ShowDialog() == DialogResult.OK)
            {
                textConfig.Text = _openFileDialog.FileName;
            }
        }

        private void BtnBrowseTarget_Click(object sender, EventArgs e)
        {
            if (_folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                textTarget.Text = _folderBrowserDialog.SelectedPath;
            }
        }
    }
}
