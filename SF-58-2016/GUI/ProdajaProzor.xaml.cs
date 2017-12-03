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
    /// Interaction logic for ProdajaProzor.xaml
    /// </summary>
    public partial class ProdajaProzor : Window
    {
        public enum Operacija
        {
            DODAVANJE,
            IZMENA
        };
        private ProdajaNamestaja prodaja;
        private Operacija operacija;
        public ProdajaProzor(ProdajaNamestaja prodaja, Operacija operacija)
        {
            InitializeComponent();
            this.prodaja = prodaja;
            this.operacija = operacija;
            dgStavke.ItemsSource = prodaja.StavkeProdaje;
            tbKupac.DataContext = prodaja;
            dpDatum.DataContext = prodaja;
        }

        private void DodajStavku(object sender, RoutedEventArgs e)
        {
            StavkaProdaje stavka = new StavkaProdaje();
            StavkeProzor sp = new StavkeProzor(stavka, StavkeProzor.Operacija.DODAVANJE);
            if (sp.ShowDialog() == true)
                prodaja.StavkeProdaje.Add(sp.Stavka);
        }

        private void Potvrdi(object sender, RoutedEventArgs e)
        {
            Random racun = new Random();
            this.DialogResult = true;
            var lista = Projekat.Instance.Prodaja;
            if(operacija == Operacija.DODAVANJE)
            {
                prodaja.Id = lista.Count + 1;
                prodaja.BrojRacuna = racun.Next(100, 10000);
                lista.Add(prodaja);
            }
            GenericSerializer.Serialize("prodaja_namestaja.xml", lista);
            this.Close();
        }

        private void Izadji(object sender, RoutedEventArgs e)
        {
            var izadji = new GlavniProzor();
            this.Close();
            izadji.ShowDialog();
            return;
        }

        private void dgStavke_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if ((string)e.Column.Header == "DodatnaUslugaId" || (string)e.Column.Header == "NamestajProdajaId" || (string)e.Column.Header == "Id"
                || (string)e.Column.Header == "Obrisan")
            {
                e.Cancel = true;
            }
        }
        private void UkloniStavku(object sender, RoutedEventArgs e)
        {
            StavkaProdaje izabrana = dgStavke.SelectedItem as StavkaProdaje;
            prodaja.StavkeProdaje.Remove(izabrana);

        }
    }
}
