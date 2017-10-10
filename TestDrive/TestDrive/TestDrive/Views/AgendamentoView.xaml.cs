using Xamarin.Forms;
using TestDrive.Models;
using TestDrive.ViewModels;
using System;

namespace TestDrive.Views
{
    //[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AgendamentoView : ContentPage
    {
        public AgendamentoViewModel ViewModel { get; set; }

        public AgendamentoView(Veiculo veiculo, Usuario usuario)
        {
            InitializeComponent();
            ViewModel = new AgendamentoViewModel(veiculo, usuario);
            BindingContext = ViewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            MessagingCenter.Subscribe<Agendamento>(this, "Agendamento",
                async (msg) =>
                {
                    var confirma = await DisplayAlert("Salvar Agendamento",
                    "Deseja mesmo enviar o agendamento?",
                    "sim", "não");

                    if (confirma)
                    {
                        ViewModel.SalvarAgendamento();
                    }
                });

            MessagingCenter.Subscribe<Agendamento>(this, "SucessoAgendamento", async (msg) =>
            {
                await DisplayAlert("Agendamento", "Agendamento salvo com sucesso!", "Ok");
                await Navigation.PopToRootAsync();
            });

            MessagingCenter.Subscribe<ArgumentException>(this, "FalhaAgendamento", async (msg) =>
            {
                await DisplayAlert("Agendamento", "Erro ao fazer o agendamento!", "Ok");
                await Navigation.PopToRootAsync();
            });
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            MessagingCenter.Unsubscribe<Agendamento>(this, "Agendamento");
            MessagingCenter.Unsubscribe<Agendamento>(this, "SucessoAgendamento");
            MessagingCenter.Unsubscribe<ArgumentException>(this, "FalhaAgendamento");
        }
    }
}