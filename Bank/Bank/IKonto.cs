using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
    //Interfejs IKonto
    public interface IKonto
    {
        //Właściwość Nazwa, która zwraca nazwę klienta
        string Nazwa { get; }
        //Właściwość Bilans, która zwraca bilans konta
        decimal Bilans { get; }
        //Właściwość Zablokowane, która zwraca stan zablokowania konta
        bool Zablokowane { get; }

        //Metoda Wplata, która pozwala na wpłacenie środków na konto
        void Wplata(decimal kwota);
        //Metoda Wyplata, która pozwala na wypłacenie środków z konta
        void Wyplata(decimal kwota);
        //Metoda BlokujKonto, która blokuje konto
        void BlokujKonto();
        //Metoda OdblokujKonto, która odblokowuje konto
        void OdblokujKonto();

    }
}
