using System.Data.SqlClient;

namespace YMHDotNetCore.NLayer.DataAccess;
internal static class ConnectionString
{
    public static SqlConnectionStringBuilder SqlConnectionStringBuilder = new SqlConnectionStringBuilder()
    {
        DataSource = "DESKTOP-QCNJ1G5", // server name
        InitialCatalog = "DotNetTrainingBatch4", // database name
        UserID = "sa",
        Password = "sasa@123",
        TrustServerCertificate = true
    };
}
