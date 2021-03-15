using Model.Dao;
using Model.Entity;
using System;
using System.Collections.Generic;

namespace Model.Neg
{
    public class FuncionarioNeg
    {
        private FuncionarioDao funcionarioDao;
        private EventoDao eventoDao;
        private ConvidadoDao convidadoDao;

        public FuncionarioNeg()
        {
            funcionarioDao = new FuncionarioDao();
            eventoDao = new EventoDao();
            convidadoDao = new ConvidadoDao();
        }

        public void create(Funcionario funcionario)
        {
            bool verification = true;

            string nome = funcionario.Nome;
            if (nome == null || nome.Equals(""))
            {
                funcionario.Execao = 2;
                return;
            }
            else
            {
                nome = funcionario.Nome.Trim();
                verification = nome.Length > 0 && nome.Length <= 50;
                if (!verification)
                {
                    funcionario.Execao = 20;
                    return;
                }
            }

            int idEve = funcionario.IdEvento;
            if (idEve == 0)
            {
                funcionario.Execao = 3;
                return;
            }

            char bebe = funcionario.Status;
            if (bebe == 0)
            {
                funcionario.Execao = 4;
                return;
            }
            else
            {
                string beber = Convert.ToString(funcionario.Status);
                verification = beber.Equals("N") || beber.Equals("S");
                if (!verification)
                {
                    funcionario.Execao = 40;
                    return;
                }
            }

            Evento evento = new Evento(funcionario.IdEvento);
            verification = eventoDao.Find(evento);
            if (!verification)
            {
                funcionario.Execao = 55;
                return;
            }

            funcionarioDao.Create(funcionario);
            funcionario.Execao = 99;

            string bebeu = Convert.ToString(funcionario.Status.ToString());

            if (bebeu.Equals("S"))
            {
                funcionarioDao.UpdateBebe(funcionario.IdEvento);
                return;
            }

            if (bebeu.Equals("N"))
                funcionarioDao.UpdateNaoBebe(funcionario.IdEvento);

            return;
        }

        public void update(Funcionario funcionario)
        {
            bool verification = true;

            string nome = funcionario.Nome;
            if (nome == null || nome.Equals(""))
            {
                funcionario.Execao = 2;
                return;
            }
            else
            {
                nome = funcionario.Nome.Trim();
                verification = nome.Length > 0 && nome.Length <= 50;
                if (!verification)
                {
                    funcionario.Execao = 20;
                    return;
                }
            }

            int idEve = funcionario.IdEvento;
            if (idEve == 0)
            {
                funcionario.Execao = 3;
                return;
            }

            char bebe = funcionario.Status;
            if (bebe == 0)
            {
                funcionario.Execao = 4;
                return;
            }
            else
            {
                string beber = Convert.ToString(funcionario.Status);
                verification = beber.Equals("N") || beber.Equals("S");
                if (!verification)
                {
                    funcionario.Execao = 40;
                    return;
                }
            }

            funcionarioDao.Update(funcionario);
            funcionario.Execao = 99;
            return;
        }

        public void delete(Funcionario funcionario)
        {
            bool verification = true;

            Funcionario funcionarioAux = new Funcionario();
            funcionarioAux.IdFuncionario = funcionario.IdFuncionario;
            verification = funcionarioDao.Find(funcionarioAux);
            if (!verification)
            {
                funcionario.execao = 33;
                return;
            }

            verification = convidadoDao.FindFun(funcionario.IdFuncionario);
            if (verification)
            {
                funcionario.Execao = 50;
                return;
            }

            string bebeu = Convert.ToString(funcionario.Status.ToString());

            if (bebeu.Equals("S"))
            {
                funcionarioDao.UpdateBebeDeletar(funcionario.IdFuncionario);
                funcionarioDao.Delete(funcionario);
                funcionario.Execao = 99;
                return;
            }
            else
            {
                funcionarioDao.UpdateNaoBebeDeletar(funcionario.IdFuncionario);
                funcionarioDao.Delete(funcionario);
                funcionario.Execao = 99;
            }

        }

        public bool find(Funcionario funcionario)
        {
            return funcionarioDao.Find(funcionario);
        }

        public List<Funcionario> findAll()
        {
            return funcionarioDao.FindAll();
        }

        public List<Funcionario> findAllFun(int codEve)
        {
            return funcionarioDao.FindAllFun(codEve);
        }
    }
}
