using System.Data.SqlClient;

namespace OnlineRecruitmentApp.Helpers
{
    public static class DatabaseHelper
    {
        // UPDATE THIS CONNECTION STRING TO MATCH YOUR SQL SERVER
        public static string ConnectionString = @"Data Source=LAPTOP-U89LCFJ5\SQLEXPRESS;Initial Catalog=""online recruitment application"";Integrated Security=True";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(ConnectionString);
        }
    }
}
