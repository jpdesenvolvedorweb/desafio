using System.ComponentModel.DataAnnotations;

namespace Model.Entity
{
    public class Funcionario
    {
        private int idFuncionario;
        private string nome;
        private int idEvento;
        private char status;
        public int execao;

        public Funcionario()
        {
        }

        public Funcionario(int idFuncionario)
        {
            this.idFuncionario = idFuncionario;
        }

        public Funcionario(int idFuncionario, string nome, int idEvento, char status)
        {
            this.idFuncionario = idFuncionario;
            this.nome = nome;
            this.idEvento = idEvento;
            this.status = status;
        }

        [Display(Name = "Código")]
        public int IdFuncionario
        {
            get => idFuncionario;
            set => idFuncionario = value;
        }

        public string Nome
        {
            get => nome;
            set => nome = value;
        }

        [Display(Name = "Código do Evento")]
        public int IdEvento
        {
            get => idEvento;
            set => idEvento = value;
        }

        public char Status
        {
            get => status;
            set => status = value;
        }

        public int Execao
        {
            get => execao;
            set => execao = value;
        }
    }
}
