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
    /// Interaction logic for KorisnikDodavanjeIzmena.xaml
    /// </summary>
    public partial class KorisnikDodavanjeIzmena : Window
    {
        public enum Operacija
        {
            DODAVANJE,
            IZMENA
        };
        private Korisnik korisnik;
        private Operacija operacija;
        public KorisnikDodavanjeIzmena(Korisnik korisnik,Operacija operacija)
        {
            InitializeComponent();
            this.korisnik = korisnik;
            this.operacija = operacija;
            tbIme.DataContext = korisnik.Ime;
            tbPrezime.DataContext = korisnik.Prezime;
            tbKorisnickoIme.DataContext = korisnik;
            tbLozinka.DataContext = korisnik;
            cbTip.ItemsSource = Enum.GetValues(typeof(TipKorisnika)).Cast<TipKorisnika>();
            cbTip.DataContext = korisnik;
            
        }

        private void Izadji(object sender, RoutedEventArgs e)
        {
            var izadji = new GlavniProzor();
            this.Close();
            izadji.ShowDialog();
            return;
        }

        private void Potvrdi(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            var listaKorisnika = Projekat.Instance.Korisnici;
            var tip_korisnika = (TipKorisnika)cbTip.SelectedItem;
            if (operacija == Operacija.DODAVANJE)
            {
                korisnik.Id = listaKorisnika.Count + 1;
                listaKorisnika.Add(korisnik);
            }
            GenericSerializer.Serialize("korisnici.xml", listaKorisnika);
            Close();
        }
    }
}
