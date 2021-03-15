using Model.Entity;
using Model.Neg;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Desafio.Controllers
{
    public class ConvidadoController : Controller
    {
        ConvidadoNeg convidadoNeg;

        public ConvidadoController()
        {
            convidadoNeg = new ConvidadoNeg();
        }

        // GET: Convidado
        public ActionResult Index()
        {
            List<Convidado> list = convidadoNeg.findAll();
            return View(list);
        }

        // GET: Convidado/Details/5
        public ActionResult Details(int id)
        {
            Convidado convidado = new Convidado(id);
            convidadoNeg.find(convidado);

            return View(convidado);
        }

        // GET: Convidado/Create
        public ActionResult Create()
        {
            MensagemInicial();
            return View();
        }

        // POST: Convidado/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Convidado convidado)
        {
            MensagemInicial();
            convidadoNeg.create(convidado);
            MensagemErroRegistrar(convidado);
            return View("Create");
        }

        // GET: Convidado/Edit/5
        public ActionResult Edit(int id)
        {
            MensagemUpdate();
            Convidado convidado = new Convidado(id);
            convidadoNeg.find(convidado);
            return View(convidado);
        }

        // POST: Convidado/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Convidado convidado)
        {
            MensagemUpdate();
            convidadoNeg.update(convidado);
            MensagemErroUpdate(convidado);
            return View();
        }

        // GET: Convidado/Delete/5
        public ActionResult Delete(int id)
        {
            MensagemDelete();
            Convidado convidado = new Convidado(id);
            convidadoNeg.find(convidado);
            return View(convidado);
        }

        // POST: Convidado/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Convidado convidado)
        {
            MensagemDelete();
            convidadoNeg.delete(convidado);
            MensagemDelete(convidado);
            return View();
        }

        //GET: Convidado/SearchConvidado
        public ActionResult SearchConvidado()
        {
            List<Convidado> list = convidadoNeg.findAll();
            return View(list);
        }

        //POST: Convidado/SearchConvidado
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SearchConvidado(int txtcodigo = -1)
        {

            if (txtcodigo == -1)
                MensagemSeachConvidados();
            
            List<Convidado> list = convidadoNeg.findAllConv(txtcodigo);
            return View(list);
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
            ViewBag.MensagemUpdate = "Insira os dados para alterar o convidado";
        }

        private void MensagemSeachConvidados()
        {
            ViewBag.MessageEmpty = "Deve ser digitado algum campo";
        }

        private void MensagemErroRegistrar(Convidado convidado)
        {
            switch (convidado.Execao)
            {
                case 2:
                    ViewBag.MensagemErro = "Insira o nome do convidado ";
                    break;

                case 20:
                    ViewBag.MensagemErro = "O nome do convidado não pode ser maior que 50 caracteres";
                    break;

                case 3:
                    ViewBag.MensagemErro = "Insira o código do funcionário";
                    break;

                case 30:
                    ViewBag.MensagemErro = "Código muito grande";
                    break;

                case 4:
                    ViewBag.MensagemErro = "Insira o dado se vai ou não beber";
                    break;

                case 40:
                    ViewBag.MensagemErro = "Não é permitido caracteres diferentes de S ou N";
                    break;

                case 55:
                    ViewBag.MensagemErro = "Funcionário ( " + convidado.IdFuncionario + " ) não está registrado no sistema";
                    break;

                case 60:
                    ViewBag.MensagemErro = "Funcionário ( " + convidado.IdFuncionario + " ) já está levando um convidado";
                    break;

                case 99:
                    ViewBag.MensagemSucesso = "Convidado ( " + convidado.Nome + " ) foi inserido no sistema";
                    break;
            }
        }

        private void MensagemErroUpdate(Convidado convidado)
        {
            switch (convidado.Execao)
            {
                case 2:
                    ViewBag.MensagemErro = "Insira o nome do convidado ";
                    break;

                case 20:
                    ViewBag.MensagemErro = "O nome do convidado não pode ser maior que 50 caracteres";
                    break;

                case 3:
                    ViewBag.MensagemErro = "Insira o código do funcionário";
                    break;

                case 30:
                    ViewBag.MensagemErro = "Código muito grande";
                    break;

                case 4:
                    ViewBag.MensagemErro = "Insira o dado se vai ou não beber";
                    break;

                case 40:
                    ViewBag.MensagemErro = "Não é permitido caracteres diferentes de S ou N";
                    break;

                case 99:
                    ViewBag.MensagemSucesso = "Dados do Convidado ( " + convidado.Nome + " ) foram modificados";
                    break;
            }
        }

        private void MensagemDelete(Convidado convidado)
        {
            switch (convidado.Execao) {

                case 33:
                    ViewBag.MensagemErro = "Convidado ( " + convidado.Nome + " ) não está registrado no sistema";
                    break;

                case 99:
                    ViewBag.MensagemSucesso = "Convidado ( " + convidado.Nome + " ) foi excluido!!!";
                    break;

                default:
                    ViewBag.MensagemErro = "===Deu Erro ???===";
                    break;
            }
        }
    }
}
