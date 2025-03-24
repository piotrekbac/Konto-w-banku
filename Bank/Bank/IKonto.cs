using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
    public interface IKonto
    {
        string Nazwa { get; }
        decimal Bilans { get; }
        bool Zablokowane { get; }
        void Wplata(decimal kwota);
        void Wyplata(decimal kwota);
        void BlokujKonto();
        void OdblokujKonto();

    }
}
