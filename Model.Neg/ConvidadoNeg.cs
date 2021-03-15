using Model.Dao;
using Model.Entity;
using System;
using System.Collections.Generic;

namespace Model.Neg
{
    public class ConvidadoNeg
    {
        private ConvidadoDao convidadoDao;
        private FuncionarioDao funcionarioDao;

        public ConvidadoNeg()
        {
            convidadoDao = new ConvidadoDao();
            funcionarioDao = new FuncionarioDao();
        }

        public void create(Convidado convidado)
        {
            bool verification = true;

            string nome = convidado.Nome;
            if (nome == null || nome.Equals(""))
            {
                convidado.Execao = 2;
                return;
            }
            else
            {
                nome = convidado.Nome.Trim();
                verification = nome.Length > 0 && nome.Length <= 50;
                if (!verification)
                {
                    convidado.Execao = 20;
                    return;
                }
            }

            int idFunc = convidado.IdFuncionario;
            if (idFunc == 0)
            {
                convidado.Execao = 3;
                return;
            }

            char bebe = convidado.Status;
            if (bebe == 0)
            {
                convidado.Execao = 4;
                return;
            }
            else
            {
                string beber = Convert.ToString(convidado.Status);
                verification = beber.Equals("N") || beber.Equals("S");
                if (!verification)
                {
                    convidado.Execao = 40;
                    return;
                }
            }

            Funcionario funcionario = new Funcionario(convidado.IdFuncionario);
            verification = funcionarioDao.Find(funcionario);
            if (!verification)
            {
                convidado.Execao = 55;
                return;
            }

            verification = convidadoDao.FindFun(convidado.IdFuncionario);
            if (verification)
            {
                convidado.Execao = 60;
                return;
            }


            convidadoDao.Create(convidado);
            convidado.Execao = 99;

            string bebeu = Convert.ToString(convidado.Status.ToString());

            if (bebeu.Equals("S"))
            {
                convidadoDao.UpdateBebe(convidado.IdFuncionario);
                return;
            }

            if (bebeu.Equals("N"))
                convidadoDao.UpdateNaoBebe(convidado.IdFuncionario);

            return;
        }

        public void update(Convidado convidado)
        {
            Convidado objCategoryAux = new Convidado();
            bool verification = true;


            string nome = convidado.Nome;
            if (nome == null || nome.Equals(""))
            {
                convidado.Execao = 2;
                return;
            }
            else
            {
                nome = convidado.Nome.Trim();
                verification = nome.Length > 0 && nome.Length <= 50;
                if (!verification)
                {
                    convidado.Execao = 20;
                    return;
                }
            }

            int idFunc = convidado.IdFuncionario;
            if (idFunc == 0)
            {
                convidado.Execao = 3;
                return;
            }

            char bebe = convidado.Status;
            if (bebe == 0)
            {
                convidado.Execao = 4;
                return;
            }
            else
            {
                string beber = Convert.ToString(convidado.Status);
                verification = beber.Equals("N") || beber.Equals("S");
                if (!verification)
                {
                    convidado.Execao = 40;
                    return;
                }
            }

            convidadoDao.Update(convidado);
            convidado.Execao = 99;
            return;
        }

        public void delete(Convidado convidado)
        {
            bool verification = true;

            Convidado convidadoAux = new Convidado();
            convidadoAux.IdConvidado = convidado.IdConvidado;
            verification = convidadoDao.Find(convidadoAux);
            if (!verification)
            {
                convidado.execao = 33;
                return;
            }

            string bebeu = Convert.ToString(convidado.Status.ToString());

            if (bebeu.Equals("S"))
            {
                convidadoDao.UpdateBebeDeletar(convidado.IdConvidado);
                convidadoDao.Delete(convidado);
                convidado.execao = 99;
                return;
            }
            else
            {
                convidadoDao.UpdateNaoBebeDeletar(convidado.IdConvidado);
                convidadoDao.Delete(convidado);
                convidado.execao = 99;
            }

        }

        public bool find(Convidado convidado)
        {
            return convidadoDao.Find(convidado);
        }

        public List<Convidado> findAll()
        {
            return convidadoDao.FindAll();
        }

        public List<Convidado> findAllConv(int codEve)
        {
            return convidadoDao.FindAllConv(codEve);
        }
    }
}
