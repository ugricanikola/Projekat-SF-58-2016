using SF_58_2016.Model;
using SF_58_2016.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for GlavniProzor.xaml
    /// </summary>
    public partial class GlavniProzor : Window
    {
        ICollectionView view;
        public static string TrenutnoAktivno;
        public GlavniProzor()
        {
            ProveriPrijavljenogKorisnika();
            InitializeComponent();
        }

        private void ProveriPrijavljenogKorisnika()
        {
            var korisnik = Korisnik.PronadjiKorisnika(MainWindow.loggedUser);
            if(korisnik.TipKorisnika != TipKorisnika.Administrator)
            {
                btnAkcije.Visibility = Visibility.Hidden;
                btnDodatneUsluge.Visibility = Visibility.Hidden;
                btnKorisnici.Visibility = Visibility.Hidden;
                btnNamestaj.Visibility = Visibility.Hidden;
                btnTipoviNamestaja.Visibility = Visibility.Hidden;
            }
        }
        private void logout(object sender, RoutedEventArgs e)
        {
            var log = new MainWindow();
            this.Close();
            log.ShowDialog();
            return;
            
        }
        public bool NamestajIspis(object obj)
        {
            return ((Namestaj)obj).Obrisan == false;
        }

        private void dgTabela_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if ((string)e.Column.Header == "Id"|| (string)e.Column.Header == "Obrisan"||(string)e.Column.Header == "NamestajProdajaId"
                ||(string)e.Column.Header == "DodatneUslugaId"||(string)e.Column.Header == "StavkaProdajeId"
                ||(string)e.Column.Header == "TipNamestajaId"||(string)e.Column.Header == "NamestajPopustId")
            {
                e.Cancel = true;
            }
        }

        private void NamestajMeni(object sender, RoutedEventArgs e)
        {
            TrenutnoAktivno = "Namestaj";
            view = CollectionViewSource.GetDefaultView(Projekat.Instance.Namestaj);
            view.Filter = NamestajIspis;
            dgTabela.ItemsSource = view;
        }

        private void Dodavanje(object sender, RoutedEventArgs e)
        {
            switch (TrenutnoAktivno)
            {
                case "Namestaj":
                    Namestaj noviNamestaj = new Namestaj();
                    NamestajDodavanjeIzmena namestajDI = new NamestajDodavanjeIzmena(noviNamestaj, NamestajDodavanjeIzmena.Operacija.DODAVANJE);
                    namestajDI.ShowDialog();
                    break;
            }
        }

        private void Izmena(object sender, RoutedEventArgs e)
        {
            switch (TrenutnoAktivno)
            {
                case "Namestaj":
                    Namestaj namestajIzmena = dgTabela.SelectedItem as Namestaj;
                    Namestaj namestajKopija = (Namestaj)namestajIzmena.Clone();
                    NamestajDodavanjeIzmena namestajDI = new NamestajDodavanjeIzmena(namestajIzmena, NamestajDodavanjeIzmena.Operacija.IZMENA);
                    if(namestajDI.ShowDialog()!= true)
                    {
                        int index = Projekat.Instance.Namestaj.IndexOf(namestajIzmena);
                        Projekat.Instance.Namestaj[index] = namestajKopija;
                    }
                    break;
            }
        }

        private void Izbrisi(object sender, RoutedEventArgs e)
        {
            switch (TrenutnoAktivno)
            {
                case "Namestaj":
                    var list = Projekat.Instance.Namestaj;
                    Namestaj namestajIzbrisi = dgTabela.SelectedItem as Namestaj;
                    if (MessageBox.Show("Obrisati?", "Potvrda", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK)
                    {
                        namestajIzbrisi.Obrisan = true;
                        GenericSerializer.Serialize("namestaj.xml", list);
                    }
                    break;
            }
        }

        private void DodatneUslugeM(object sender, RoutedEventArgs e)
        {
            TrenutnoAktivno = "DodatneUsluge";
            dgTabela.ItemsSource = Projekat.Instance.DodatneUsluge;
        }

        private void TipNamestajaM(object sender, RoutedEventArgs e)
        {
            TrenutnoAktivno = "TipoviNamestaja";
            dgTabela.ItemsSource = Projekat.Instance.TipNamestaja;
        }

        private void ProdajaM(object sender, RoutedEventArgs e)
        {
            TrenutnoAktivno = "Prodaja";
            dgTabela.ItemsSource = Projekat.Instance.Prodaja;
        }

        private void AkcijeM(object sender, RoutedEventArgs e)
        {
            TrenutnoAktivno = "Akcije";
            dgTabela.ItemsSource = Projekat.Instance.Akcije;
        }

        private void KorisniciM(object sender, RoutedEventArgs e)
        {
            TrenutnoAktivno = "Korisnici";
            dgTabela.ItemsSource = Projekat.Instance.Korisnici;
        }
    }
}
