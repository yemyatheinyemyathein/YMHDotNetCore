using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YMHDotNetCore.WinFormsApp
{
    public partial class FormMainMenu : Form
    {
        public FormMainMenu()
        {
            InitializeComponent();
        }

        private void newBlogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 frm = new Form1();
            frm.ShowDialog();
            //frm.Show();
        }

        private void blogsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormBlogList frmBlogList = new FormBlogList();
            frmBlogList.ShowDialog();
        }
    }
}
