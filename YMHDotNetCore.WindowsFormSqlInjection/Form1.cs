using YMHDotNetCore.Shared;

namespace YMHDotNetCore.WindowsFormSqlInjection
{
    public partial class Form1 : Form
    {
        private readonly DapperService _dapperService;
        public Form1()
        {
            InitializeComponent();
            _dapperService = new DapperService(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            //string query = $"select * from tbl_user where email = '{textEmail.Text.Trim()}' and password = '{textPassword.Text.Trim()}'"; // ဒီလိုထည့်တာက Sql Injection ပေါက်မှာပါ ' or 1=1 + ' ဒါလေးထည့်ပေးရုံနဲ့ Email pss မှန်စရာမလိုပဲ Admin Access ရသွားမှာပါ
            string query = $"select * from tbl_user where email = @Email and password = @Password"; // အဲ့ဒါကြောင့် Parameter နဲ့ထည့်ခိုင်းရတာဖြစ်တယ်

            //var model = _dapperService.QueryFirstOrDefault<UserModel>(query);
            var model = _dapperService.QueryFirstOrDefault<UserModel>(query, new
            {
                Email = textEmail.Text.Trim(),
                Password = textPassword.Text.Trim(),
            });
            if (model is null)
            {
                MessageBox.Show("User Does not Exit!");
                return;
            }
            MessageBox.Show("Is Admin : " + model.Email);
        }
    }

    public class UserModel
    {
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
    }
}
