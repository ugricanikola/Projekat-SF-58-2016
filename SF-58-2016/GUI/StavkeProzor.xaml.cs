using SF_58_2016.Model;
using SF_58_2016.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SF_58_2016.GUI
{
    /// <summary>
    /// Interaction logic for StavkeProzor.xaml
    /// </summary>
    public partial class StavkeProzor : Window
    {
        public enum Operacija
        {
            DODAVANJE,
            IZMENA
        };
        public StavkaProdaje Stavka { set; get; }
        private Operacija operacija;
        public StavkeProzor(StavkaProdaje stavkaProdaje, Operacija operacija)
        {
            InitializeComponent();
            dgNamestaj.ItemsSource = NamestajPrikaz();
            dgNamestaj.IsSynchronizedWithCurrentItem = true;
            dgNamestaj.SelectedIndex = 0;
            cbUsluga.ItemsSource = UslugePrikaz();
            cbUsluga.DataContext = Stavka;
            tbKolicina.DataContext = Stavka;
        }
        public List<DodatnaUsluga> UslugePrikaz()
        {
            var usluge = Projekat.Instance.DodatneUsluge;
            List<DodatnaUsluga> Prikaz = new List<DodatnaUsluga>();
            foreach (var usluga in usluge)
            {
                if (usluga.Obrisan == false)
                    Prikaz.Add(usluga);
            }
            return Prikaz;
        }
        public List<Namestaj> NamestajPrikaz()
        {
            var namestaj = Projekat.Instance.Namestaj;
            List<Namestaj>Prikaz = new List<Namestaj>();
            foreach (var trenutni in namestaj)
            {
                if (trenutni.Obrisan == false)
                    Prikaz.Add(trenutni);
            }
            return Prikaz;
        }
        private void PotvrdiUslugu(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            var lista = Projekat.Instance.StavkeProdaje;
            if (operacija == Operacija.DODAVANJE)
            {
                Stavka.Id = lista.Count + 1;
                Stavka.NamestajProdaja = dgNamestaj.SelectedItem as Namestaj;
                lista.Add(Stavka);

            }
            GenericSerializer.Serialize("stavka_prodaje.xml", lista);
            this.Close();
        }

        private void dgNamestaj_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if ((string)e.Column.Header == "Id" || (string)e.Column.Header == "TipNamestajaId" ||
            (string)e.Column.Header == "DodatneUslugaId" || (string)e.Column.Header == "Obrisan")
            {
                e.Cancel = true;
            }
        }

        private void Izadji(object sender, RoutedEventArgs e)
        {
            var izadji = new GlavniProzor();
            this.Close();
            izadji.ShowDialog();
            return;
        }
    }
}
