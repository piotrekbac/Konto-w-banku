namespace Bank
{
    public class Konto
    {
        private string klient;              //nazwa klienta
        private decimal bilans;             //aktualny stan środków na koncie 
        private bool zablokowane = false;           //stan konta 

        //private void UtworzKonto() { }    //metoda tworząca konto w podkroku 3 kroku 1 z treści - niepotrzebna
        public Konto(string klient, decimal bilansNaStart = 0)
        {
            this.klient = klient;
            this.bilans = bilansNaStart;
        }
            
        public string Nazwa => klient;
        public decimal Bilans => bilans;
        public bool Zablokowane => zablokowane;


    }

}
