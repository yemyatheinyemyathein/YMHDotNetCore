using System.Data.SqlClient;
using System.Data;
using YMHDotNetCore.Shared;
using YMHDotNetCore.WinFormsApp.Models;
using YMHDotNetCore.WinFormsApp.Queries;

namespace YMHDotNetCore.WinFormsApp
{
    public partial class Form1 : Form
    {
        private readonly DapperService _dapperService;
        private readonly int _blogId;
        public Form1()
        {
            InitializeComponent();
            _dapperService = new DapperService(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
        }

        public Form1(int BlogId)
        {
            InitializeComponent();
            _blogId = BlogId;
            _dapperService = new DapperService(ConnectionString.SqlConnectionStringBuilder.ConnectionString);

            var model = _dapperService.QueryFirstOrDefault<BlogModel>("select * from tbl_Blog where blogId = @BlogId",
                new { BlogId = _blogId });
            textTitle.Text = model.BlogTitle;
            textAuthor.Text = model.BlogAuthor;
            textContent.Text = model.BlogContent;

            btnClick.Visible = false;
            btnUpdate.Visible = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                BlogModel blog = new BlogModel()
                {
                    BlogTitle = textTitle.Text.Trim(),
                    BlogAuthor = textAuthor.Text.Trim(),
                    BlogContent = textContent.Text.Trim(),
                };
                var result = _dapperService.Execute(BlogQuery.BlogCreate, blog);
                string message = result > 0 ? "Saving Successful!" : "Saving Failed!";
                var messageBoxIcon = result > 0 ? MessageBoxIcon.Information : MessageBoxIcon.Error;
                MessageBox.Show(message, "Blog", MessageBoxButtons.OK, messageBoxIcon);

                if (result > 0) ClearControls();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearControls();
        }

        private void ClearControls()
        {
            textTitle.Clear();
            textAuthor.Clear();
            textContent.Clear();

            textTitle.Focus();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                var item = new BlogModel
                {
                    BlogId = _blogId,
                    BlogTitle = textTitle.Text.Trim(),
                    BlogAuthor = textAuthor.Text.Trim(),
                    BlogContent = textContent.Text.Trim()
                };

                string query = @"UPDATE [dbo].[tbl_Blog]
   SET [BlogTitle] = @BlogTitle
      ,[BlogAuthor] = @BlogAuthor
      ,[BlogContent] = @BlogContent
 WHERE BlogId = @BlogId";

                int result = _dapperService.Execute(query, item);
                string message = result > 0 ? "Updating Successful" : "Updating Failed";
                MessageBox.Show(message);
                this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
