using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Client;
using System.Data;
using System.Data.SqlClient;
using YMHDotNetCore.ConsoleApp.AdoDotNetExamples;
using YMHDotNetCore.ConsoleApp.DapperExamples;
using YMHDotNetCore.ConsoleApp.EfCoreExamples;
using YMHDotNetCore.ConsoleApp.Services;

Console.WriteLine("Hello, World!");

// nuget is For install package such as npm 

// ctrl + . = suggestion
// F10 = debug , F11 = detail debug
// F9 = Breakpoint

// SqlConnectionStringBuilder stringBuilder = new SqlConnectionStringBuilder();
// stringBuilder.DataSource = "DESKTOP-QCNJ1G5"; // server name
// stringBuilder.InitialCatalog = "DotNetTrainingBatch4"; // database name
// stringBuilder.UserID = "sa";
// stringBuilder.Password = "sasa@123";
// SqlConnection connecton = new SqlConnection(stringBuilder.ConnectionString);
// 
// connecton.Open();
// Console.WriteLine("Connection Open!");
// 
// 
// string query = "SELECT * from Tbl_Blog";
// SqlCommand cmd = new SqlCommand(query, connecton);
// SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
// DataTable dt = new DataTable();
// sqlDataAdapter.Fill(dt);
// 
// connecton.Close();
// Console.WriteLine("Connection Close!");
// 
// // dataasset => datatable
// // datatable   => dataRow
// // datarow  => dataColumn
// 
// foreach (DataRow dr in dt.Rows)
// {
//     Console.WriteLine("Blog ID => " + dr["BlogId"]);
//     Console.WriteLine("Blog Title => " + dr["BlogTItle"]);
//     Console.WriteLine("Blog Author => " + dr["BlogAuthor"]);
//     Console.WriteLine("Blog Content => " + dr["BlogContent"]);
// }


// AdoDotNet Read
// CRUD

//AdoDotNetExample adoDotNetExample = new AdoDotNetExample();
//// adoDotNetExample.Read();
//// adoDotNetExample.Create("Title", "Author", "Content");
//// adoDotNetExample.Update(11,"Title 1", "Author 1", "Content 1");
//// adoDotNetExample.Delete(11);
//// adoDotNetExample.Edit(102);
//adoDotNetExample.Edit(110);

//DapperExample dapperExamle = new DapperExample();
//dapperExamle.Run();

//EfCoreExample efCoreExample = new EfCoreExample();
//efCoreExample.Run();

// with dependecy injection
var connectionString = ConnectionString.SqlConnectionStringBuilder.ConnectionString;
var sqlConnectionStringBuilder = new SqlConnectionStringBuilder(connectionString);
var serviceProvider = new ServiceCollection()
    .AddScoped(n => new AdoDotNetExample(sqlConnectionStringBuilder))
    .AddScoped(n => new DapperExample(sqlConnectionStringBuilder))
    .AddDbContext<AppDbContext>(opt =>
    {
        opt.UseSqlServer();
    })
    .AddScoped<EfCoreExample>()
    .BuildServiceProvider();

//AppDbContext db = serviceProvider.GetRequiredService<AppDbContext>();
var adoDotNetExample = serviceProvider.GetRequiredService<AdoDotNetExample>();
adoDotNetExample.Read();

//var dapperExample = serviceProvider.GetRequiredService<DapperExample>();
//dapperExample.Run();

Console.ReadKey(); 