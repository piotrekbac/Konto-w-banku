using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
    public class KontoPlus : Konto
    {
        private decimal jednorazowyLimitDebetowy;
        private bool debetWykorzystany = false;

        public KontoPlus(string klient, decimal bilansNaStart, decimal jednorazowyLimitDebetowy) : base(klient, bilansNaStart)
        {
            this.jednorazowyLimitDebetowy = jednorazowyLimitDebetowy;
        }

        public decimal JednorazowyLimitDebetowy
        {
            get => jednorazowyLimitDebetowy;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Limit debetowy nie może być ujemny.");
                jednorazowyLimitDebetowy = value;
            }
        }

        public new decimal Bilans
        {
            get
            {
                if (debetWykorzystany)
                    return base.Bilans + jednorazowyLimitDebetowy;
                return base.Bilans;
            }
        }

        public new void Wyplata(decimal kwota)
        {
            if (Zablokowane)
                throw new InvalidOperationException("Konto jest zablokowane.");
            if (kwota <= 0)
                throw new ArgumentException("Kwota wypłaty musi być dodatnia.");
            if (kwota > Bilans) 
                throw new InvalidOperationException("Brak wystarczających środków na koncie, do wypłaty.");

            base.Wyplata(kwota);

            if (base.Bilans < 0)
            {
                debetWykorzystany = true;
                BlokujKonto();
            }
        }

        public new void Wplata(decimal kwota)
        {
            base.Wplata(kwota);

            if (base.Bilans > 0)
            {
                debetWykorzystany = false;
                OdblokujKonto();
            }

        }

    }
}
