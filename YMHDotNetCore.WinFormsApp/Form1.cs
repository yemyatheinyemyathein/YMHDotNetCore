using YMHDotNetCore.Shared;
using YMHDotNetCore.WinFormsApp.Models;
using YMHDotNetCore.WinFormsApp.Queries;

namespace YMHDotNetCore.WinFormsApp
{
    public partial class Form1 : Form
    {
        private readonly DapperService _dapperService;
        public Form1()
        {
            InitializeComponent();
            _dapperService = new DapperService(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
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
    }
}
