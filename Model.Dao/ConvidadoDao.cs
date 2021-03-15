using Model.Entity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Model.Dao
{
    public class ConvidadoDao : IRepositorio<Convidado>
    {
        private ConexaoDb conexaoDb;
        private SqlCommand command;
        private SqlDataReader reader;

        public ConvidadoDao()
        {
            conexaoDb = ConexaoDb.knowState();
        }

        public void Create(Convidado convidado)
        {

            string script = @"INSERT INTO convidado VALUES(@NOME, @IDFUNCIONARIO, @STATUS)";

            try
            {
                command = new SqlCommand(script, conexaoDb.getCon());
                command.Parameters.AddWithValue("@NOME", convidado.Nome);
                command.Parameters.AddWithValue("@IDFUNCIONARIO", convidado.IdFuncionario);
                command.Parameters.AddWithValue("@STATUS", convidado.Status);
                conexaoDb.getCon().Open();
                command.ExecuteNonQuery();
            }
            catch (Exception erro)
            {
                throw new Exception("Mensagem de erro:", erro);
            }
            finally
            {
                conexaoDb.getCon().Close();
                conexaoDb.CloseDb();
            }
        }

        public void Delete(Convidado convidado)
        {
           
                string script = @"DELETE FROM convidado WHERE cod_conv = @IDCONVIDADO";

                try
                {
                    command = new SqlCommand(script, conexaoDb.getCon());
                    command.Parameters.AddWithValue("@IDCONVIDADO", convidado.IdConvidado);
                    conexaoDb.getCon().Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception erro)
                {
                    throw new Exception("Mensagem de erro: ", erro);
                }
                finally
                {
                    conexaoDb.getCon().Close();
                    conexaoDb.CloseDb();
                }
        }

        public bool Find(Convidado convidado)
        {
            bool register;
            string script = @"SELECT * FROM convidado WHERE cod_conv = @IDCONVIDADO";

            try
            {
                command = new SqlCommand(script, conexaoDb.getCon());
                command.Parameters.AddWithValue("@IDCONVIDADO", convidado.IdConvidado);
                conexaoDb.getCon().Open();
                reader = command.ExecuteReader();
                register = reader.Read();
                if (register)
                {
                    convidado.Nome = reader["nom_conv"].ToString();
                    convidado.IdFuncionario = Convert.ToInt16(reader["cod_func"].ToString());
                    convidado.Status = Convert.ToChar(reader["t_beber"].ToString());
                }

            }
            catch (Exception erro)
            {
                throw new Exception("Mensagem de erro: ", erro);
            }
            finally
            {
                conexaoDb.getCon().Close();
                conexaoDb.CloseDb();
            }

            return register;
        }

        public bool FindFun(int codFun)
        {
            bool register;
            string script = @"SELECT * FROM convidado WHERE cod_func = @IDFUNCIONARIO";

            try
            {
                command = new SqlCommand(script, conexaoDb.getCon());
                command.Parameters.AddWithValue("@IDFUNCIONARIO", codFun);
                conexaoDb.getCon().Open();
                reader = command.ExecuteReader();
                register = reader.Read();
            }
            catch (Exception erro)
            {
                throw new Exception("Mensagem de erro: ", erro);
            }
            finally
            {
                conexaoDb.getCon().Close();
                conexaoDb.CloseDb();
            }

            return register;
        }

        public List<Convidado> FindAll()
        {
            List<Convidado> list = new List<Convidado>();
            string script = @"SELECT * FROM convidado ORDER BY cod_conv ASC";

            try
            {
                command = new SqlCommand(script, conexaoDb.getCon());
                conexaoDb.getCon().Open();
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Convidado convidado = new Convidado();
                    convidado.IdConvidado = Convert.ToInt16(reader["cod_conv"].ToString());
                    convidado.Nome = reader["nom_conv"].ToString();
                    convidado.IdFuncionario = Convert.ToInt16(reader["cod_func"].ToString());
                    convidado.Status = Convert.ToChar(reader["t_beber"].ToString());
                    list.Add(convidado);
                }
            }
            catch (Exception erro)
            {
                throw new Exception("Mensagem de erro: ", erro);
            }
            finally
            {
                conexaoDb.getCon().Close();
                conexaoDb.CloseDb();
            }
            return list;
        }

        public List<Convidado> FindAllConv(int codEve)
        {
            List<Convidado> list = new List<Convidado>();
            string script = @"SELECT C.cod_conv AS codigo ,
                                     C.nom_conv As nome,
                                     C.cod_func As codFun,
                                     C.t_beber AS beber
                                     FROM convidado C
                              INNER JOIN funcionario F ON C.cod_func = F.cod_func
                              INNER JOIN evento E ON E.cod_eve = F.cod_eve
                              WHERE E.cod_eve = @CODEVENTO";

            try
            {
                command = new SqlCommand(script, conexaoDb.getCon());
                command.Parameters.AddWithValue("@CODEVENTO",codEve);
                conexaoDb.getCon().Open();
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Convidado convidado = new Convidado();
                    convidado.IdConvidado = Convert.ToInt16(reader["codigo"].ToString());
                    convidado.Nome = reader["nome"].ToString();
                    convidado.IdFuncionario = Convert.ToInt16(reader["codFun"].ToString());
                    convidado.Status = Convert.ToChar(reader["beber"].ToString());
                    list.Add(convidado);
                }
            }
            catch (Exception erro)
            {
                throw new Exception("Mensagem de erro: ", erro);
            }
            finally
            {
                conexaoDb.getCon().Close();
                conexaoDb.CloseDb();
            }
            
            return list;
        }

        public void Update(Convidado convidado)
        {

            string script = @"UPDATE convidado 
                                     SET nom_conv = @NOME, 
                                     t_beber = @STATUS 
                              WHERE cod_conv = @IDCONVIDADO";

            try
            {
                command = new SqlCommand(script, conexaoDb.getCon());
                command.Parameters.AddWithValue("@NOME", convidado.Nome);
                command.Parameters.AddWithValue("@STATUS", convidado.Status);
                command.Parameters.AddWithValue("@IDCONVIDADO", convidado.IdConvidado);
                conexaoDb.getCon().Open();
                command.ExecuteNonQuery();
            }
            catch (Exception erro)
            {
                throw new Exception("Mensagem de erro: ", erro);
            }
            finally
            {
                conexaoDb.getCon().Close();
                conexaoDb.CloseDb();
            }
        }

        public void UpdateBebe(int codFunc)
        {

            string script = @"UPDATE evento  
                                     SET gasto_comida += 10,
                                     gasto_bebida += 10, 
                                     gasto_total += 20, 
                                     conv_total += 1
                             FROM evento E 
                             INNER JOIN funcionario F ON F.cod_eve = E.cod_eve
                             INNER JOIN convidado C ON C.cod_func = F.cod_func
                             WHERE F.cod_func = @IDFUNCIONARIO";

            try
            {
                command = new SqlCommand(script, conexaoDb.getCon());
                command.Parameters.AddWithValue("@IDFUNCIONARIO", codFunc);
                conexaoDb.getCon().Open();
                command.ExecuteNonQuery();
            }
            catch (Exception erro)
            {
                throw new Exception("Mensagem de erro: ", erro);
            }
            finally
            {
                conexaoDb.getCon().Close();
                conexaoDb.CloseDb();
            }
        }

        public void UpdateNaoBebe(int codFunc)
        {

            string script = @"UPDATE evento  
                                     SET gasto_comida += 10,
                                     gasto_total += 10, 
                                     conv_total += 1
                             FROM evento E 
                             INNER JOIN funcionario F ON F.cod_eve = E.cod_eve
                             INNER JOIN convidado C ON C.cod_func = F.cod_func
                             WHERE F.cod_func = @IDFUNCIONARIO";

            try
            {
                command = new SqlCommand(script, conexaoDb.getCon());
                command.Parameters.AddWithValue("@IDFUNCIONARIO", codFunc);
                conexaoDb.getCon().Open();
                command.ExecuteNonQuery();
            }
            catch (Exception erro)
            {
                throw new Exception("Mensagem de erro: ", erro);
            }
            finally
            {
                conexaoDb.getCon().Close();
                conexaoDb.CloseDb();
            }
        }

        
        public void UpdateBebeDeletar(int codConv)
        {

            string script = @"UPDATE evento  
                                     SET gasto_comida -= 10,
                                     gasto_bebida -= 10, 
                                     gasto_total -= 20, 
                                     conv_total -= 1
                             FROM evento E 
                             INNER JOIN funcionario F ON F.cod_eve = E.cod_eve
                             INNER JOIN convidado C ON C.cod_func = F.cod_func
                             WHERE C.cod_conv = @IDCONVIDADO";

            try
            {
                command = new SqlCommand(script, conexaoDb.getCon());
                command.Parameters.AddWithValue("@IDCONVIDADO", codConv);
                conexaoDb.getCon().Open();
                command.ExecuteNonQuery();
            }
            catch (Exception erro)
            {
                throw new Exception("Mensagem de erro: ", erro);
            }
            finally
            {
                conexaoDb.getCon().Close();
                conexaoDb.CloseDb();
            }
        }

        public void UpdateNaoBebeDeletar(int codConv)
        {

            string script = @"UPDATE evento  
                                     SET gasto_comida -= 10,
                                     gasto_total -= 10, 
                                     conv_total -= 1
                             FROM evento E 
                             INNER JOIN funcionario F ON F.cod_eve = E.cod_eve
                             INNER JOIN convidado C ON C.cod_func = F.cod_func
                             WHERE C.cod_conv = @IDCONVIDADO";

            try
            {
                command = new SqlCommand(script, conexaoDb.getCon());
                command.Parameters.AddWithValue("@IDCONVIDADO", codConv);
                conexaoDb.getCon().Open();
                command.ExecuteNonQuery();
            }
            catch (Exception erro)
            {
                throw new Exception("Mensagem de erro: ", erro);
            }
            finally
            {
                conexaoDb.getCon().Close();
                conexaoDb.CloseDb();
            }
        }
    }
}
