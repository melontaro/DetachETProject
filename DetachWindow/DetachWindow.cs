using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DetachWindow
{
    public partial class DetachWindow : Form
    {
        public DetachWindow()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var dialogResout = folderBrowserDialog.ShowDialog();
            if (dialogResout == System.Windows.Forms.DialogResult.OK)
            {
                this.textBox1.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(folderBrowserDialog.SelectedPath))
            {
                MessageBox.Show("请先选择ET目录");
                return;
            }

            Helper.Detach(folderBrowserDialog.SelectedPath);
            var dialogResult = MessageBox.Show("是否打开文件夹!", "完成!", MessageBoxButtons.OKCancel);
            if (dialogResult == DialogResult.OK)
            {
                System.Diagnostics.Process.Start("explorer.exe", folderBrowserDialog.SelectedPath + @"\");
            }
        }

        private void button2_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(btnLinkToBD, "此操作前,请先将BDFramework.Core项目复制到ET目录下!");
        }
    }
}
