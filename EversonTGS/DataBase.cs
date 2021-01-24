using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;

namespace EversonTGS
{
    class DataBase
    {
        private static SQLiteConnection conexao;

        public DataBase()
        {
        }

        public static SQLiteConnection ConectarBanco()
        {
            conexao = new SQLiteConnection("Data Source = c:\\Teste\\acme.sqlite; Version = 3");
            conexao.Open();
            return conexao;
        }

        public static void CriarBanco()
        {
            try
            {
                SQLiteConnection.CreateFile(@"c:\\Teste\\acme.sqlite");          
            }
            catch
            {
                throw;
            }
        }

        public static void CriarTabela()
        {
            try
            {
                using(var cmd = ConectarBanco().CreateCommand())
                {
                    cmd.CommandText = "CREATE TABLE IF NOT EXISTS TB_VOO" +
                        "(" +
                        "ID_VOO INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, " +
                        "DATA_VOO DATETIME NOT NULL, " +
                        "CUSTO NUMERIC(10,2) NOT NULL, " +
                        "DISTANCIA INT NOT NULL, " +
                        "CAPTURA CHAR(1) NOT NULL, " +
                        "NIVEL_DOR INT NULL" +
                        ")";
                    cmd.ExecuteNonQuery();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public static DataTable ExibirVoos()
        {
            SQLiteDataAdapter dataAdapter = null;
            DataTable dataTable = new DataTable();

            try
            {
                using(var cmd = ConectarBanco().CreateCommand())
                {
                    cmd.CommandText = "SELECT DATA_VOO AS [Data], CAPTURA AS [Captura], NIVEL_DOR AS [Nível de dor], * FROM TB_VOO";
                    dataAdapter = new SQLiteDataAdapter(cmd.CommandText, ConectarBanco());
                    dataAdapter.Fill(dataTable);
                    return dataTable;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }

        public static DataTable GetVoo(int idVoo)
        {
            SQLiteDataAdapter dataAdapter = null;
            DataTable dataTable = new DataTable();

            try
            {
                using (var cmd = ConectarBanco().CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM TB_VOO WHERE ID_VOO =" + idVoo;
                    dataAdapter = new SQLiteDataAdapter(cmd.CommandText, ConectarBanco());
                    dataAdapter.Fill(dataTable);
                    return dataTable;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public static void RegistrarVoo(Foguete foguete)
        {
            try
            {
                using (var cmd = ConectarBanco().CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO TB_VOO (ID_VOO, DATA_VOO, CUSTO, DISTANCIA, CAPTURA, NIVEL_DOR) VALUES (@id, @data, @custo, @distancia,@captura, @niveldor)";
                    cmd.Parameters.AddWithValue("@id", foguete.IdVoo);
                    cmd.Parameters.AddWithValue("@data", foguete.DataVoo);
                    cmd.Parameters.AddWithValue("@custo", foguete.Custo);
                    cmd.Parameters.AddWithValue("@distancia", foguete.Distancia);
                    cmd.Parameters.AddWithValue("@captura", foguete.Captura);
                    cmd.Parameters.AddWithValue("@niveldor", foguete.NivelDor);
                    cmd.ExecuteNonQuery();
                }

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public static void EditarVoo(Foguete foguete)
        {
            
            try
            {
                using (var cmd = ConectarBanco().CreateCommand())
                {
                    cmd.CommandText = "UPDATE TB_VOO SET DATA_VOO = @data, CUSTO = @custo, DISTANCIA = @distancia, CAPTURA = @captura, NIVEL_DOR = @niveldor WHERE ID_VOO = @id";
                    cmd.Parameters.AddWithValue("@id", foguete.IdVoo);
                    cmd.Parameters.AddWithValue("@data", foguete.DataVoo);
                    cmd.Parameters.AddWithValue("@custo", foguete.Custo);
                    cmd.Parameters.AddWithValue("@distancia", foguete.Distancia);
                    cmd.Parameters.AddWithValue("@captura", foguete.Captura);
                    cmd.Parameters.AddWithValue("@niveldor", foguete.NivelDor);
                    cmd.ExecuteNonQuery();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void ExcluirVoo(int idVoo)
        {
            try
            {
                using(var cmd = new SQLiteCommand(ConectarBanco()))
                {
                    cmd.CommandText = "DELETE FROM TB_VOO  WHERE ID_VOO = @id";
                    cmd.Parameters.AddWithValue("@id", idVoo);
                    cmd.ExecuteNonQuery();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
