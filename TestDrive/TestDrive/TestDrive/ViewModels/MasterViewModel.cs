using System.Windows.Input;
using TestDrive.Models;
using Xamarin.Forms;

namespace TestDrive.ViewModels
{
    public class MasterViewModel : BaseViewModel
    {
        public string Nome
        {
            get { return _usuario.Nome; }
            set { _usuario.Nome = value; }
        }

        public string Email
        {
            get { return _usuario.Email; }
            set { _usuario.Email = value; }
        }

        public string DataNascimento
        {
            get { return _usuario.DataNascimento; }
            set { _usuario.DataNascimento = value; }
        }

        public string Telefone
        {
            get { return _usuario.Telefone; }
            set { _usuario.Telefone = value; }
        }

        private bool editando = false;
        public bool Editando
        {
            get { return editando; }
            private set
            {
                editando = value;
                OnPropertyChanged();
            }
        }


        public ICommand EditarPerfilCommand { get; private set; }
        public ICommand SalvarCommand { get; private set; }
        public ICommand EditarCommand { get; private set; }

        private readonly Usuario _usuario;

        public MasterViewModel(Usuario usuario)
        {
            _usuario = usuario;

            EditarPerfilCommand = new Command(() => 
            {
                MessagingCenter.Send(usuario, "EditarPerfil");
            });

            SalvarCommand = new Command(() => 
            {
                Editando = false;
                MessagingCenter.Send(usuario, "SucessoSalvarUsuario");
            });

            EditarCommand = new Command(() =>
            {
                Editando = true;
            });
        }
    }
}
