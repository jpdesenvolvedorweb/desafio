using Model.Entity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Model.Dao
{
    public class EventoDao : IRepositorio<Evento>
    {
        private ConexaoDb con;
        private SqlCommand command;
        private SqlDataReader reader;

        public EventoDao()
        {
            con = ConexaoDb.knowState();
        }

        public void Create(Evento evento)
        {
            string script = @"INSERT INTO evento VALUES(@NOME, 0, 0, 0, 0, 0, @DATA)";

            try
            {
                command = new SqlCommand(script, con.getCon());
                command.Parameters.AddWithValue("@NOME", evento.Nome);
                command.Parameters.AddWithValue("@DATA", evento.DataEvento);
                con.getCon().Open();
                command.ExecuteNonQuery();

            }
            catch (Exception erro)
            {
                throw new Exception("MEnsagem de erro: ", erro);
            }
            finally
            {
                con.getCon().Close();
                con.CloseDb();
            }
        }

        public void Delete(Evento evento)
        {
                string script = @"DELETE FROM evento WHERE cod_eve = @IDEVENTO";

                try
                {
                    command = new SqlCommand(script, con.getCon());
                    command.Parameters.AddWithValue("@IDEVENTO", evento.IdEvento);
                    con.getCon().Open();
                    command.ExecuteNonQuery();

                }
                catch (Exception erro)
                {
                    throw new Exception("Mensagem de erro: ", erro);
                }
                finally
                {
                    con.getCon().Close();
                    con.CloseDb();
                }
        }

        public bool Find(Evento evento)
        {

            bool register;

            string script = @"SELECT * FROM evento(NOLOCK) WHERE cod_eve = @IDEVENTO";

            try
            {
                command = new SqlCommand(script, con.getCon());
                command.Parameters.AddWithValue("@IDEVENTO", evento.IdEvento);
                con.getCon().Open();
                reader = command.ExecuteReader();
                register = reader.Read();
                if (register)
                {
                    evento.Nome = reader["nom_eve"].ToString();
                    evento.GastoComida = Convert.ToInt32(reader["gasto_comida"].ToString());
                    evento.GastoBebida = Convert.ToInt32(reader["gasto_bebida"].ToString());
                    evento.GastoTotal = Convert.ToInt32(reader["gasto_total"].ToString());
                    evento.TotalFuncionarios = Convert.ToInt32(reader["func_total"].ToString());
                    evento.TotalConvidados = Convert.ToInt32(reader["conv_total"].ToString());
                    evento.DataEvento = reader["data_eve"].ToString();
                }
            }
            catch (Exception erro)
            {
                throw new Exception("Mensagem de erro: ", erro);
            }
            finally
            {
                con.getCon().Close();
                con.CloseDb();
            }

            return register;
        }

        public List<Evento> FindAll()
        {
            string script = "SELECT * FROM evento(NOLOCK) ORDER BY cod_eve ASC";
            List<Evento> list = new List<Evento>();

            try
            {
                command = new SqlCommand(script, con.getCon());
                con.getCon().Open();
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Evento evento = new Evento();
                    evento.IdEvento = Convert.ToInt32(reader["cod_eve"].ToString());
                    evento.Nome = reader["nom_eve"].ToString();
                    evento.GastoComida = Convert.ToInt32(reader["gasto_comida"].ToString());
                    evento.GastoBebida = Convert.ToInt32(reader["gasto_bebida"].ToString());
                    evento.GastoTotal = Convert.ToInt32(reader["gasto_total"].ToString());
                    evento.TotalFuncionarios = Convert.ToInt32(reader["func_total"].ToString());
                    evento.TotalConvidados = Convert.ToInt32(reader["conv_total"].ToString());
                    evento.DataEvento = reader["data_eve"].ToString();

                    list.Add(evento);
                }
            }
            catch (Exception erro)
            {
                throw new Exception("Mensagem de erro: ", erro);
            }
            finally
            {
                con.getCon().Close();
                con.CloseDb();
            }
            return list;
        }

        public void Update(Evento evento)
        {
            string script = @"UPDATE evento SET nom_eve = @NOME, data_eve= @DATA WHERE cod_eve = @IDEVENTO";

            try
            {
                command = new SqlCommand(script, con.getCon());
                command.Parameters.AddWithValue("@NOME", evento.Nome);
                command.Parameters.AddWithValue("@DATA", evento.DataEvento);
                command.Parameters.AddWithValue("@IDEVENTO", evento.IdEvento);
                con.getCon().Open();
                command.ExecuteNonQuery();
            }
            catch (Exception erro)
            {
                throw new Exception("Mensagem de erro: ", erro);
            }
            finally
            {
                con.getCon().Close();
                con.CloseDb();
            }
        }
    }
}
