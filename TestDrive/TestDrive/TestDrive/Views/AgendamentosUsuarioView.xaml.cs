using TestDrive.Models;
using TestDrive.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestDrive.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AgendamentosUsuarioView : ContentPage
    {
        readonly AgendamentosUsuarioViewModel _viewModel;

        public AgendamentosUsuarioView()
        {
            InitializeComponent();
            _viewModel = new AgendamentosUsuarioViewModel();
            BindingContext = _viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            MessagingCenter.Subscribe<Agendamento>(this, "AgendamentoSelecionado", async (agendamento) => 
            {
                if (!agendamento.Confirmado)
                {
                    var reenviar = await DisplayAlert("Reenviar", "Deseja reenviar os dados do agendamento?", "Sim", "Não");

                    if (reenviar)
                    {
                        AgendamentoService service = new AgendamentoService();
                        await service.EnviarAgendamento(agendamento);
                        _viewModel.AtualizarLista();
                    }
                }
            });

            MessagingCenter.Subscribe<Agendamento>(this, "SucessoAgendamento", async (agendamento) => {
                await DisplayAlert("Reenviar" , "Reenvio com sucesso!", "Ok");
            });

            MessagingCenter.Subscribe<Agendamento>(this, "FalhaAgendamento", async (agendamento) => {
                await DisplayAlert("Reenviar", "Falha ao sucesso!", "Ok");
            });
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            MessagingCenter.Unsubscribe<Agendamento>(this, "AgendamentoSelecionado");
            MessagingCenter.Unsubscribe<Agendamento>(this, "SucessoAgendamento");
            MessagingCenter.Unsubscribe<Agendamento>(this, "FalhaAgendamento");
        }
    }
}