using Xamarin.Forms;
using TestDrive.Models;
using TestDrive.ViewModels;

namespace TestDrive.Views
{
    //[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetalheView : ContentPage
    {
        public Veiculo Veiculo { get; set; }
        public Usuario Usuario { get; set; }

        public DetalheView(Veiculo veiculo, Usuario usuario)
        {
            InitializeComponent();
            Veiculo = veiculo;
            Usuario = usuario;
            BindingContext = new DetalheViewModel(veiculo);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            MessagingCenter.Subscribe<Veiculo>(this, "Proximo", (veiculo) => 
            {
                Navigation.PushAsync(new AgendamentoView(veiculo, Usuario));
            });
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            MessagingCenter.Unsubscribe<Veiculo>(this, "Proximo");
        }

    }
}