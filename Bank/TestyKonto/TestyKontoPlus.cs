using System;
using System.Security.Cryptography.X509Certificates;
using Bank;
using Microsoft.VisualStudio.TestTools.UnitTesting; //dodanie biblioteki do testowania

namespace TestyBanku
{
    [TestClass]
    public class TestyKontoPlus
    {
        //Test sprawdzaj¹cy czy metoda Wplata zwiêksza bilans konta
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

        //Test sprawdzaj¹cy czy metoda Wplata wyrzuca wyj¹tek gdy kwota jest równa 0
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestWplataZero()
        {
            //Arrange
            var konto = new KontoPlus("Piotr Bacior", 1000, 500);

            //Act
            konto.Wplata(0);

            //Assert
            //ExpectedException sprawdza czy wyrzucany jest wyj¹tek wynikaj¹cy z wplaty 0
        }

        //Test sprawdzaj¹cy czy metoda Wplata wyrzuca wyj¹tek, gdy kwota jest ujemna
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestWplataUjemna()
        {
            //Arrange
            var konto = new KontoPlus("Piotr Bacior", 1000, 500);

            //Act
            konto.Wplata(-500);

            //Assert
            //ExpectedException sprawdza czy wyrzucany jest wyj¹tek wynikaj¹cy z wplaty ujemnej kwoty
        }

        //Test sprawdzaj¹cy czy metoda Wplata wyrzuca wyj¹tek, gdy konto jest zablokowane
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
            //ExpectedException sprawdza czy wyrzucany jest wyj¹tek wynikaj¹cy z wplaty na zablokowane konto
        }

        //Test sprawdzaj¹cy czy metoda Wyplata zmniejsza bilans konta
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

        //Test sprawdzaj¹cy czy metoda Wyplata wyrzuca wyj¹tek, gdy kwota jest równa 0
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestWyplataZero()
        {
            //Arrange
            var konto = new KontoPlus("Piotr Bacior", 1000, 500);

            //Act
            konto.Wyplata(0);

            //Assert
            //ExpectedException sprawdza czy wyrzucany jest wyj¹tek wynikaj¹cy z wplaty 0
        }

        //Test sprawdzaj¹cy czy metoda Wyplata wyrzuca wyj¹tek, gdy kwota jest ujemna
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestWyplataUjemna()
        {
            //Arrange
            var konto = new KontoPlus("Piotr Bacior", 1000, 500);

            //Act
            konto.Wyplata(-500);

            //Assert
            //ExpectedException sprawdza czy wyrzucany jest wyj¹tek wynikaj¹cy z wplaty ujemnej kwoty
        }

        //Test sprawdzaj¹cy czy metoda Wyplata wyrzuca wyj¹tek, gdy konto jest zablokowane
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
            //ExpectedException sprawdza czy wyrzucany jest wyj¹tek wynikaj¹cy z wplaty na zablokowane konto
        }

        //Test sprawdzaj¹cy czy metoda Wyplata wyrzuca wyj¹tek, gdy brakuje œrodków na koncie
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestWyplataNiewystarczajaceSrodki()
        {
            //Arrange
            var konto = new KontoPlus("Piotr Bacior", 1000, 500);

            //Act
            konto.Wyplata(1501);

            //Assert
            //ExpectedException sprawdza czy wyrzucany jest wyj¹tek wynikaj¹cy z próby wyp³aty zbyt du¿ej iloœci œrodków (niewystarczaj¹ce œrodki)

        }

        //Test sprawdzaj¹cy czy metoda BlokujKonto blokuje konto oraz czy metoda OdblokujKonto odblokowuje konto
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

        //Test sprawdzaj¹cy czy metoda JednorazowyLimitDebetowy ustawia limit debetowy
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

        //Test sprawdzaj¹cy czy metoda JednorazowyLimitDebetowyUjemny wyrzuca wyj¹tek, gdy próbujemy ustawiæ ujemny limit debetowy
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestJednorazowyLimitDebetowyUjemny()
        {
            //Arrange
            var konto = new KontoPlus("Piotr Bacior", 1000, 500);
            //Act
            konto.JednorazowyLimitDebetowy = -500;
            //Assert
            //ExpectedException sprawdza czy wyrzucany jest wyj¹tek wynikaj¹cy z próby ustawienia ujemnego limitu debetowego
        }

        //Test sprawdzaj¹cy czy metoda KonwertujNaKonto zwraca poprawne dane
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

        //Test sprawdzaj¹cy, czy flaga debetWykorzystany jest ustawiana na true oraz czy konto jest blokowane, gdy bilans jest mniejszy od zera
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