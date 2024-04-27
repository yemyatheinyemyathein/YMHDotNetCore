using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YMHDotNetCore.ConsoleApp.Dtos;
using YMHDotNetCore.ConsoleApp.Services;

namespace YMHDotNetCore.ConsoleApp.DapperExamples;

internal class DapperExample
{
    public void Run()
    {
        Read();
        Edit(1);
        Edit(13);
        Create("Test Title", "Test Author", "Test Content");
        Update(13, "Test Title 2", "Test Author 2", "Test Content 2");
        Delete(14);
    }

    private void Read()
    {

        using IDbConnection db = new SqlConnection(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
        //List<BlogDto> lst = db.Query<BlogDto>("select * from tbl_Blog").ToList();
        var lst = db.Query<BlogDto>("select * from tbl_Blog").ToList(); // var is more fast then upper line

        foreach (BlogDto item in lst)
        {
            Console.WriteLine(item.BlogId);
            Console.WriteLine(item.BlogTitle);
            Console.WriteLine(item.BlogAuthor);
            Console.WriteLine(item.BlogContent);
            Console.WriteLine("-----------------------------");
        }
    }


    private void Edit(int id)
    {
        using IDbConnection db = new SqlConnection(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
        var item = db.Query<BlogDto>("select * from tbl_Blog where blogId = @BlogId", new BlogDto { BlogId = id }).FirstOrDefault();

        if (item is null)
        {
            Console.WriteLine("No Data Found");
            return;
        }
        Console.WriteLine(item.BlogId);
        Console.WriteLine(item.BlogTitle);
        Console.WriteLine(item.BlogAuthor);
        Console.WriteLine(item.BlogContent);
        Console.WriteLine("-----------------------------");
    }
    private void Create(string title, string author, string content)
    {
        var item = new BlogDto
        {
            BlogTitle = title,
            BlogAuthor = author,
            BlogContent = content
        };

        string query = @"INSERT INTO [dbo].[tbl_Blog]
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent])
     VALUES
           (@BlogTitle
           ,@BlogAuthor
           ,@BlogContent)";

        using IDbConnection db = new SqlConnection(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
        int result = db.Execute(query, item);

        string message = result > 0 ? "Saving Successful" : "Saving Failed";
        Console.WriteLine(message);
    }

    private void Update(int id, string title, string author, string content)
    {
        var item = new BlogDto
        {
            BlogId = id,
            BlogTitle = title,
            BlogAuthor = author,
            BlogContent = content
        };

        string query = @"UPDATE [dbo].[tbl_Blog]
   SET [BlogTitle] = @BlogTitle
      ,[BlogAuthor] = @BlogAuthor
      ,[BlogContent] = @BlogContent
 WHERE BlogId = @BlogId";

        using IDbConnection db = new SqlConnection(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
        int result = db.Execute(query, item);

        string message = result > 0 ? "Updating Successful" : "Updating Failed";
        Console.WriteLine(message);
    }

    private void Delete(int id)
    {
        var item = new BlogDto
        {
            BlogId = id,
        };
        string query = @"delete from tbl_Blog where BlogId = @BlogId";

        using IDbConnection db = new SqlConnection(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
        int result = db.Execute(query, item);

        string message = result > 0 ? "Deleting Successful" : "Deleting Failed";
        Console.WriteLine(message);
    }


}
