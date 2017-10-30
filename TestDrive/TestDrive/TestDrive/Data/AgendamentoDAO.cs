using SQLite;
using System.Collections.Generic;
using System.Linq;
using TestDrive.Models;

namespace TestDrive.Data
{
    public class AgendamentoDAO
    {
        private readonly SQLiteConnection _conexao;

        public AgendamentoDAO(SQLiteConnection conexao)
        {
            _conexao = conexao;
            _conexao.CreateTable<Agendamento>();
        }

        public void Salvar(Agendamento agendamento)
        {
            if (_conexao.Find<Agendamento>(agendamento.ID) == null)
                _conexao.Insert(agendamento);
            else
                _conexao.Update(agendamento);
        }

        private List<Agendamento> lista;

        public List<Agendamento> Lista
        {
            get
            {
                return _conexao.Table<Agendamento>().ToList();
            }
            set { lista = value; }
        }

    }
}
