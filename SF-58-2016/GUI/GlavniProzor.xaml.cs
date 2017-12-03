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
            InitializeComponent();
            ProveriPrijavljenogKorisnika();
            
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
        public bool KorisniciIspis(object obj)
        {
            return ((Korisnik)obj).Obrisan == false;
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
                case "TipoviNamestaja":
                    TipNamestaja noviTip = new TipNamestaja();
                    TipNamestajaDodavanjeIzmena tipNamestajaDI = new TipNamestajaDodavanjeIzmena(noviTip, TipNamestajaDodavanjeIzmena.Operacija.DODAVANJE);
                    tipNamestajaDI.ShowDialog();
                    break;
                case "DodatneUsluge":
                    DodatnaUsluga novaDodatnaUsluga = new DodatnaUsluga();
                    DodatneUslugeDodavanjeIzmena dodatneUslugeDI = new DodatneUslugeDodavanjeIzmena(novaDodatnaUsluga, DodatneUslugeDodavanjeIzmena.Operacija.DODAVANJE);
                    dodatneUslugeDI.ShowDialog();
                    break;
                case "Korisnici":
                    Korisnik noviKorisnik = new Korisnik();
                    KorisnikDodavanjeIzmena korisnikDI = new KorisnikDodavanjeIzmena(noviKorisnik, KorisnikDodavanjeIzmena.Operacija.DODAVANJE);
                    korisnikDI.ShowDialog();
                    break;
                case "Akcije":
                    Akcija novaAkcija = new Akcija();
                    AkcijaDodavanjeIzmena akcijaDI = new AkcijaDodavanjeIzmena(novaAkcija, AkcijaDodavanjeIzmena.Operacija.DODAVANJE);
                    akcijaDI.ShowDialog();
                    break;
                case "Prodaja":
                    ProdajaNamestaja novaProdaja = new ProdajaNamestaja();
                    ProdajaProzor pp = new ProdajaProzor(novaProdaja, ProdajaProzor.Operacija.DODAVANJE);
                    pp.ShowDialog();
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
                case "TipoviNamestaja":
                    TipNamestaja tipIzmena = dgTabela.SelectedItem as TipNamestaja;
                    TipNamestaja tipKopija = (TipNamestaja)tipIzmena.Clone();
                    TipNamestajaDodavanjeIzmena tipNamestajaDI = new TipNamestajaDodavanjeIzmena(tipIzmena, TipNamestajaDodavanjeIzmena.Operacija.IZMENA);
                    if(tipNamestajaDI.ShowDialog() != true)
                    {
                        int index = Projekat.Instance.TipNamestaja.IndexOf(tipIzmena);
                        Projekat.Instance.TipNamestaja[index] = tipKopija;
                    }
                    break;
                case "DodatneUsluge":
                    DodatnaUsluga uslugaIzmena = dgTabela.SelectedItem as DodatnaUsluga;
                    DodatnaUsluga uslugaKopija = (DodatnaUsluga)uslugaIzmena.Clone();
                    DodatneUslugeDodavanjeIzmena dodatneUslugeDI = new DodatneUslugeDodavanjeIzmena(uslugaIzmena, DodatneUslugeDodavanjeIzmena.Operacija.IZMENA);
                    if(dodatneUslugeDI.ShowDialog() != true)
                    {
                        int index = Projekat.Instance.DodatneUsluge.IndexOf(uslugaIzmena);
                        Projekat.Instance.DodatneUsluge[index] = uslugaKopija;
                    }
                    break;
                case "Korisnici":
                    Korisnik korisnikIzmena = dgTabela.SelectedItem as Korisnik;
                    Korisnik korisnikKopija = (Korisnik)korisnikIzmena.Clone();
                    KorisnikDodavanjeIzmena korisnikDI = new KorisnikDodavanjeIzmena(korisnikIzmena, KorisnikDodavanjeIzmena.Operacija.IZMENA);
                    if(korisnikDI.ShowDialog() != true)
                    {
                        int index = Projekat.Instance.Korisnici.IndexOf(korisnikIzmena);
                        Projekat.Instance.Korisnici[index] = korisnikKopija;
                    }
                    break;
                case "Akcija":
                    Akcija akcijaIzmena = dgTabela.SelectedItem as Akcija;
                    Akcija akcijaKopija = (Akcija)akcijaIzmena.Clone();
                    AkcijaDodavanjeIzmena akcijaDI = new AkcijaDodavanjeIzmena(akcijaIzmena, AkcijaDodavanjeIzmena.Operacija.IZMENA);
                    if(akcijaDI.ShowDialog() != true)
                    {
                        int index = Projekat.Instance.Akcije.IndexOf(akcijaIzmena);
                        Projekat.Instance.Akcije[index] = akcijaKopija;
                    }
                    break;
                case "Prodaja":
                    ProdajaNamestaja prodaja = dgTabela.SelectedItem as ProdajaNamestaja;
                    ProdajaNamestaja prodajaKopija = (ProdajaNamestaja)prodaja.Clone();
                    ProdajaProzor pp = new ProdajaProzor(prodaja, ProdajaProzor.Operacija.IZMENA);
                    if(pp.ShowDialog() != true)
                    {
                        int index = Projekat.Instance.Prodaja.IndexOf(prodaja);
                        Projekat.Instance.Prodaja[index] = prodajaKopija;
                    }
                    break;

            }
        }

        private void Izbrisi(object sender, RoutedEventArgs e)
        {
            switch (TrenutnoAktivno)
            {
                case "Namestaj":
                    var listNamestaj = Projekat.Instance.Namestaj;
                    Namestaj namestajIzbrisi = dgTabela.SelectedItem as Namestaj;
                    if (MessageBox.Show("Obrisati?", "Potvrda", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK)
                    {
                        namestajIzbrisi.Obrisan = true;
                        GenericSerializer.Serialize("namestaj.xml", listNamestaj);
                    }
                    break;
                case "TipoviNamestaja":
                    var listTipNamestaja = Projekat.Instance.TipNamestaja;
                    TipNamestaja tipNamestajaIzbrisi = dgTabela.SelectedItem as TipNamestaja;
                    if (MessageBox.Show("Obrisati?", "Potvrda", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK)
                    {
                        tipNamestajaIzbrisi.Obrisan = true;
                        GenericSerializer.Serialize("tip_namestaja.xml", listTipNamestaja);
                    }
                    break;
                case "DodatneUsluge":
                    var listDodatneUsluge = Projekat.Instance.DodatneUsluge;
                    DodatnaUsluga dodatneUslugeIzbrisi = dgTabela.SelectedItem as DodatnaUsluga;
                    if (MessageBox.Show("Obrisati?", "Potvrda", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK)
                    {
                        dodatneUslugeIzbrisi.Obrisan = true;
                        GenericSerializer.Serialize("dodatne_usluge.xml", listDodatneUsluge);
                    }
                    break;
                case "Korisnici":
                    var listKorisnici = Projekat.Instance.Korisnici;
                    var korisnikIzbrisi = dgTabela.SelectedItem as Korisnik;
                    if (MessageBox.Show("Obrisati?", "Potvrda", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK)
                    {
                        korisnikIzbrisi.Obrisan = true;
                        GenericSerializer.Serialize("korisnici.xml", listKorisnici);
                    }
                    break;
                case "Akcija":
                    var listAkcija = Projekat.Instance.Akcije;
                    var akcijaIzbrisi = dgTabela.SelectedItem as Akcija;
                    if (MessageBox.Show("Obrisati?", "Potvrda", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK)
                    {
                        akcijaIzbrisi.Obrisan = true;
                        GenericSerializer.Serialize("akcije.xml", listAkcija);
                    }
                    break;
                case "Prodaja":
                    var listProdaja = Projekat.Instance.Prodaja;
                    ProdajaNamestaja prodajaIzbrisi = dgTabela.SelectedItem as ProdajaNamestaja;
                    if (MessageBox.Show("Obrisati?", "Potvrda", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK)
                    {
                        prodajaIzbrisi.Obrisan = true;
                        GenericSerializer.Serialize("prodaja_namestaja.xml", listProdaja);
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
