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
            Konto kontoStandardowe = new Konto("Piotr Bacior", 1000);
            Console.WriteLine($"Utworzono konto standardowe dla: {kontoStandardowe.Nazwa}, z bilansem: {kontoStandardowe.Bilans}");
            Console.WriteLine();

            KontoPlus kontoPlus = new KontoPlus("Paweł Bacior", 2000, 500);
            Console.WriteLine($"Utworzono kontoPlus dla: {kontoPlus.Nazwa}, z bilansem: {kontoPlus.Bilans} oraz limitem debetowym: {kontoPlus.JednorazowyLimitDebetowy}");
            Console.WriteLine();




        }




    }
}