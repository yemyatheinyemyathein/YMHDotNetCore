using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YMHDotNetCore.ConsoleApp
{
    internal static class ConnectionString
    {
        public static SqlConnectionStringBuilder SqlConnectionStringBuilder = new SqlConnectionStringBuilder()
        {
            DataSource = "DESKTOP-QCNJ1G5", // server name
            InitialCatalog = "DotNetTrainingBatch4", // database name
            UserID = "sa",
            Password = "sasa@123"
        };
    }
}
