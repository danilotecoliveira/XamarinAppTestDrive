using System;
using Xamarin.Forms;
using TestDrive.Models;
using System.Windows.Input;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using TestDrive.Data;

namespace TestDrive.ViewModels
{
    public class AgendamentoViewModel : BaseViewModel
    {
        const string URL_POST_AGENDAMENTO = "https://aluracar.herokuapp.com/salvaragendamento";
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
            var client = new HttpClient();

            var json = JsonConvert.SerializeObject(new
            {
                nome = Nome,
                fone = Fone,
                email = Email,
                carro = Modelo,
                preco = Preco,
                dataAgendamento =
                new DateTime(
                    DataAgendamento.Year,
                    DataAgendamento.Month,
                    DataAgendamento.Day,
                    HoraAgendamento.Hours,
                    HoraAgendamento.Minutes,
                    HoraAgendamento.Seconds
                )
            });

            var conteudo = new StringContent(json, Encoding.UTF8, "application/json");
            var resposta = await client.PostAsync(URL_POST_AGENDAMENTO, conteudo);

            SalvarAgendamentoDB();

            if (resposta.IsSuccessStatusCode)
                MessagingCenter.Send(Agendamento, "SucessoAgendamento");
            else
                MessagingCenter.Send(new ArgumentException(), "FalhaAgendamento");
        }

        private void SalvarAgendamentoDB()
        {
            using (var conexao = DependencyService.Get<ISQLite>().PegarConexao())
            {
                var dao = new AgendamentoDAO(conexao);
                dao.Salvar(new Agendamento(Nome, Fone, Email, Modelo, Preco));
            }
        }
    }
}
