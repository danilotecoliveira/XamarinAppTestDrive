using Xamarin.Forms;
using TestDrive.Models;
using System.Windows.Input;

namespace TestDrive.ViewModels
{
    public class DetalheViewModel : BaseViewModel
    {
        public Veiculo Veiculo { get; set; }
        public ICommand ProximoComando { get; set; }

        public string FreioABS
        {
            get
            {
                return string.Format("Freio ABS - R$ {0}", Veiculo._freioABS);
            }
        }

        public string ArCondicionado
        {
            get
            {
                return string.Format("Ar condicionado - R$ {0}", Veiculo._arCondicionado);
            }
        }

        public string MP3Player
        {
            get
            {
                return string.Format("Mp3 Player - R$ {0}", Veiculo._mp3);
            }
        }

        public bool TemFreioABS
        {
            get
            {
                return Veiculo.TemFreioAbs;
            }
            set
            {
                Veiculo.TemFreioAbs = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(ValorTotal));
            }
        }

        public bool TemArCondicionado
        {
            get
            {
                return Veiculo.TemArCondicionado;
            }
            set
            {
                Veiculo.TemArCondicionado = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(ValorTotal));
            }
        }

        public bool TemMP3Player
        {
            get
            {
                return Veiculo.TemMP3Player;
            }
            set
            {
                Veiculo.TemMP3Player = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(ValorTotal));
            }
        }

        public string ValorTotal
        {
            get
            {
                return Veiculo.PrecoTotalFormatado;
            }
        }

        public DetalheViewModel(Veiculo veiculo)
        {
            Veiculo = veiculo;
            ProximoComando = new Command(() => 
            {
                MessagingCenter.Send(veiculo, "Proximo"); 
            });
        }
    }
}
