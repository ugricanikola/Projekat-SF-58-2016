﻿using SF_58_2016.Model;
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
    /// Interaction logic for AkcijaDodavanjeIzmena.xaml
    /// </summary>
    public partial class AkcijaDodavanjeIzmena : Window
    {
        public enum Operacija
        {
            DODAVANJE,
            IZMENA
        };
        private Akcija akcija;
        private Operacija operacija;
        public AkcijaDodavanjeIzmena(Akcija akcija, Operacija operacija)
        {
            InitializeComponent();
            this.operacija = operacija;
            this.akcija = akcija;
            dpPocetak.DataContext = akcija;
            dpKraj.DataContext = akcija;
            var lista = Projekat.Instance.Namestaj;
            var filtriranaLista = new List<Namestaj>();
            for (int i = 0; i < lista.Count; i++)
                if (lista[i].Obrisan == false)
                    filtriranaLista.Add(lista[i]);
            cbNamestaj.ItemsSource = filtriranaLista;
            cbNamestaj.DataContext = akcija;
            tbPopust.DataContext = akcija;
        }

        private void Potvrdi(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            var lista = Projekat.Instance.Akcije;
            var namestaj = (Namestaj)cbNamestaj.SelectedItem;
            if(operacija == Operacija.DODAVANJE)
            {
                akcija.Id = lista.Count + 1;
                lista.Add(akcija);
            }
            GenericSerializer.Serialize("akcije.xml", lista);
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
