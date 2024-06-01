using Dapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YMHDotNetCore.Shared;
using YMHDotNetCore.WinFormsApp.Models;
using YMHDotNetCore.WinFormsApp.Queries;

namespace YMHDotNetCore.WinFormsApp
{
    public partial class FormBlogList : Form
    {
        private readonly DapperService _dapperService;
        //private readonly int _edit = 1;
        //private readonly int _delete = 2;
        public FormBlogList()
        {
            InitializeComponent();
            dgvData.AutoGenerateColumns = false;
            _dapperService = new DapperService(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
        }

        private void FormBlogList_Load(object sender, EventArgs e)
        {
            BlogList();
        }

        private void BlogList()
        {
            List<BlogModel> lst = _dapperService.Query<BlogModel>(BlogQuery.BlogList);
            dgvData.DataSource = lst;
        }


        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //int ColumnIndex = e.ColumnIndex;
            //int RowIndex = e.RowIndex;

            if (e.RowIndex == -1) return;
            var blogId = Convert.ToInt32(dgvData.Rows[e.RowIndex].Cells["colId"].Value);

            #region if else case
            //if (e.ColumnIndex == (int)EnumFormControlType.Edit)
            //{
            //    Form1 frm = new Form1(blogId);
            //    frm.ShowDialog();

            //    BlogList();
            //}
            //else if (e.ColumnIndex == (int)EnumFormControlType.Delete)
            //{
            //    var dialogResult = MessageBox.Show("Are you sure U want to delete ?!", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //    if (dialogResult != DialogResult.Yes) return;
            //    DeleteBlog(blogId);
            //}
            #endregion

            #region swtich case
            int index = e.ColumnIndex;
            EnumFormControlType enumFormControlType = (EnumFormControlType)index;
            switch (enumFormControlType)
            {
                case EnumFormControlType.Edit:
                    Form1 frm = new Form1(blogId);
                    frm.ShowDialog();
                    BlogList();
                    break;
                case EnumFormControlType.Delete:
                    var dialogResult = MessageBox.Show("Are you sure U want to delete ?!", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogResult != DialogResult.Yes) return;
                    DeleteBlog(blogId);
                    break;
                case EnumFormControlType.None:
                default:
                    break;
            }
            #endregion

            //EnumFormControlType enumFormControlType = EnumFormControlType.None;
            //switch(enumFormControlType) {
            //    case EnumFormControlType.None:
            //        break;
            //    case EnumFormControlType.Edit:
            //        break;
            //    case EnumFormControlType.Delete:
            //        break;
            //    default:
            //        break;
            //}

            //string formControllerType = "None";
            //switch (formControllerType)
            //{
            //    case "ရှောက်ရိုက် အယ်ရာမတက်ဘူး ဒါပေမဲ့ enum မှာက error တယ် ဆိုတော့ enum က အဆင်ပြေမှာ":
            //        break;
            //    default:
            //        break;
            //}


        }

        private void DeleteBlog(int id)
        {
            string query = @"delete from tbl_Blog where BlogId = @BlogId";

            int result = _dapperService.Execute(query, new { BlogId = id });

            string message = result > 0 ? "Deleting Successful" : "Deleting Failed";
            Console.WriteLine(message);
            BlogList();
        }
    }
}
