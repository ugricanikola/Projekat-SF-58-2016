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
    /// Interaction logic for DodatneUslugeDodavanjeIzmena.xaml
    /// </summary>
    public partial class DodatneUslugeDodavanjeIzmena : Window
    {
        public enum Operacija
        {
            DODAVANJE,
            IZMENA
        }
        private DodatnaUsluga dodatneUsluge;
        private Operacija operacija;
        public DodatneUslugeDodavanjeIzmena( DodatnaUsluga dodatneUsluge, Operacija operacja)
        {
            InitializeComponent();
            this.operacija = operacija;
            this.dodatneUsluge = dodatneUsluge;
            tbCena.DataContext = dodatneUsluge;
            tbNaziv.DataContext = dodatneUsluge;
            
        }

        private void Potvrdi(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            var lista = Projekat.Instance.DodatneUsluge;
            if( operacija == Operacija.DODAVANJE)
            {
                dodatneUsluge.Id = lista.Count + 1;
                lista.Add(dodatneUsluge);
            }
            GenericSerializer.Serialize("dodatne_usluge.xml", lista);
            Close();
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
