using SF_58_2016.Utils;
//using POP_SF42_2016_GUI.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SF_58_2016.Model
{
    public class Projekat
    {
        public static Projekat Instance { get; } = new Projekat();
        public ObservableCollection<Namestaj> Namestaj { get; set; } = GenericSerializer.Deserialize<Namestaj>("namestaj.xml");
        public ObservableCollection<TipNamestaja> TipNamestaja { get; set; }
        public ObservableCollection<Akcija> Akcije { get; set; }
        public ObservableCollection<Korisnik> Korisnici { get; set; }
        public ObservableCollection<ProdajaNamestaja> Prodaja { get; set; } = GenericSerializer.Deserialize<ProdajaNamestaja>("prodaja_namestaja.xml");
        public ObservableCollection<DodatnaUsluga> DodatneUsluge { get; set; }
        public ObservableCollection<StavkaProdaje> StavkeProdaje { get; set; } = GenericSerializer.Deserialize<StavkaProdaje>("stavka_prodaje.xml");

        private Projekat()
        {

            TipNamestaja = GenericSerializer.Deserialize<TipNamestaja>("tip_namestaja.xml");
            Akcije = GenericSerializer.Deserialize<Akcija>("akcije.xml");
            Korisnici = GenericSerializer.Deserialize<Korisnik>("korisnici.xml");
            DodatneUsluge = GenericSerializer.Deserialize<DodatnaUsluga>("dodatne_usluge.xml");


        }
    }
}
