using Model.Dao;
using Model.Entity;
using System.Collections.Generic;

namespace Model.Neg
{
    public class EventoNeg
    {
        private EventoDao eventoDao;
        private FuncionarioDao funcionarioDao;

        public EventoNeg()
        {
            eventoDao = new EventoDao();
            funcionarioDao = new FuncionarioDao();
        }

        public void create(Evento evento)
        {
            bool verification = true;

            string nome = evento.Nome;
            if (nome == null || nome.Equals(""))
            {
                evento.Execao = 2;
                return;
            }
            else
            {
                nome = evento.Nome.Trim();
                verification = nome.Length > 0 && nome.Length <= 40;
                if (!verification)
                {
                    evento.Execao = 20;
                    return;
                }
            }

            string dataEvento = evento.DataEvento;
            if (dataEvento == null || dataEvento.Equals(""))
            {
                evento.Execao = 3;
                return;
            }
            else
            {
                dataEvento = evento.DataEvento.Trim();
                verification = dataEvento.Length == 10;
                if (!verification)
                {
                    evento.Execao = 30;
                    return;
                }
            }

            eventoDao.Create(evento);
            evento.Execao = 99;
            return;
        }

        public void update(Evento evento)
        {
            bool verification = true;

            string nome = evento.Nome;
            if (nome == null || nome.Equals(""))
            {
                evento.Execao = 2;
                return;
            }
            else
            {
                nome = evento.Nome.Trim();
                verification = nome.Length > 0 && nome.Length <= 40;
                if (!verification)
                {
                    evento.Execao = 20;
                    return;
                }
            }

            string dataEvento = evento.DataEvento;
            if (dataEvento == null || dataEvento.Equals(""))
            {
                evento.Execao = 3;
                return;
            }
            else
            {
                dataEvento = evento.DataEvento.Trim();
                verification = dataEvento.Length == 10;
                if (!verification)
                {
                    evento.Execao = 30;
                    return;
                }
            }

            eventoDao.Update(evento);
            evento.Execao = 99;
            return;
        }

        public void delete(Evento evento)
        {
            bool verification = true;

            Evento eventoAux = new Evento(evento.IdEvento);
            verification = eventoDao.Find(eventoAux);
            if (!verification)
            {
                evento.Execao = 33;
                return;
            }

            verification = funcionarioDao.FindEve(evento.IdEvento);
            if (verification)
            {
                evento.Execao = 50;
                return;
            }
           
            eventoDao.Delete(evento);
            evento.Execao = 99;
            return;
        }


        public bool find(Evento evento)
        {
            return eventoDao.Find(evento);
        }


        public List<Evento> findAll()
        {
            return eventoDao.FindAll();
        }
    }
}
