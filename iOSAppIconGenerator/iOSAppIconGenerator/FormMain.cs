using Com.EnjoyCodes.iOSAppIconGenerator.Models;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Com.EnjoyCodes.iOSAppIconGenerator
{
    public partial class FormMain : Form
    {
        #region Member & Properties
        private string _imgPath = string.Empty;
        private string _imgFolder = string.Empty;
        #endregion

        #region Structures & Initialize Methods
        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            this.initialize();
        }
        private void initialize()
        {
            Common.Config = Config.Load(Common.ConfigPath);
            if (Common.Config == null)
                Common.Config = Common.DefaultConfig.Clone() as Config;
            Common.Config.UpdateTime = DateTime.Now;
            Common.Config.Save(Common.ConfigPath);

            this.btnExit.Location = new Point(-100, -100); // 隐藏退出按钮
        }
        #endregion

        #region Events
        private void btnOpen_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Multiselect = true;
                dialog.Title = "请选择图标文件：";
                dialog.Filter = "All Image Files|*.bmp;*.ico;*.gif;*.jpeg;*.jpg;*.png; | Windows Bitmap(*.bmp) | *.bmp | Windows Icon(*.ico) | *.ico | Graphics Interchange Format(*.gif) | (*.gif) | JPEG File Interchange Format(*.jpg) | *.jpg; *.jpeg | Portable Network Graphics(*.png) | *.png";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    this._imgPath = dialog.FileName;

                    this.txtImgPath.Text = this._imgPath;
                    this._imgFolder = Path.Combine(Path.GetDirectoryName(this._imgPath), Path.GetFileNameWithoutExtension(this._imgPath));

                    this.btnSave.Text = "保存";
                }
            }
        }

        private void txtImgPath_MouseDoubleClick(object sender, MouseEventArgs e)
        { this.btnOpen_Click(sender, e); }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (this.btnSave.Text == "保存")
            {
                if (Common.Config.Icons == null || Common.Config.Icons.Count == 0)
                    MessageBox.Show("未读取到图标配置！请配置要截取的图标。");
                else
                {
                    if (!Directory.Exists(this._imgFolder))
                        Directory.CreateDirectory(this._imgFolder);

                    Bitmap b = (Bitmap)Image.FromFile(this._imgPath);
                    foreach (var item in Common.Config.Icons)
                    {
                        Bitmap b1 = (Bitmap)b.Clone();

                        var imgFolder = Path.Combine(this._imgFolder, string.IsNullOrEmpty(item.Tag) ? string.Empty : item.Tag);

                        if (!Directory.Exists(imgFolder))
                            Directory.CreateDirectory(imgFolder);

                        b1.Resize(item.Width, item.Height).Save(Path.Combine(imgFolder, item.Name));
                        b1.Dispose();
                    }
                    b.Dispose();

                    this.btnSave.Text = "保存成功";
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(this._imgFolder) && Directory.Exists(this._imgFolder))
                    System.Diagnostics.Process.Start(this._imgFolder);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        { this.Close(); }
        #endregion
    }
}
