using System;
using System.ComponentModel.DataAnnotations;

namespace Model.Entity
{
    public class Evento
    {
        private int idEvento;
        private string nome;
        private int gastoComida;
        private int gastoBebida;
        private int gastoTotal;
        private int totalFuncionarios;
        private int totalConvidados;
        private string dataEvento;
        public int execao;

        public Evento()
        {
        }

        public Evento(int idEvento)
        {
            this.idEvento = idEvento;
        }

        public Evento(int idEvento, string nome, int gastoComida, int gastoBebida, int gastoTotal, int totalFuncionarios, int totalConvidados, string dataEvento)
        {
            this.idEvento = idEvento;
            this.nome = nome;
            this.gastoComida = gastoComida;
            this.gastoBebida = gastoBebida;
            this.gastoTotal = gastoTotal;
            this.totalFuncionarios = totalFuncionarios;
            this.totalConvidados = totalConvidados;
            this.dataEvento = dataEvento;

        }

        [Display(Name = "Código")]
        public int IdEvento
        {
            get => idEvento;
            set => idEvento = value;
        }

        public string Nome
        {
            get => nome;
            set => nome = value;
        }

        [Display(Name = "Gasto com Comida")]
        public int GastoComida
        {
            get => gastoComida;
            set => gastoComida = value;
        }

        [Display(Name = "Gasto com Bebida")]
        public int GastoBebida
        {
            get => gastoBebida;
            set => gastoBebida = value;
        }

        [Display(Name = "Gasto Total")]
        public int GastoTotal
        {
            get => gastoTotal;
            set => gastoTotal = value;
        }

        [Display(Name = "Funcionários")]
        public int TotalFuncionarios
        {
            get => totalFuncionarios;
            set => totalFuncionarios = value;
        }

        [Display(Name = "Convidados")]
        public int TotalConvidados
        {
            get => totalConvidados;
            set => totalConvidados = value;
        }

        [Display(Name = "Data do Evento")]
        public string DataEvento
        {
            get => dataEvento;
            set => dataEvento = value;
        }

        public int Execao
        {
            get => execao;
            set => execao = value;
        }
    }
}
