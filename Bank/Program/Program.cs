using System;
using static System.Net.Mime.MediaTypeNames;
using Bank;
using System.Reflection.Metadata;

namespace Bank
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("--------------------------------------------------------------------------------------------------");
            Konto kontoStandardowe = new Konto("Piotr Bacior", 1000);
            Console.WriteLine($"Utworzono konto standardowe dla: {kontoStandardowe.Nazwa}, z bilansem: {kontoStandardowe.Bilans}");
            Console.WriteLine();

            KontoPlus kontoPlus = new KontoPlus("Paweł Bacior", 2000, 500);
            Console.WriteLine($"Utworzono kontoPlus dla: {kontoPlus.Nazwa}, z bilansem: {kontoPlus.Bilans} oraz limitem debetowym: {kontoPlus.JednorazowyLimitDebetowy}");
            Console.WriteLine();

            KontoLimit kontoLimit = new KontoLimit("Stanisław Bacior", 3000, 1000);
            Console.WriteLine($"Utworzono kontoLimit dla: {kontoLimit.Nazwa}, z bilansem: {kontoLimit.Bilans} oraz limitem debetowym: {kontoLimit.JednorazowyLimitDebetowy}");

            Console.WriteLine("--------------------------------------------------------------------------------------------------");

            try
            {
                Console.WriteLine("Operacje na standardowym koncie: ");
                kontoStandardowe.Wplata(300);
                Console.WriteLine($"Bilans konta po wplacie 300: {kontoStandardowe.Bilans}");
                kontoStandardowe.Wyplata(100);
                Console.WriteLine($"Bilans konta po wyplacie 100: {kontoStandardowe.Bilans}");
                kontoStandardowe.BlokujKonto();
                Console.WriteLine($"Czy konto jest zablokowane (po zablokowaniu): {kontoStandardowe.Zablokowane}");
                kontoStandardowe.OdblokujKonto();
                Console.WriteLine($"Czy konto jest zablokowane (po odblokowaniu): {kontoStandardowe.Zablokowane}");
            }
            catch (Exception blad)
            {
                Console.WriteLine($"Błąd: {blad.Message}");
            }

            Console.WriteLine("--------------------------------------------------------------------------------------------------");

            try
            {
                Console.WriteLine("Operacje na kontoPlus: ");
                kontoPlus.Wplata(500);
                Console.WriteLine($"Bilans konta po wplacie 500: {kontoPlus.Bilans}");
                kontoPlus.Wyplata(300);
                Console.WriteLine($"Bilans konta po wyplacie 300: {kontoPlus.Bilans}");
                kontoPlus.BlokujKonto();
                Console.WriteLine($"Czy kontoPlus jest zablokowane? (po zablokowaniu): {kontoPlus.Zablokowane}");
                kontoPlus.OdblokujKonto();
                Console.WriteLine($"Czy kontoPlus jest zablokowane? (po odblokowaniu): {kontoPlus.Zablokowane}");
                Console.WriteLine($"Aktualny bilans kontoPlus: {kontoPlus.Bilans}");
            }
            catch(Exception blad)
            {
                Console.WriteLine($"Błąd: {blad.Message}");
            }

            try
            {
                Console.WriteLine("\nPróba wypłaty kwoty większej niż bilans na koncie KontoPlus:");
                kontoPlus.Wyplata(2300);
                Console.WriteLine($"Bilans po wypłacie 2300: {kontoPlus.Bilans}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Błąd: {e.Message}");
            }

            try
            {
                Console.WriteLine("\nWpłata w celu odblokowania konta KontoPlus:");
                kontoPlus.Wplata(2500);
                Console.WriteLine($"Bilans po wpłacie 2500: {kontoPlus.Bilans}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Błąd: {e.Message}");
            }

            try
            {
                Console.WriteLine("\nPróba ponownej wypłaty po odblokowaniu konta KontoPlus:");
                kontoPlus.Wyplata(2100);
                Console.WriteLine($"Bilans po wypłacie 2100: {kontoPlus.Bilans}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Błąd: {e.Message}");
            }

            Console.WriteLine("--------------------------------------------------------------------------------------------------");


            try
            {
                Console.WriteLine("Operacje na kontoLimit: ");
                kontoLimit.Wplata(1000);
                Console.WriteLine($"Bilans konta po wpłacie 1000: {kontoLimit.Bilans}");
                kontoLimit.Wyplata(500);
                Console.WriteLine($"Bilans konta po wyplacie 500: {kontoLimit.Bilans}");
                kontoLimit.BlokujKonto();
                Console.WriteLine($"Czy kontoLimit jest zablokowane? (po zablokowaniu): {kontoLimit.Zablokowane}");
                kontoLimit.OdblokujKonto();
                Console.WriteLine($"Czy kontoLimit jest zablokowane? (po odblokowaniu): {kontoLimit.Zablokowane}");
                Console.WriteLine($"Aktualny bilans kontoLimit (wraz wliczonym z limitem debetowym): {kontoLimit.Bilans}");
            }
            catch (Exception blad)
            {
                Console.WriteLine($"Błąd {blad.Message}");
            }

            try
            {
                Console.WriteLine("\nPróba wypłaty kwoty większej niż bilans na koncie KontoLimit (5000):");
                kontoLimit.Wyplata(5000);
                Console.WriteLine($"Bilans po wypłacie 5000: {kontoLimit.Bilans}");
            }

            catch (Exception e)
            {
                Console.WriteLine($"Błąd: {e.Message}");
            }

            try
            {
                Console.WriteLine("\nWpłata w celu odblokowania konta KontoLimit:");
                kontoLimit.Wplata(6000);
                Console.WriteLine($"Bilans po wpłacie 6000: {kontoLimit.Bilans}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Błąd: {e.Message}");
            }

            try
            {
                Console.WriteLine("\nPróba ponownej wypłaty po odblokowaniu konta KontoLimit:");
                kontoLimit.Wyplata(5000);
                Console.WriteLine($"Bilans po wypłacie 5000: {kontoLimit.Bilans}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Błąd: {e.Message}");
            }

            Console.WriteLine("--------------------------------------------------------------------------------------------------");

        }
    }
}