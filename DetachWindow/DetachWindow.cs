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

        private void btnLinkToBD_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(folderBrowserDialog.SelectedPath))
            {
                MessageBox.Show("请先选择ET目录");
                return;
            }
            string UnityETPath = Path.Combine(folderBrowserDialog.SelectedPath, "Unity");
            string UnityBDPath = Path.Combine(folderBrowserDialog.SelectedPath, "BDFramework.Core");

            //copy Model Directory
            DirectoryInfo etModelPath = new DirectoryInfo(Path.Combine(UnityBDPath, @"Assets\ET\Model"));
            if (!etModelPath.Exists)
            {
                Directory.CreateDirectory(etModelPath.FullName);
            }

            if (!Helper.DirectoryCopy(Path.Combine(UnityETPath, @"Assets\Model"),
                Path.Combine(UnityBDPath, @"Assets\ET\Model")))
            {
                return;
            }

            ;  //copy Hotfix Directory
            DirectoryInfo etHotfixPath = new DirectoryInfo(Path.Combine(UnityBDPath, @"Assets\ET\@hotfix"));
            if (!etHotfixPath.Exists)
            {
                Directory.CreateDirectory(etHotfixPath.FullName);
            }

            if (!Helper.DirectoryCopy(Path.Combine(UnityETPath, @"Assets\Hotfix"),
                Path.Combine(UnityBDPath, @"Assets\ET\@hotfix")))
            {
                return;
            }


            ;  //copy Hotfix Directory
            DirectoryInfo etThirdPartyPath = new DirectoryInfo(Path.Combine(UnityBDPath, @"Assets\ET\ThirdParty"));
            if (!etThirdPartyPath.Exists)
            {
                Directory.CreateDirectory(etThirdPartyPath.FullName);
            }

            if (!Helper.DirectoryCopy(Path.Combine(UnityETPath, @"Assets\ThirdParty"),
                Path.Combine(UnityBDPath, @"Assets\ET\ThirdParty")))
            {
                return;
            }
            //plugins目录手动甄别复制吧

            Helper.Detach4BD(folderBrowserDialog.SelectedPath);
            var dialogResult = MessageBox.Show("是否打开文件夹!", "完成!", MessageBoxButtons.OKCancel);
            if (dialogResult == DialogResult.OK)
            {
                System.Diagnostics.Process.Start("explorer.exe", folderBrowserDialog.SelectedPath + @"\");
            }
        }
    }
}
