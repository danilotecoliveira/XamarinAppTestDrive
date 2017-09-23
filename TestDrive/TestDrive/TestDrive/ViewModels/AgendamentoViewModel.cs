using System;
using Xamarin.Forms;
using TestDrive.Models;
using System.Windows.Input;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace TestDrive.ViewModels
{
    public class AgendamentoViewModel : BaseViewModel
    {
        const string URL_POST_AGENDAMENTO = "https://aluracar.herokuapp.com/salvaragendamento";
        public Agendamento Agendamento { get; set; }
        public ICommand ComandoAgendar { get; set; }

        public AgendamentoViewModel(Veiculo veiculo)
        {
            Agendamento = new Agendamento();
            Veiculo = veiculo;
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


        public Veiculo Veiculo
        {
            get
            {
                return Agendamento.Veiculo;
            }
            set
            {
                Agendamento.Veiculo = value;
            }
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
                carro = Veiculo.Nome,
                preco = Veiculo.Preco,
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

            if (resposta.IsSuccessStatusCode)
                MessagingCenter.Send(Agendamento, "SucessoAgendamento");
            else
                MessagingCenter.Send(new ArgumentException(), "FalhaAgendamento");
        }
    }
}
