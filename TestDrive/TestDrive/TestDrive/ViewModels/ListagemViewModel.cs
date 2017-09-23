using Xamarin.Forms;
using System.Net.Http;
using Newtonsoft.Json;
using TestDrive.Models;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace TestDrive.ViewModels
{
    public class ListagemViewModel : BaseViewModel
    {
        public ObservableCollection<Veiculo> Veiculos { get; set; }
        const string URL = "http://aluracar.herokuapp.com/";

        private bool aguarde;
        public bool Aguarde
        {
            get { return aguarde; }
            set
            {
                aguarde = value;
                OnPropertyChanged();
            }
        }

        private Veiculo veiculoSelecionado;

        public Veiculo VeiculoSelecionado
        {
            get { return veiculoSelecionado; }
            set
            {
                veiculoSelecionado = value;

                if (value != null)
                    MessagingCenter.Send(veiculoSelecionado, "VeiculoSelecionado");
            }
        }

        public ListagemViewModel()
        {
            Veiculos = new ObservableCollection<Veiculo>();
        }

        public async Task GetVeiculos()
        {
            Aguarde = true;
            HttpClient client = new HttpClient();
            var resultado = await client.GetStringAsync(URL);
            var veiculosJson = JsonConvert.DeserializeObject<VeiculoJson[]>(resultado);

            foreach (var item in veiculosJson)
            {
                Veiculos.Add(new Veiculo
                {
                    Nome = item.nome,
                    Preco = item.preco
                });
            }

            Aguarde = false;
        }

    }

    public class VeiculoJson
    {
        public string nome { get; set; }
        public int preco { get; set; }
    }
}
