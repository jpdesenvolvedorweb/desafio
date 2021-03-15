using System.Data.SqlClient;

namespace Model.Dao
{
    public class ConexaoDb
    {
        private static ConexaoDb conexaoDb = null;
        private SqlConnection con;

        private ConexaoDb()
        {
            con = new SqlConnection("Data Source=Zeus; Initial Catalog=DbChurrasco; Integrated Security= True");
        }

        public static ConexaoDb knowState()
        {
            if (conexaoDb == null)
            {
                conexaoDb = new ConexaoDb();
            }
            return conexaoDb;
        }

        public SqlConnection getCon()
        {
            return con;
        }

        public void CloseDb()
        {
            conexaoDb = null;
        }
    }
}
