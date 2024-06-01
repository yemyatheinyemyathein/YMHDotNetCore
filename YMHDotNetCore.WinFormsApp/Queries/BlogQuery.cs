using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YMHDotNetCore.WinFormsApp.Queries
{
    internal class BlogQuery
    {
        public static string BlogCreate = @"INSERT INTO [dbo].[tbl_Blog]
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent])
     VALUES
           (@BlogTitle
           ,@BlogAuthor
           ,@BlogContent)";

        public static string BlogList { get; } = @"
    SELECT [BlogId]
      ,[BlogTitle]
      ,[BlogAuthor]
      ,[BlogContent]
  FROM [dbo].[tbl_Blog]";
    }
}
