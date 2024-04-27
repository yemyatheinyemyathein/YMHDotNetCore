using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Metadata;
using System.ComponentModel.DataAnnotations;

namespace YMHDotNetCore.ConsoleApp.AdoDotNetExamples
{
    internal class AdoDotNetExample
    {
        private readonly SqlConnectionStringBuilder _sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
        {
            DataSource = "DESKTOP-QCNJ1G5", // server name
            InitialCatalog = "DotNetTrainingBatch4", // database name
            UserID = "sa",
            Password = "sasa@123"
        };
        public void Read()
        {
            // SqlConnectionStringBuilder stringBuilder = new SqlConnectionStringBuilder();
            // stringBuilder.DataSource = "DESKTOP-QCNJ1G5"; // server name
            // stringBuilder.InitialCatalog = "DotNetTrainingBatch4"; // database name
            // stringBuilder.UserID = "sa";
            // stringBuilder.Password = "sasa@123";
            SqlConnection connecton = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);

            connecton.Open();
            Console.WriteLine("Connection Open!");


            string query = "SELECT * from Tbl_Blog";
            SqlCommand cmd = new SqlCommand(query, connecton);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);

            connecton.Close();
            Console.WriteLine("Connection Close!");

            foreach (DataRow dr in dt.Rows)
            {
                Console.WriteLine("Blog ID => " + dr["BlogId"]);
                Console.WriteLine("Blog Title => " + dr["BlogTItle"]);
                Console.WriteLine("Blog Author => " + dr["BlogAuthor"]);
                Console.WriteLine("Blog Content => " + dr["BlogContent"]);
            }
        }

        public void Create(string title, string author, string content)
        {
            SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            connection.Open();

            string query = @"INSERT INTO [dbo].[tbl_Blog]
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent])
     VALUES
           (@BlogTitle
           ,@BlogAuthor
           ,@BlogContent)";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogTitle", title);
            cmd.Parameters.AddWithValue("@BlogAuthor", author);
            cmd.Parameters.AddWithValue("@BlogContent", content);
            int result = cmd.ExecuteNonQuery();

            connection.Close();

            string message = result > 0 ? "Saving Success" : "Saving Failed";
            Console.WriteLine(message);
        }


        public void Update(int id, string title, string author, string content)
        {
            SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            connection.Open();

            string query = @"UPDATE [dbo].[tbl_Blog]
   SET [BlogTitle] = @BlogTitle
      ,[BlogAuthor] = @BlogAuthor
      ,[BlogContent] = @BlogContent
 WHERE BlogId = @BlogId";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            cmd.Parameters.AddWithValue("@BlogTitle", title);
            cmd.Parameters.AddWithValue("@BlogAuthor", author);
            cmd.Parameters.AddWithValue("@BlogContent", content);
            int result = cmd.ExecuteNonQuery();

            connection.Close();

            string message = result > 0 ? "Updaing Success" : "Updaing Failed";
            Console.WriteLine(message);
        }

        public void Delete(int id)
        {
            SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            connection.Open();

            string query = @"delete from tbl_Blog where BlogId = @BlogId";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            int result = cmd.ExecuteNonQuery();

            connection.Close();

            string message = result > 0 ? "Deleted Success" : "Deleted Failed";
            Console.WriteLine(message);
        }


        public void Edit(int id)
        {
            SqlConnection connecton = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);

            connecton.Open();
            Console.WriteLine("Connection Open!");


            string query = "SELECT * from Tbl_Blog where BlogId = @BlogId";
            SqlCommand cmd = new SqlCommand(query, connecton);
            cmd.Parameters.AddWithValue("@BlogId", id);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);

            connecton.Close();

            if (dt.Rows.Count == 0)
            {
                Console.WriteLine("No Data Found");
                return;
            }

            DataRow dr = dt.Rows[0];

            Console.WriteLine("Blog ID => " + dr["BlogId"]);
            Console.WriteLine("Blog Title => " + dr["BlogTItle"]);
            Console.WriteLine("Blog Author => " + dr["BlogAuthor"]);
            Console.WriteLine("Blog Content => " + dr["BlogContent"]);
        }
    }
}
