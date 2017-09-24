using Xamarin.Forms;
using TestDrive.Models;
using System.Windows.Input;

namespace TestDrive.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private string usuario;
        public string Usuario
        {
            get { return usuario; }
            set {
                usuario = value;
                ((Command)EntrarCommand).ChangeCanExecute();
            }
        }

        private string senha;
        public string Senha
        {
            get { return senha; }
            set {
                senha = value;
                ((Command)EntrarCommand).ChangeCanExecute();
            }
        }

        public ICommand EntrarCommand { get; private set; }

        public LoginViewModel()
        {
            EntrarCommand = new Command(() =>
            {
                MessagingCenter.Send(new Usuario(), "SucessoLogin");
            },
                () => 
                {
                    return (!string.IsNullOrWhiteSpace(usuario) && !string.IsNullOrWhiteSpace(senha));
                }
            );
        }
    }
}
