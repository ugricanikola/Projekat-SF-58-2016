using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SF_58_2016.Model
{
    public enum TipKorisnika
    {
        Administrator,
        Prodavac
    }
    public class Korisnik : INotifyPropertyChanged, ICloneable
    {
        private int id;
        public int Id
        {
            get { return id; }
            set { id = value;
                OnPropertyChanged("Id");
            }
        }

        private bool obrisan;
        public bool Obrisan
        {
            get { return obrisan; }
            set { obrisan = value;
                OnPropertyChanged("Obrisan");
            }
        }

        private string korisnicko_Ime;
        public string Korisnicko_Ime
        {
            get { return korisnicko_Ime; }
            set { korisnicko_Ime = value;
                OnPropertyChanged("Korisnicko_Ime");
            }
        }

        private string lozinka;
        public string Lozinka
        {
            get { return lozinka; }
            set { lozinka = value;
                OnPropertyChanged("Lozinka");
            }
        }

        private string ime;
        public string Ime
        {
            get { return ime; }
            set { ime = value;
                OnPropertyChanged("Ime");
            }
        }

        private string prezime;
        public string Prezime
        {
            get { return prezime; }
            set { prezime = value;
                OnPropertyChanged("Prezime");
            }
        }

        private TipKorisnika tipKorisnika;
        public TipKorisnika TipKorisnika
        {
            get { return tipKorisnika; }
            set { tipKorisnika = value;
                OnPropertyChanged("TipKorisnika");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public override string ToString()
        {
            if (!Obrisan)
            {
                return $"{Ime} {Prezime} {Korisnicko_Ime} {Lozinka} {TipKorisnika}";
            }
            return null;
        }

        public Korisnik()
        {
            tipKorisnika = TipKorisnika.Prodavac;
        }

        public static Korisnik PronadjiKorisnika(string userName)
        {
            foreach (var korisnik in Projekat.Instance.Korisnici)
            {
                if (korisnik.Korisnicko_Ime == userName)
                {
                    return korisnik;
                }

            }
            return null;
        }

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public object Clone()
        {
            Korisnik kopija = new Korisnik();
            kopija.Ime = Ime;
            kopija.Prezime = Prezime;
            kopija.Korisnicko_Ime = Korisnicko_Ime;
            kopija.Lozinka = Lozinka;
            kopija.TipKorisnika = TipKorisnika;
            return kopija;
        }
    }
}