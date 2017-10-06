using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TestDrive.ViewModels;
using TestDrive.Models;

namespace TestDrive.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MasterView : ContentPage
	{
        public MasterViewModel ViewModel { get; set; }

        public MasterView (Usuario usuario)
		{
			InitializeComponent();
            ViewModel = new MasterViewModel(usuario);
            BindingContext = ViewModel;
		}
	}
}