using Model.Entity;
using Model.Neg;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Desafio.Controllers
{
    public class FuncionarioController : Controller
    {
        FuncionarioNeg funcionarioNeg;

        public FuncionarioController()
        {
            funcionarioNeg = new FuncionarioNeg();
        }

        // GET: Funcionario
        public ActionResult Index()
        {
            List<Funcionario> list = funcionarioNeg.findAll();
            return View(list);
        }

        // GET: Funcionario/Details/5
        public ActionResult Details(int id)
        {
            Funcionario funcionario = new Funcionario(id);
            funcionarioNeg.find(funcionario);
            return View(funcionario);
        }

        // GET: Funcionario/Create
        public ActionResult Create()
        {
            MensagemInicial();
            return View();
        }

        // POST: Funcionario/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Funcionario funcionario)
        {
            MensagemInicial();
            funcionarioNeg.create(funcionario);
            MensagemErroRegistrar(funcionario);
            return View("Create");
        }

        // GET: Funcionario/Edit/5
        public ActionResult Edit(int id)
        {
            MensagemUpdate();
            Funcionario funcionario = new Funcionario(id);
            funcionarioNeg.find(funcionario);
            return View(funcionario);
        }

        // POST: Funcionario/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Funcionario funcionario)
        {
            MensagemUpdate();
            funcionarioNeg.update(funcionario);
            MensagemErroUpdate(funcionario);
            return View();
        }

        // GET: Funcionario/Delete/5
        public ActionResult Delete(int id)
        {
            MensagemDelete();
            Funcionario funcionario = new Funcionario(id);
            funcionarioNeg.find(funcionario);
            return View(funcionario);
        }

        // POST: Funcionario/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Funcionario funcionario)
        {
            MensagemDelete();
            funcionarioNeg.delete(funcionario);
            MensagemDelete(funcionario);
            return View();
        }

        //GET: Funcionario/SearchFuncionario
        public ActionResult SearchFuncionario()
        {
            List<Funcionario> list = funcionarioNeg.findAll();
            return View(list);
        }

        //POST: Funcionario/SearchFuncionario
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SearchFuncionario(int txtcodigo = -1)
        {

            if (txtcodigo == -1)
                MensagemSeachFuncionarios();

            List<Funcionario> list = funcionarioNeg.findAllFun(txtcodigo);
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
            ViewBag.MensagemUpdate = "Insira os dados para alterar o funcionário";
        }

        private void MensagemSeachFuncionarios()
        {
            ViewBag.MessageEmpty = "Deve ser digitado algum campo";
        }

        private void MensagemErroRegistrar(Funcionario funcionario)
        {
            switch (funcionario.Execao)
            {
                case 2:
                    ViewBag.MensagemErro = "Insira o nome do funcionário ";
                    break;

                case 20:
                    ViewBag.MensagemErro = "O nome do funcionário não pode ser maior que 50 caracteres";
                    break;

                case 3:
                    ViewBag.MensagemErro = "Insira o código do evento";
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
                    ViewBag.MensagemErro = "Evento ( " + funcionario.IdEvento + " ) não está registrado no sistema";
                    break;

                case 99:
                    ViewBag.MensagemSucesso = "Funcionario ( " + funcionario.Nome + " ) foi inserido no sistema";
                    break;
            }
        }

        private void MensagemErroUpdate(Funcionario funcionario)
        {
            switch (funcionario.Execao)
            {
                case 2:
                    ViewBag.MensagemErro = "Insira o nome do funcionário ";
                    break;

                case 20:
                    ViewBag.MensagemErro = "O nome do funcionário não pode ser maior que 50 caracteres";
                    break;

                case 3:
                    ViewBag.MensagemErro = "Insira o código do evento";
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
                    ViewBag.MensagemSucesso = "Dados do Convidado ( " + funcionario.Nome + " ) foram modificados";
                    break;
            }
        }

        private void MensagemDelete(Funcionario funcionario)
        {
            switch (funcionario.Execao)
            {
                case 33:
                    ViewBag.MensagemErro = "Funcionário ( "+ funcionario.Nome +" ) não está mais cadastrado no sistema";
                    break;

                case 50:
                    ViewBag.MensagemErro = "Não se pode excluir o funcionário ( "+ funcionario.Nome +" ) pois existe um convidado associado";
                    break;

                case 99:
                    ViewBag.MensagemSucesso = "Funcionário ( " + funcionario.Nome + " ) foi excluido!!!";
                    break;

                default:
                    ViewBag.MensagemErro = "===Deu Erro ???===";
                    break;
            }
        }
    }
}
