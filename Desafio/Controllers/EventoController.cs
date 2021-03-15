using Model.Entity;
using Model.Neg;
using System.Collections.Generic;
using System.Web.Mvc;

namespace DesafioDoChurrasco.Controllers
{
    public class EventoController : Controller
    {
        EventoNeg eventoNeg;

        public EventoController()
        {
            eventoNeg = new EventoNeg();
        }

        // GET: Evento
        public ActionResult Index()
        {
            Evento evento = new Evento();
            List<Evento> list = eventoNeg.findAll();
            return View(list);
        }

        // GET: Evento/Details/5
        public ActionResult Details(int id)
        {
            Evento evento = new Evento(id);
            eventoNeg.find(evento);
            return View(evento);
        }

        // GET: Evento/Create
        public ActionResult Create()
        {
            MensagemInicial();
            return View();
        }

        // POST: Evento/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Evento evento)
        {
            MensagemInicial();
            eventoNeg.create(evento);
            MensagemErroRegistrar(evento);
            return View("Create");
        }

        // GET: Evento/Edit/5
        public ActionResult Edit(int id)
        {
            MensagemUpdate();
            Evento evento = new Evento(id);
            eventoNeg.find(evento);
            return View(evento);
        }

        // POST: Evento/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Evento evento)
        {
            MensagemUpdate();
            eventoNeg.update(evento);
            MensagemErroUpdate(evento);
            return View();
        }

        // GET: Evento/Delete/5
        public ActionResult Delete(int id)
        {
            MensagemDelete();
            Evento evento = new Evento(id);
            eventoNeg.find(evento);
            return View(evento);
        }

        // POST: Evento/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Evento evento)
        {
            MensagemDelete();
            eventoNeg.delete(evento);
            MensagemErroDelete(evento);
            return View();
        }

        private void MensagemInicial()
        {
            ViewBag.MensagemInicio = "Insira os dados e clique em Salvar";
        }

        private void MensagemDelete()
        {
            ViewBag.MensagemDelete = "Formulário de Deleção";
        }

        private void MensagemUpdate()
        {
            ViewBag.MensagemUpdate = "Insira os dados para alterar o evento";
        }

        private void MensagemErroRegistrar(Evento evento)
        {
            switch (evento.Execao)
            {
                case 2:
                    ViewBag.MensagemErro = "Insira o nome do evento ";
                    break;

                case 20:
                    ViewBag.MensagemErro = "O nome do evento não pode ser maior que 50 caracteres";
                    break;

                case 3:
                    ViewBag.MensagemErro = "Insira a data do churrasco";
                    break;

                case 30:
                    ViewBag.MensagemErro = "A data do evento dever ter 10 caracteres";
                    break;

                case 99:
                    ViewBag.MensagemSucesso = "Evento ( " + evento.Nome + " ) foi inserido no sistema";
                    break;
            }
        }

        private void MensagemErroUpdate(Evento evento)
        {
            switch (evento.Execao)
            {
                case 2:
                    ViewBag.MensagemErro = "Insira o nome do evento ";
                    break;

                case 20:
                    ViewBag.MensagemErro = "O nome do evento não pode ser maior que 50 caracteres";
                    break;

                case 3:
                    ViewBag.MensagemErro = "Insira a data do churrasco";
                    break;

                case 30:
                    ViewBag.MensagemErro = "A data do evento dever ter 10 caracteres";
                    break;

                case 99:
                    ViewBag.MensagemSucesso = "Dados do Evento ( " + evento.Nome + " ) foram modificados";
                    break;
            }
        }

        private void MensagemErroDelete(Evento evento)
        {
            switch (evento.Execao)
            {
                case 33:
                    ViewBag.MensagemErro = "Evento ( " + evento.Nome + " ) não está mais cadastrado no sistema";
                    break;

                case 50:
                    ViewBag.MensagemErro = "O evento ( " + evento.Nome + " ) não pode ser excluido pois há funcionários cadastrados";
                    break;

                case 99:
                    ViewBag.MensagemSucesso = "Evento ( " + evento.Nome + " ) foi excluido!!!";
                    break;

                default:
                    ViewBag.MensagemErro = "===Deu Erro ???===";
                    break;
            }

        }
    }
}