using System.Configuration;
using System.Data.Common;


namespace PROGMGMT.Common
{
    public class DataAccess
    {
        DbProviderFactory factory;
        DbConnection conn;
        DbConnectionStringBuilder ocsb;
        public DbConnection DbConnect()
        {
            factory =
            DbProviderFactories.GetFactory("Oracle.DataAccess.Client");
            ocsb = factory.CreateConnectionStringBuilder();

            ocsb["Data Source"] = ConfigurationManager.ConnectionStrings["DataSource"].ConnectionString;
            ocsb["User ID"] = ConfigurationManager.ConnectionStrings["UserId"].ConnectionString; ;
            ocsb["Password"] = ConfigurationManager.ConnectionStrings["PassWord"].ConnectionString; ;
            conn = factory.CreateConnection();
            conn.ConnectionString = ocsb.ConnectionString;

            //データベース接続
            conn.Open();
            return conn;

        }
    }
}