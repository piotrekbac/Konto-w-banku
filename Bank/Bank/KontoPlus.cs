using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
    public class KontoPlus : Konto
    {
        private decimal jednorazowyLimitDebetowy;   //prywatna zmienna jednorazowyLimitDebetowy
        private bool debetWykorzystany = false;     //prywatna zmienna debetWykorzystany - flaga wskazująza czy debet został wykorzystany

        //Konstruktor, który inicjuje nazwę klienta, bilansNaStart oraz jednorazowyLimitDebetowy oraz wywołuje konstruktor klasy bazowej Konto z odpowiednimi parametrami klient i bilansNaStart
        public KontoPlus(string klient, decimal bilansNaStart, decimal jednorazowyLimitDebetowy) : base(klient, bilansNaStart)
        {
            this.jednorazowyLimitDebetowy = jednorazowyLimitDebetowy;
        }

        //Właściwość publiczna do odczytu oraz zapisu limitu debetowego 
        public decimal JednorazowyLimitDebetowy
        {
            get => jednorazowyLimitDebetowy;
            set
            {
                //Jeżeli wartość jest mniejsza od zera to wyrzucany jest wyjątek ArgumentException z odpowiednią informacją (limit debetowy nie może być ujemny)
                if (value < 0)
                    throw new ArgumentException("Limit debetowy nie może być ujemny.");
                jednorazowyLimitDebetowy = value;
            }
        }

        //Właściwość publiczna, która zwraca bilans konta
        public new decimal Bilans
        {
            get
            {
                //Jeżeli debetWykorzystany to zwracany jest bilans konta plus jednorazowyLimitDebetowy, w przeciwnym wypadku zwracany jest bilans konta
                if (debetWykorzystany)
                    return base.Bilans + jednorazowyLimitDebetowy;
                return base.Bilans;
            }
        }

        //Metoda do wypłacania środków z konta
        public new void Wyplata(decimal kwota)
        {
            if (Zablokowane)                                                                                //Jeżeli konto jest zablokowane to wyrzucany jest wyjątek:
                throw new InvalidOperationException("Konto jest zablokowane.");                             //Wyrzucenie wyjątku InvalidOperationException, gdy konto jest zablokowane
            if (kwota <= 0)                                                                                 //Jeżeli kwota jest mniejsza lub równa zero to wyrzucany jest wyjątek:
                throw new ArgumentException("Kwota wypłaty musi być dodatnia.");                            //Wyrzucenie wyjątku ArgumentException, gdy kwota jest mniejsza lub równa zero
            if (kwota > (Bilans + jednorazowyLimitDebetowy))                                                                             //Jeżeli kwota jest większa od bilansu to wyrzucany jest wyjątek:
                throw new InvalidOperationException("Brak wystarczających środków na koncie, do wypłaty."); //Wyrzucenie wyjątku InvalidOperationException, gdy występuje brak wystarczających środków na koncie

            base.Wyplata(kwota);

            //Jeżeli bilans konta jest mniejszy od zera to debetWykorzystany ustawiany jest na true oraz konto jest blokowane
            if (base.Bilans < 0)
            {
                debetWykorzystany = true;
                BlokujKonto();
            }
        }

        //Metoda do wpłacania środków na konto
        public new void Wplata(decimal kwota)
        {
            base.Wplata(kwota);

            //Jeżeli bilans konta jest większy od zera to debetWykorzystany ustawiany jest na false oraz konto jest odblokowane
            if (base.Bilans > 0)
            {
                debetWykorzystany = false;
                OdblokujKonto();
            }
        }

        //Metoda konwertująca konto na kontoLimit
        public Konto KonwertujNaKonto()
        {
            return new Konto(Nazwa, Bilans);
        }

    }
}