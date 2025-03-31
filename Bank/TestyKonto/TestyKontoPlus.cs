using System;
using System.Security.Cryptography.X509Certificates;
using Bank;
using Microsoft.VisualStudio.TestTools.UnitTesting; //dodanie biblioteki do testowania

namespace TestyBanku
{
    [TestClass]
    public class TestyKontoPlus
    {
        //Test sprawdzaj�cy czy metoda Wplata zwi�ksza bilans konta
        [TestMethod]
        public void TestWplata()
        {
            //Arrange
            var konto = new KontoPlus("Piotr Bacior", 1000, 500);

            //Act
            konto.Wplata(500);

            //Assert
            Assert.AreEqual(1500, konto.Bilans);
        }

        //Test sprawdzaj�cy czy metoda Wplata wyrzuca wyj�tek gdy kwota jest r�wna 0
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestWplataZero()
        {
            //Arrange
            var konto = new KontoPlus("Piotr Bacior", 1000, 500);

            //Act
            konto.Wplata(0);

            //Assert
            //ExpectedException sprawdza czy wyrzucany jest wyj�tek wynikaj�cy z wplaty 0
        }

        //Test sprawdzaj�cy czy metoda Wplata wyrzuca wyj�tek, gdy kwota jest ujemna
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestWplataUjemna()
        {
            //Arrange
            var konto = new KontoPlus("Piotr Bacior", 1000, 500);

            //Act
            konto.Wplata(-500);

            //Assert
            //ExpectedException sprawdza czy wyrzucany jest wyj�tek wynikaj�cy z wplaty ujemnej kwoty
        }

        //Test sprawdzaj�cy czy metoda Wplata wyrzuca wyj�tek, gdy konto jest zablokowane
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestWplataKontoZablokowane()
        {
            //Arrange
            var konto = new KontoPlus("Piotr Bacior", 1000, 500);

            //Act
            konto.BlokujKonto();
            konto.Wplata(500);

            //Assert
            //ExpectedException sprawdza czy wyrzucany jest wyj�tek wynikaj�cy z wplaty na zablokowane konto
        }

        //Test sprawdzaj�cy czy metoda Wyplata zmniejsza bilans konta
        [TestMethod]
        public void TestWyplata()
        {
            //Arrange
            var konto = new KontoPlus("Piotr Bacior", 1000, 500);

            //Act
            konto.Wyplata(500);

            //Assert
            Assert.AreEqual(500, konto.Bilans);
        }

        //Test sprawdzaj�cy czy metoda Wyplata wyrzuca wyj�tek, gdy kwota jest r�wna 0
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestWyplataZero()
        {
            //Arrange
            var konto = new KontoPlus("Piotr Bacior", 1000, 500);

            //Act
            konto.Wyplata(0);

            //Assert
            //ExpectedException sprawdza czy wyrzucany jest wyj�tek wynikaj�cy z wplaty 0
        }

        //Test sprawdzaj�cy czy metoda Wyplata wyrzuca wyj�tek, gdy kwota jest ujemna
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestWyplataUjemna()
        {
            //Arrange
            var konto = new KontoPlus("Piotr Bacior", 1000, 500);

            //Act
            konto.Wyplata(-500);

            //Assert
            //ExpectedException sprawdza czy wyrzucany jest wyj�tek wynikaj�cy z wplaty ujemnej kwoty
        }

        //Test sprawdzaj�cy czy metoda Wyplata wyrzuca wyj�tek, gdy konto jest zablokowane
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestWyplataZablokowaneKonto()
        {
            //Arrange
            var konto = new KontoPlus("Piotr Bacior", 1000, 500);

            //Act
            konto.BlokujKonto();
            konto.Wyplata(500);

            //Assert
            //ExpectedException sprawdza czy wyrzucany jest wyj�tek wynikaj�cy z wplaty na zablokowane konto
        }

        //Test sprawdzaj�cy czy metoda Wyplata wyrzuca wyj�tek, gdy brakuje �rodk�w na koncie
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestWyplataNiewystarczajaceSrodki()
        {
            //Arrange
            var konto = new KontoPlus("Piotr Bacior", 1000, 500);

            //Act
            konto.Wyplata(1501);

            //Assert
            //ExpectedException sprawdza czy wyrzucany jest wyj�tek wynikaj�cy z pr�by wyp�aty zbyt du�ej ilo�ci �rodk�w (niewystarczaj�ce �rodki)

        }

        //Test sprawdzaj�cy czy metoda BlokujKonto blokuje konto oraz czy metoda OdblokujKonto odblokowuje konto
        [TestMethod]
        public void TestBlokujOdblokujKonto()
        {
            //Arrange
            var konto = new KontoPlus("Piotr Bacior", 1000, 500);

            //Act i Assert
            konto.BlokujKonto();
            Assert.IsTrue(konto.Zablokowane);
            konto.OdblokujKonto();
            Assert.IsFalse(konto.Zablokowane);
        }

        //Test sprawdzaj�cy czy metoda JednorazowyLimitDebetowy ustawia limit debetowy
        [TestMethod]
        public void TestJednorazowyLimitDebetowy()
        {
            //Arrange
            var konto = new KontoPlus("Piotr Bacior", 1000, 500);

            //Act
            konto.JednorazowyLimitDebetowy = 600;

            //Assert
            Assert.AreEqual(600, konto.JednorazowyLimitDebetowy);
        }

        //Test sprawdzaj�cy czy metoda JednorazowyLimitDebetowyUjemny wyrzuca wyj�tek, gdy pr�bujemy ustawi� ujemny limit debetowy
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestJednorazowyLimitDebetowyUjemny()
        {
            //Arrange
            var konto = new KontoPlus("Piotr Bacior", 1000, 500);
            //Act
            konto.JednorazowyLimitDebetowy = -500;
            //Assert
            //ExpectedException sprawdza czy wyrzucany jest wyj�tek wynikaj�cy z pr�by ustawienia ujemnego limitu debetowego
        }

        //Test sprawdzaj�cy czy metoda KonwertujNaKonto zwraca poprawne dane
        [TestMethod]
        public void TestKonwertujNaKonto()
        {
            //Arrange
            var konto = new KontoPlus("Piotr Bacior", 1000, 500);

            //Act
            Konto konto2 = konto.KonwertujNaKonto();

            //Assert
            Assert.AreEqual(1000, konto2.Bilans);
        }

        //Test sprawdzaj�cy, czy flaga debetWykorzystany jest ustawiana na true oraz czy konto jest blokowane, gdy bilans jest mniejszy od zera
        [TestMethod]
        public void TestDebetWykorzystany()
        {
            //Arrange
            var konto = new KontoPlus("Piotr Bacior", 1000, 500);

            //Act
            konto.Wyplata(1200);

            //Assert
            Assert.IsTrue(konto.Zablokowane);
            Assert.AreEqual(300, konto.Bilans);
        }
    }
}