using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using YMHDotNetCore.RestApi.Models;

namespace YMHDotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogAdoController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetBlogs()
        {
            string query = "SELECT * from Tbl_Blog";
            SqlConnection connecton = new SqlConnection(ConnectionString.SqlConnectionStringBuilder.ConnectionString);

            connecton.Open();

            SqlCommand cmd = new SqlCommand(query, connecton);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);

            connecton.Close();

            //List<BlogModel> lst = new List<BlogModel>();
            //foreach(DataRow dr in dt.Rows)
            //{
            //    //    BlogModel blog = new BlogModel();
            //    //    blog.BlogId = Convert.ToInt32(dr["BlogId"]);
            //    //    blog.BlogTitle = Convert.ToString(dr["BlogTitle"]);
            //    //    blog.BlogAuthor = Convert.ToString(dr["BlogAuthor"]);
            //    //    blog.BlogContent = Convert.ToString(dr["BlogContent"]);
            //    //    lst.Add(blog);

            //    BlogModel blog = new BlogModel
            //    {
            //        BlogId = Convert.ToInt32(dr["BlogId"]),
            //        BlogTitle = Convert.ToString(dr["BlogTitle"]),
            //        BlogAuthor = Convert.ToString(dr["BlogAuthor"]),
            //        BlogContent = Convert.ToString(dr["BlogContent"]),
            //    };
            //    lst.Add(blog);
            //}



            List<BlogModel> lst = dt.AsEnumerable().Select(dr => new BlogModel
            {
                BlogId = Convert.ToInt32(dr["BlogId"]),
                BlogTitle = Convert.ToString(dr["BlogTitle"]),
                BlogAuthor = Convert.ToString(dr["BlogAuthor"]),
                BlogContent = Convert.ToString(dr["BlogContent"]),
            }).ToList();


            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {
            string query = "SELECT * from Tbl_Blog where BlogId = @BlogId";
            SqlConnection connecton = new SqlConnection(ConnectionString.SqlConnectionStringBuilder.ConnectionString);

            connecton.Open();

            SqlCommand cmd = new SqlCommand(query, connecton);
            cmd.Parameters.AddWithValue("@BlogId", id);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);

            connecton.Close();

            if(dt.Rows.Count == 0)
            {
                return NotFound("No Data Found");
            }

            DataRow dr = dt.Rows[0];
            var item = new BlogModel
            {
                BlogId = Convert.ToInt32(dr["BlogId"]),
                BlogTitle = Convert.ToString(dr["BlogTitle"]),
                BlogAuthor = Convert.ToString(dr["BlogAuthor"]),
                BlogContent = Convert.ToString(dr["BlogContent"]),
            };

            return Ok(item);
        }


        [HttpPost]
        public IActionResult CreateBlog(BlogModel blog)
        {

            string query = @"INSERT INTO [dbo].[tbl_Blog]
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent])
     VALUES
           (@BlogTitle
           ,@BlogAuthor
           ,@BlogContent)";
            SqlConnection connecton = new SqlConnection(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
            connecton.Open();

            SqlCommand cmd = new SqlCommand(query, connecton);
            cmd.Parameters.AddWithValue("@BlogTitle", blog.BlogTitle);
            cmd.Parameters.AddWithValue("@BlogAuthor", blog.BlogAuthor);
            cmd.Parameters.AddWithValue("@BlogContent", blog.BlogContent);
            int result = cmd.ExecuteNonQuery();

            connecton.Close();

            string message = result > 0 ? "Saving Success" : "Saving Failed";
            
            return Ok(message);
            //return StatusCode(500, message);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id, BlogModel blog)
        {
            var item = FindById(id);

            if (item == 0)
            {
                return NotFound("No Data Found.");
            }

            string query = @"UPDATE Tbl_Blog
                            SET
                                BlogTitle = @BlogTitle,
                                BlogAuthor = @BlogAuthor,
                                BlogContent = @BlogContent
                            WHERE
                                BlogId = @BlogId";

            SqlConnection connection = new SqlConnection(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
            connection.Open();

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            cmd.Parameters.AddWithValue("@BlogTitle", blog.BlogTitle);
            cmd.Parameters.AddWithValue("@BlogAuthor", blog.BlogAuthor);
            cmd.Parameters.AddWithValue("@BlogContent", blog.BlogContent);
            int result = cmd.ExecuteNonQuery();

            connection.Close();

            string message = result > 0 ? "Updating Successful." : "Updating Failed.";
            return Ok(message);
        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id, BlogModel blog)
        {
            var item = FindById(id);

            if (item == 0)
            {
                return NotFound("No Data Found.");
            }

            string conditions = string.Empty;
            var parametersList = new List<SqlParameter> { new SqlParameter("@BlogId", id) };

            if (!string.IsNullOrEmpty(blog.BlogTitle))
            {
                conditions += "BlogTitle = @BlogTitle, ";
                parametersList.Add(new SqlParameter("@BlogTitle", blog.BlogTitle));
            }

            if (!string.IsNullOrEmpty(blog.BlogAuthor))
            {
                conditions += "BlogAuthor = @BlogAuthor, ";
                parametersList.Add(new SqlParameter("@BlogAuthor", blog.BlogAuthor));
            }

            if (!string.IsNullOrEmpty(blog.BlogContent))
            {
                conditions += "BlogContent = @BlogContent, ";
                parametersList.Add(new SqlParameter("@BlogContent", blog.BlogContent));
            }

            if (conditions.Length == 0)
            {
                return NotFound("No data to update.");
            }

            conditions = conditions.Substring(0, conditions.Length - 2);

            string query = $"UPDATE Tbl_Blog SET {conditions} WHERE BlogId = @BlogId";

            SqlConnection connection = new SqlConnection(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
            connection.Open();

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddRange(parametersList.ToArray());

            int result = cmd.ExecuteNonQuery();

            connection.Close();

            string message = result > 0 ? "Updating Successful." : "Updating Failed.";
            return Ok(message);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            var item = FindById(id);

            if (item == 0)
            {
                return NotFound("No Data Found.");
            }

            string query = @"DELETE FROM 
                            Tbl_Blog
                         WHERE 
                            BlogId = @BlogId";

            SqlConnection connection = new SqlConnection(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
            connection.Open();

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            int result = cmd.ExecuteNonQuery();

            connection.Close();

            string message = result > 0 ? "Deleting Successful." : "Deleting Failed.";
            return Ok(message);
        }

        private int FindById(int id)
        {
            string query = "SELECT * FROM Tbl_Blog WHERE BlogId = @BlogId";

            SqlConnection connection = new SqlConnection(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
            connection.Open();

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);

            connection.Close();

            return dt.Rows.Count == 0 ? 0 : 1;
        }
    }


    }
