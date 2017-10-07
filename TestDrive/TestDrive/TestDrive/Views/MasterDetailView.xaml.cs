using TestDrive.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestDrive.Views
{
    //[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterDetailView : MasterDetailPage
    {
        private readonly Usuario _usuario;

        public MasterDetailView(Usuario usuario)
        {
            InitializeComponent();
            _usuario = usuario;
            Master = new MasterView(usuario);
        }
    }
}