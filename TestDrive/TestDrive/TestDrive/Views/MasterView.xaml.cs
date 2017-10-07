using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TestDrive.ViewModels;
using TestDrive.Models;

namespace TestDrive.Views
{
	//[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MasterView : TabbedPage
	{
        public MasterView (Usuario usuario)
		{
			InitializeComponent();
            BindingContext = new MasterViewModel(usuario);
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();

            MessagingCenter.Subscribe<Usuario>(this, "EditarPerfil", (usuario) => 
            {
                CurrentPage = Children[1];
            });

            MessagingCenter.Subscribe<Usuario>(this, "SucessoSalvarUsuario", (usuario) => 
            {
                CurrentPage = Children[0];
            });
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            MessagingCenter.Unsubscribe<Usuario>(this, "EditarPerfil");
            MessagingCenter.Unsubscribe<Usuario>(this, "SucessoSalvarUsuario");
        }
    }
}