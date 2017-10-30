using SQLite;
using System;

namespace TestDrive.Models
{
    public class Agendamento
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Nome { get; set; }
        public string Fone { get; set; }
        public string Email { get; set; }
        public string Modelo { get; set; }
        public decimal Preco { get; set; }
        public bool Confirmado { get; set; }
        public string DataFormatada
        {
            get
            {
                return DataAgendamento.Add(HoraAgendamento).ToString("dd/MM/yyyy HH:mm");
            }
        }

        DateTime dataAgendamento = DateTime.Today;
        public DateTime DataAgendamento
        {
            get
            {
                return dataAgendamento;
            }
            set
            {
                dataAgendamento = value;
            }
        }

        public TimeSpan HoraAgendamento { get; set; }

        public Agendamento(string nome, string fone, string email, string modelo, decimal preco, DateTime dataAgendamento, TimeSpan horaAgendamento)
            : this(nome, fone, email, modelo, preco)
        {
            DataAgendamento = DataAgendamento;
            HoraAgendamento = HoraAgendamento;
        }

        public Agendamento(string nome, string fone, string email, string modelo, decimal preco)
        {
            Nome = nome;
            Fone = fone;
            Email = email;
            Modelo = modelo;
            Preco = preco;
        }

        public Agendamento()
        {

        }
    }
}
