using SQLite;
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
            _conexao.Insert(agendamento);
        }
    }
}
