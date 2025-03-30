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
        private bool debetWykorzystany = false;     //prywatna zmienna debetWykorzystany - flaga wskazująca czy debet został wykorzystany

        //Konstruktor, który inicjuje obiekt klasy Konto oraz jednorazowyLimitDebetowy
        public KontoLimit(string klient, decimal bilansNaStart, decimal jednorazowyLimitDebetowy)
        {
            konto = new Konto(klient, bilansNaStart);
            this.jednorazowyLimitDebetowy = jednorazowyLimitDebetowy;
        }

        //Właściwość tylko do odczytu (ReadOnly), która zwraca nazwę klienta
        public string Nazwa => konto.Nazwa;

        //Właściwość publiczna, która zwraca bilans konta
        public decimal Bilans
        {
            get
            {
                //Jeżeli bilans konta jest ujemny to zwracany jest bilans konta plus jednorazowyLimitDebetowy, w przeciwnym wypadku zwracany jest bilans konta
                if (konto.Bilans < 0)
                    return konto.Bilans + jednorazowyLimitDebetowy;
                return konto.Bilans;
            }
        }

        //Właściwość tylko do odczytu (ReadOnly), która zwraca stan zablokowania konta
        public bool Zablokowane => konto.Zablokowane;

        //Właściwość publiczna, która zwraca jednorazowyLimitDebetowy oraz pozwala na jego zmianę
        public decimal JednorazowyLimitDebetowy
        {
            get => jednorazowyLimitDebetowy;
            set
            {
                //Jeżeli wartość jest mniejsza od zera to wyrzucany jest wyjątek ArgumentException z odpowiednią informacją (limit debetowy który musi być większy lub równy 0)
                if (value < 0)
                    throw new ArgumentException("Limit debetowy musi być większy lub równy zero.");
                //Przypisanie wartości do jednorazowyLimitDebetowy
                jednorazowyLimitDebetowy = value;
            }
        }


        //Metoda do wypłacania środków z konta
        public void Wyplata(decimal kwota)
        {
            if (konto.Zablokowane)                                                  //Jeżeli konto jest zablokowane to wyrzucany jest wyjątek:
                throw new InvalidOperationException("Konto jest zablokowane.");     //Wyrzucenie wyjątku InvalidOperationException, gdy konto jest zablokowane
            if (kwota <= 0)                                                         //Jeżeli kwota jest mniejsza lub równa zero to wyrzucany jest wyjątek:
                throw new ArgumentException("Kwota musi być większa od zera.");     //Wyrzucenie wyjątku ArgumentException, gdy kwota jest mniejsza lub równa zero
            if (kwota > Bilans)                    //Jeżeli kwota jest większa od bilansu konta plus limit debetowy to wyrzucany jest wyjątek:
                throw new InvalidOperationException
                    ("Niewystarczające środki na koncie, uwzględniając limit debetowy");     //Wyrzucenie wyjątku InvalidOperationException, gdy występuje brak wystarczających środków na koncie z uwzględnieniem limitu debetowego

            //wypłata kwoty z konta 
            konto.Wyplata(kwota);

            //Jeżeli bilans konta jest mniejszy od zera to debetWykorzystany jest ustawiany na true oraz konto jest blokowane
            if (konto.Bilans < 0)
            {
                debetWykorzystany = true;
                konto.BlokujKonto();
            }
        }

        //Metoda do wpłacania środków na konto
        public void Wplata(decimal kwota)
        {
            konto.Wplata(kwota);
            if (konto.Bilans >= 0)
            {
                //Jeżeli bilans konta jest większy lub równy zero to debetWykorzystany jest ustawiany na false oraz konto jest odblokowane
                debetWykorzystany = false;
                konto.OdblokujKonto();
            }
        }

        //Metoda do blokowania konta
        public void BlokujKonto()
        {
            debetWykorzystany = true;
            konto.BlokujKonto();
        }

        //Metoda do odblokowania konta
        public void OdblokujKonto()
        {
            konto.OdblokujKonto();
        }

        //Metoda konwertująca konto na kontoPlus
        public Konto KonwertujNaKonto()
        {
            return new Konto(Nazwa, Bilans);
        }
    }
}