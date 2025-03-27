namespace Bank
{
    
    public class Konto : IKonto
    {
        private string klient;                      //nazwa klienta
        private decimal bilans;                     //aktualny stan środków na koncie 
        private bool zablokowane = false;           //stan konta 

        //private void UtworzKonto() { }            //metoda tworząca konto w podkroku 3 kroku 1 z treści - niepotrzebna

        //Konstruktor, który inicjuje nazwę klienta i początkowy bilans konta
        public Konto(string klient, decimal bilansNaStart = 0)
        {
            this.klient = klient;
            this.bilans = bilansNaStart;
        }
            
        //Właściwości tylko do odczytu (ReadOnly), które zwracają nazwę klienta, bilans oraz stan zablokowania konta
        public string Nazwa => klient;
        public decimal Bilans => bilans;
        public bool Zablokowane => zablokowane;

        //Metoda do wpłacania środków na konto
        public void Wplata(decimal kwota)
        {
            if (kwota <= 0)                                                          //Jeżeli kwota nie jest dodatnia to wyrzucany jest wyjątek:
                throw new ArgumentException("Kwota wpłaty musi być dodatnia.");      //Wyrzucenie wyjątku ArgumentException, gdy kwota wpłaty nie jest dodatnia
            if (zablokowane)                                                         //Jeżeli konto jest zablokowane to wyrzucany jest wyjątek:
                throw new InvalidOperationException("Konto jest zablokowane.");      //Wyrzucenie wyjątku InvalidOperationException, gdy konto jest zablokowane   
            bilans += kwota;                                                         //Dodanie kwoty do bilansu
        }

        //Metoda do wypłacania środków z konta
        public void Wyplata(decimal kwota)
        {
            if (kwota <= 0)                                                                                  //Jeżeli kwota nie jest dodatnia to wyrzucany jest wyjątek:
                throw new ArgumentException("Kwota wypłaty musi być dodatnia.");                             //Wyrzucenie wyjątku ArgumentException, gdy kwota wpłaty nie jest dodatnia    
            if (kwota > bilans)                                                                              //Jeżeli kwota jest większa od bilnasu to wyrzucany jest wyjątek: 
                throw new InvalidOperationException("Brak wystarczających środków na koncie, do wypłaty.");  //Wyrzucenie wyjątku InvalidOperationException, gdy występuje brak wystarczających środków do wypłaty
            if (zablokowane)                                                                                 //Jeżeli konto jest zablokowane to wyrzucany jest wyjątek:
                throw new InvalidOperationException("Konto jest zablokowane.");                              //Odjęcie kwoty od bilansu
            bilans -= kwota;
        }

        //Metoda do blokowania konta
        public void BlokujKonto()
        {
            zablokowane = true;
        }

        //Metoda do odblokowania konta
        public void OdblokujKonto()
        {
            zablokowane = false;   
        }

        //Metoda konwertująca konto na kontoPlus
        public KontoPlus KonwertujNaKontoPlus(decimal limitDebetowy)
        {
            return new KontoPlus(klient, bilans, limitDebetowy);
        }

        //Metoda konwertująca konto na kontoLimit
        public KontoLimit KonwertujNaKontoLimit(decimal limitDebetowy)
        {
            return new KontoLimit(klient,bilans, limitDebetowy);
        }

    }

}
