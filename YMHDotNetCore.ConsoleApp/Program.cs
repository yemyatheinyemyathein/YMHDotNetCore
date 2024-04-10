﻿using System.Data;
using System.Data.SqlClient;

Console.WriteLine("Hello, World!");

// nuget is For install package such as npm 

// ctrl + . = suggestion
// F10 = debug , F11 = detail debug
// F9 = Breakpoint

SqlConnectionStringBuilder stringBuilder = new SqlConnectionStringBuilder();
stringBuilder.DataSource = "DESKTOP-QCNJ1G5"; // server name
stringBuilder.InitialCatalog = "DotNetTrainingBatch4"; // database name
stringBuilder.UserID = "sa";
stringBuilder.Password = "sasa@123";
SqlConnection connecton = new SqlConnection(stringBuilder.ConnectionString);

connecton.Open();
Console.WriteLine("Connection Open!");


string query = "SELECT * from Tbl_Blog";
SqlCommand cmd = new SqlCommand(query, connecton);
SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
DataTable dt = new DataTable();
sqlDataAdapter.Fill(dt);

connecton.Close();
Console.WriteLine("Connection Close!");

// dataasset => datatable
// datatable   => dataRow
// datarow  => dataColumn

foreach (DataRow dr in dt.Rows)
{
    Console.WriteLine("Blog ID => " + dr["BlogId"]);
    Console.WriteLine("Blog Title => " + dr["BlogTItle"]);
    Console.WriteLine("Blog Author => " + dr["BlogAuthor"]);
    Console.WriteLine("Blog Content => " + dr["BlogContent"]);
}

Console.ReadKey();