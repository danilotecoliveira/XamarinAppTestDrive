using Xamarin.Forms;
using TestDrive.Models;
using System.Windows.Input;

namespace TestDrive.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private string usuario = "joao@alura.com.br";
        public string Usuario
        {
            get { return usuario; }
            set {
                usuario = value;
                ((Command)EntrarCommand).ChangeCanExecute();
            }
        }

        private string senha = "alura123";
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
            EntrarCommand = new Command( async () =>
            {
                var loginService = new LoginService();
                await loginService.FazerLogin(new Login(usuario, senha));
            },
                () => 
                {
                    return (!string.IsNullOrWhiteSpace(usuario) && !string.IsNullOrWhiteSpace(senha));
                }
            );
        }
    }
}
