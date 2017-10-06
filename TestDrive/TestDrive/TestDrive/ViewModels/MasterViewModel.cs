using TestDrive.Models;

namespace TestDrive.ViewModels
{
    public class MasterViewModel
    {
        private string nome;
        public string Nome
        {
            get { return _usuario.Nome; }
            set { nome = value; }
        }

        private string email;
        public string Email
        {
            get { return _usuario.Email; }
            set { email = value; }
        }

        private readonly Usuario _usuario;

        public MasterViewModel(Usuario usuario)
        {
            _usuario = usuario;
        }
    }
}
