﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SF_58_2016.Model
{

    public class DodatnaUsluga : INotifyPropertyChanged, ICloneable
    {
        private int id;
        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }

        private bool obrisan;
        public bool Obrisan
        {
            get { return obrisan; }
            set
            {
                obrisan = value;
                OnPropertyChanged("Obrisan");
            }
        }

        private string naziv;
        public string Naziv
        {
            get { return naziv; }
            set
            {
                naziv = value;
                OnPropertyChanged("Naziv");
            }
        }

        private double cena;
        public double Cena
        {
            get { return cena; }
            set
            {
                cena = value;
                OnPropertyChanged("Cena");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public override string ToString()
        {
            if (!Obrisan)
            {
                return $"{Naziv}";
            }
            return null;
        }
        public static DodatnaUsluga PronadjiUslugu(int id)
        {
            foreach (var usluga in Projekat.Instance.DodatneUsluge)
            {
                if (usluga.Id == id)
                {
                    return usluga;
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
            DodatnaUsluga clone = new DodatnaUsluga();
            clone.Id = Id;
            clone.Naziv = Naziv;
            clone.Cena = Cena;
            return clone;
        }
    }
}
