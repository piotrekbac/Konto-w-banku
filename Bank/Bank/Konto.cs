namespace Bank
{
    public class Konto
    {
        private string klient;                      //nazwa klienta
        private decimal bilans;                     //aktualny stan środków na koncie 
        private bool zablokowane = false;           //stan konta 

        //private void UtworzKonto() { }            //metoda tworząca konto w podkroku 3 kroku 1 z treści - niepotrzebna
        public Konto(string klient, decimal bilansNaStart = 0)
        {
            this.klient = klient;
            this.bilans = bilansNaStart;
        }
            
        public string Nazwa => klient;
        public decimal Bilans => bilans;
        public bool Zablokowane => zablokowane;

        public void Wplata(decimal kwota)
        {
            if (kwota <= 0)
                throw new ArgumentException("Kwota wpłaty musi być dodatnia.");
            if (zablokowane)
                throw new InvalidOperationException("Konto jest zablokowane.");
            bilans += kwota;
        }

        public void Wyplata(decimal kwota)
        {
            if (kwota <= 0)
                throw new ArgumentException("Kwota wypłaty musi być dodatnia.");
            if (kwota > bilans)
                throw new InvalidOperationException("Brak wystarczających środków na koncie, do wypłaty.");
            if (zablokowane)
                throw new InvalidOperationException("Konto jest zablokowane.");
            bilans -= kwota;
        }

        public void BlokujKonto()
        {
            zablokowane = true;
        }

        public void OdblokujKonto()
        {
            zablokowane = false;   
        }


    }

}
