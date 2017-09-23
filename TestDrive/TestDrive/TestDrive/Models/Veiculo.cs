namespace TestDrive.Models
{
    public class Veiculo
    {
        public const int _freioABS = 800;
        public const int _arCondicionado = 1000;
        public const int _mp3 = 500;

        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public string PrecoFormatado
        {
            get { return string.Format("R$ {0}", Preco); }
        }

        public bool TemFreioAbs { get; set; }
        public bool TemArCondicionado { get; set; }
        public bool TemMP3Player { get; set; }

        public string PrecoTotalFormatado
        {
            get
            {
                return string.Format("Valor total: R$ {0}",
                    Preco +
                    ((TemFreioAbs) ? _freioABS : 0) +
                    ((TemArCondicionado) ? _arCondicionado : 0) +
                    ((TemMP3Player) ? _mp3 : 0));
            }
        }
    }
}
