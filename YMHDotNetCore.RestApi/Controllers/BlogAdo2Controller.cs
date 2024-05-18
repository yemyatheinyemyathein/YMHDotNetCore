using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using YMHDotNetCore.RestApi.Models;
using YMHDotNetCore.Shared;

namespace YMHDotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogAdo2Controller : ControllerBase
    {
        private readonly AdoDotNetService _adoDotNetService = new AdoDotNetService(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
        [HttpGet]
        public IActionResult GetBlogs()
        {
            string query = "SELECT * from Tbl_Blog";
            var lst = _adoDotNetService.Query<BlogModel>(query);
            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {
            string query = "SELECT * from Tbl_Blog where BlogId = @BlogId";
            //AdoDotNetParameter[] parameters = new AdoDotNetParameter[1];
            //parameters[0] = new AdoDotNetParameter("@BlogId", id);
            //var lst = _adoDotNetService.Query<BlogModel>(query, parameters);

            var item = _adoDotNetService.QueryFirstOrDefault<BlogModel>(query, 
                new AdoDotNetParameter("@BlogId", id)
            );


            if (item is null)
            {
                return NotFound("No Data Found");
            }
           
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
            int result = _adoDotNetService.Execute(query,
                new AdoDotNetParameter("BlogTitle", blog.BlogTitle),
                new AdoDotNetParameter("BlogAuthor", blog.BlogAuthor),
                new AdoDotNetParameter("BlogContent", blog.BlogContent)
            );

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
