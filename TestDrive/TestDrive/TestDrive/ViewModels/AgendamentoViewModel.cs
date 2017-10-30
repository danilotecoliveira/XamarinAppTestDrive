using System;
using Xamarin.Forms;
using TestDrive.Models;
using System.Windows.Input;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using TestDrive.Data;
using System.Threading.Tasks;

namespace TestDrive.ViewModels
{
    public class AgendamentoViewModel : BaseViewModel
    {
        public Agendamento Agendamento { get; set; }
        public ICommand ComandoAgendar { get; set; }

        public AgendamentoViewModel(Veiculo veiculo, Usuario usuario)
        {
            Agendamento = new Agendamento(usuario.Nome, usuario.Telefone, usuario.Email, veiculo.Nome, veiculo.Preco);
            ComandoAgendar = new Command(() =>
            {
                MessagingCenter.Send(Agendamento, "Agendamento");
            }, () => 
            {
                return 
                !string.IsNullOrWhiteSpace(Nome) && 
                !string.IsNullOrWhiteSpace(Fone) && 
                !string.IsNullOrWhiteSpace(Email);
            });
        }


        private string Modelo;
        public string modelo
        {
            get { return Agendamento.Nome; }
            set { Agendamento.Nome = value; }
        }

        private decimal Preco;

        public decimal MyProperty
        {
            get { return Agendamento.Preco; }
            set { Agendamento.Preco = value; }
        }



        public string Nome
        {
            get
            {
                return Agendamento.Nome;
            }
            set
            {
                Agendamento.Nome = value;
                OnPropertyChanged();
                ((Command)ComandoAgendar).ChangeCanExecute();
            }
        }

        public string Fone
        {
            get
            {
                return Agendamento.Fone;
            }
            set
            {
                Agendamento.Fone = value;
                OnPropertyChanged();
                ((Command)ComandoAgendar).ChangeCanExecute();
            }
        }

        public string Email
        {
            get
            {
                return Agendamento.Email;
            }
            set
            {
                Agendamento.Email = value;
                OnPropertyChanged();
                ((Command)ComandoAgendar).ChangeCanExecute();
            }
        }

        public DateTime DataAgendamento
        {
            get
            {
                return Agendamento.DataAgendamento;
            }
            set
            {
                Agendamento.DataAgendamento = value;
            }
        }

        public TimeSpan HoraAgendamento
        {
            get
            {
                return Agendamento.HoraAgendamento;
            }
            set
            {
                Agendamento.HoraAgendamento = value;
            }
        }

        public async void SalvarAgendamento()
        {
            AgendamentoService servico = new AgendamentoService();
            await servico.EnviarAgendamento(Agendamento);
        }
    }

    public class AgendamentoService
    {
        const string URL_POST_AGENDAMENTO = "https://aluracar.herokuapp.com/salvaragendamento";

        public async Task EnviarAgendamento(Agendamento agendamento)
        {
            var client = new HttpClient();

            var json = JsonConvert.SerializeObject(new
            {
                nome = agendamento.Nome,
                fone = agendamento.Fone,
                email = agendamento.Email,
                carro = agendamento.Modelo,
                preco = agendamento.Preco,
                dataAgendamento =
                new DateTime(
                    agendamento.DataAgendamento.Year,
                    agendamento.DataAgendamento.Month,
                    agendamento.DataAgendamento.Day,
                    agendamento.HoraAgendamento.Hours,
                    agendamento.HoraAgendamento.Minutes,
                    agendamento.HoraAgendamento.Seconds
                )
            });

            var conteudo = new StringContent(json, Encoding.UTF8, "application/json");
            var resposta = await client.PostAsync(URL_POST_AGENDAMENTO, conteudo);
            agendamento.Confirmado = resposta.IsSuccessStatusCode;

            SalvarAgendamentoDB(agendamento);

            if (agendamento.Confirmado)
                MessagingCenter.Send(agendamento, "SucessoAgendamento");
            else
                MessagingCenter.Send(new ArgumentException(), "FalhaAgendamento");
        }

        public void SalvarAgendamentoDB(Agendamento agendamento)
        {
            using (var conexao = DependencyService.Get<ISQLite>().PegarConexao())
            {
                var dao = new AgendamentoDAO(conexao);
                dao.Salvar(agendamento);
            }
        }
    }
}
