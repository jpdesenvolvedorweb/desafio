using Model.Entity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Model.Dao
{
    public class FuncionarioDao : IRepositorio<Funcionario>
    {
        private ConexaoDb con;
        private SqlCommand command;
        private SqlDataReader reader;

        public FuncionarioDao()
        {
            con = ConexaoDb.knowState();
        }

        public void Create(Funcionario funcionario)
        {
            string script = @"INSERT INTO funcionario VALUES(@NOME, @IDEVENTO, @STATUS)";

            try
            {
                command = new SqlCommand(script, con.getCon());
                command.Parameters.AddWithValue("@NOME", funcionario.Nome);
                command.Parameters.AddWithValue("@IDEVENTO", funcionario.IdEvento);
                command.Parameters.AddWithValue("@STATUS", funcionario.Status);
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

        public void Delete(Funcionario funcionario)
        {
            string script = @"DELETE FROM funcionario WHERE cod_func = @IDFUNCIONARIO";

            try
            {
                command = new SqlCommand(script, con.getCon());
                command.Parameters.AddWithValue("@IDFUNCIONARIO", funcionario.IdFuncionario);
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

        public bool Find(Funcionario funcionario)
        {
            bool register;
            string script = @"SELECT * FROM funcionario WHERE cod_func = @IDFUNCIONARIO";

            try
            {
                command = new SqlCommand(script, con.getCon());
                command.Parameters.AddWithValue("@IDFUNCIONARIO", funcionario.IdFuncionario);
                con.getCon().Open();
                reader = command.ExecuteReader();
                register = reader.Read();

                if (register)
                {
                    funcionario.Nome = reader["nom_func"].ToString();
                    funcionario.IdEvento = Convert.ToInt16(reader["cod_eve"].ToString());
                    funcionario.Status = Convert.ToChar(reader["t_beber"].ToString());

                }
            }
            catch (Exception erro)
            {
                throw new Exception("Mesagem de erro: ", erro);
            }
            finally
            {
                con.getCon().Close();
                con.CloseDb();
            }

            return register;
        }

        public bool FindEve(int codEve)
        {
            bool register;
            string script = @"SELECT * FROM funcionario WHERE cod_eve = @IDEVENTO";

            try
            {
                command = new SqlCommand(script, con.getCon());
                command.Parameters.AddWithValue("@IDEVENTO", codEve);
                con.getCon().Open();
                reader = command.ExecuteReader();
                register = reader.Read();
            }
            catch (Exception erro)
            {
                throw new Exception("Mesagem de erro: ", erro);
            }
            finally
            {
                con.getCon().Close();
                con.CloseDb();
            }

            return register;
        }

        public List<Funcionario> FindAll()
        {
            string script = @"SELECT * FROM funcionario ORDER BY cod_eve ASC";
            List<Funcionario> list = new List<Funcionario>();

            try
            {
                command = new SqlCommand(script, con.getCon());
                con.getCon().Open();
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Funcionario funcionario = new Funcionario();
                    funcionario.IdFuncionario = Convert.ToInt32(reader["cod_func"]);
                    funcionario.Nome = reader["nom_func"].ToString();
                    funcionario.IdEvento = Convert.ToInt32(reader["cod_eve"].ToString());
                    funcionario.Status = Convert.ToChar(reader["t_beber"].ToString());
                    list.Add(funcionario);
                }
            }
            catch (Exception erro)
            {
                throw new Exception("Mesagem de erro: ", erro);
            }
            finally
            {
                con.getCon().Close();
                con.CloseDb();
            }
            return list;
        }

        public List<Funcionario> FindAllFun(int codEve)
        {
            string script = @"SELECT F.cod_func AS cod_func,
                                     F.nom_func AS nom_func,
	                                 F.cod_eve  AS cod_eve,
	                                 F.t_beber As t_beber
	                                 From funcionario F
                             INNER JOIN evento E ON E.cod_eve = F.cod_eve
                             WHERE E.cod_eve = @IDEVENTO";

            List<Funcionario> list = new List<Funcionario>();

            try
            {
                command = new SqlCommand(script, con.getCon());
                command.Parameters.AddWithValue("@IDEVENTO", codEve);
                con.getCon().Open();
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Funcionario funcionario = new Funcionario();
                    funcionario.IdFuncionario = Convert.ToInt32(reader["cod_func"]);
                    funcionario.Nome = reader["nom_func"].ToString();
                    funcionario.IdEvento = Convert.ToInt32(reader["cod_eve"].ToString());
                    funcionario.Status = Convert.ToChar(reader["t_beber"].ToString());
                    list.Add(funcionario);
                }
            }
            catch (Exception erro)
            {
                throw new Exception("Mesagem de erro: ", erro);
            }
            finally
            {
                con.getCon().Close();
                con.CloseDb();
            }
            return list;
        }

        public void Update(Funcionario funcionario)
        {
            string script = @"UPDATE funcionario
                                     SET nom_func = @NOME, 
                                     cod_eve = @IDEVENTO, 
                                     t_beber = @STATUS 
                              WHERE cod_func = @IDFUNCIONARIO";

            try
            {
                command = new SqlCommand(script, con.getCon());
                command.Parameters.AddWithValue("@NOME", funcionario.Nome);
                command.Parameters.AddWithValue("@IDEVENTO", funcionario.IdEvento);
                command.Parameters.AddWithValue("@STATUS", funcionario.Status);
                command.Parameters.AddWithValue("@IDFUNCIONARIO", funcionario.IdFuncionario);
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

        public void UpdateBebe(int codEve)
        {
            string script = @"UPDATE evento  
                                     SET gasto_comida += 10,
                                     gasto_bebida += 10, 
                                     gasto_total += 20, 
                                     func_total += 1
                                     FROM evento E 
                             INNER JOIN funcionario F ON F.cod_eve = E.cod_eve
                             WHERE E.cod_eve = @IDEVENTO";

            try
            {
                command = new SqlCommand(script, con.getCon());
                command.Parameters.AddWithValue("@IDEVENTO", codEve);
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

        public void UpdateNaoBebe(int codEve)
        {
            string script = @"UPDATE evento  
                                     SET gasto_comida += 10,
                                     gasto_total += 10, 
                                     func_total += 1
                                     FROM evento E 
                             INNER JOIN funcionario F ON F.cod_eve = E.cod_eve
                             WHERE E.cod_eve = @IDEVENTO";

            try
            {
                command = new SqlCommand(script, con.getCon());
                command.Parameters.AddWithValue("@IDEVENTO", codEve);
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

        public void UpdateBebeDeletar(int codFunc)
        {
            string script = @"UPDATE evento  
                                     SET gasto_comida -= 10,
                                     gasto_bebida -= 10, 
                                     gasto_total -= 20, 
                                     func_total -= 1
                                     FROM evento E 
                             INNER JOIN funcionario F ON F.cod_eve = E.cod_eve
                             WHERE F.cod_func = @IDFUNCIONARIO";

            try
            {
                command = new SqlCommand(script, con.getCon());
                command.Parameters.AddWithValue("@IDFUNCIONARIO", codFunc);
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

        public void UpdateNaoBebeDeletar(int codFunc)
        {
            string script = @"UPDATE evento  
                                     SET gasto_comida -= 10,
                                     gasto_total -= 10, 
                                     func_total -= 1
                                     FROM evento E 
                             INNER JOIN funcionario F ON F.cod_eve = E.cod_eve
                             WHERE F.cod_func = @IDFUNCIONARIO";

            try
            {
                command = new SqlCommand(script, con.getCon());
                command.Parameters.AddWithValue("@IDFUNCIONARIO", codFunc);
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
