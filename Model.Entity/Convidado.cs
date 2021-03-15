using System.ComponentModel.DataAnnotations;

namespace Model.Entity
{
    public class Convidado
    {
        private int idConvidado;
        private string nome;
        private int idFuncionario;
        private char status;
        public int execao;

        public Convidado()
        {
        }

        public Convidado(int idConvidado)
        {
            this.idConvidado = idConvidado;
        }

        public Convidado(int idConvidado, string nome, int idFuncionario, char status)
        {
            this.idConvidado = idConvidado;
            this.nome = nome;
            this.idFuncionario = idFuncionario;
            this.status = status;
        }

        [Display(Name = "Código")]
        public int IdConvidado
        {
            get => idConvidado;
            set => idConvidado = value;
        }

        public string Nome
        {
            get => nome;
            set => nome = value;
        }

        [Display(Name = "Código do Funcionário")]
        public int IdFuncionario
        {
            get => idFuncionario;
            set => idFuncionario = value;
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
