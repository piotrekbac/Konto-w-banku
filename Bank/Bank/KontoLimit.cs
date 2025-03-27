using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank 
{
    public class KontoLimit : IKonto
    {
        private Konto konto;                        //prywatny obiekt klasy Konto
        private decimal jednorazowyLimitDebetowy;   //prywatna zmienna jednorazowyLimitDebetowy
        private bool debetWykorzystany = false;     //prywatna zmienna debetWykorzystany

        //Konstruktor, który inicjuje obiekt klasy Konto oraz jednorazowyLimitDebetowy
        public KontoLimit(string klient, decimal bilansNaStart, decimal jednorazowyLimitDebetowy)
        {
            konto = new Konto(klient, bilansNaStart);
            this.jednorazowyLimitDebetowy = jednorazowyLimitDebetowy;
        }

        //Właściwości tylko do odczytu (ReadOnly), które zwracają nazwę klienta, bilans oraz stan zablokowania konta
        public string Nazwa => konto.Nazwa;
        public decimal Bilans
        {
            get
            {
                //Jeżeli debetWykorzystany to zwracany jest bilans konta plus jednorazowyLimitDebetowy, w przeciwnym wypadku zwracany jest bilans konta
                if (debetWykorzystany)
                    return konto.Bilans + jednorazowyLimitDebetowy;
                return konto.Bilans;
            }
        }

        public bool Zablokowane => konto.Zablokowane;
        public decimal JednorazowyLimitDebetowy
        {
            get => jednorazowyLimitDebetowy;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Limit debetowy musi być większy lub równy zero.");
                jednorazowyLimitDebetowy = value;
            }
        }

        public void Wyplata(decimal kwota)
        {
            if (konto.Zablokowane)
                throw new InvalidOperationException("Konto jest zablokowane.");
            if (kwota <= 0)
                throw new ArgumentException("Kwota musi być większa od zera.");
            if (kwota > Bilans)
                throw new InvalidOperationException("Niewystarczające środki na koncie, uwzględniając limit debetowy");

            konto.Wyplata(kwota);

            if (konto.Bilans < 0)
            {
                debetWykorzystany = true;
                konto.BlokujKonto();
            }
        }

        public void Wplata(decimal kwota)
        {
            konto.Wplata(kwota);
            if (konto.Bilans >= 0)
            {
                debetWykorzystany = false;
                konto.OdblokujKonto();
            }
        }
        
        public void BlokujKonto()
        {
            debetWykorzystany = true;
            konto.BlokujKonto();
        }

        public void OdblokujKonto()
        {
            konto.OdblokujKonto();
        }

        public Konto KonwertujNaKonto()
        {
            return new Konto(Nazwa, Bilans);
        }

    }
}
