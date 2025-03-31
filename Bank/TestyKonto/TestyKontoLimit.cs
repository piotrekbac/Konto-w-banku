using System;
using Bank;
using Microsoft.VisualStudio.TestTools.UnitTesting; //dodanie biblioteki do testowania

namespace TestyBanku
{
    [TestClass]

    public class TestKontoLimit
    {
        //Test sprawdzaj¹cy czy metoda Wplata zwiêksza bilans konta
        [TestMethod]
        public void TestWplata()
        {
            //Arrange
            var konto = new KontoLimit("Piotr Bacior", 1000, 500);

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
            var konto = new KontoLimit("Piotr Bacior", 1000, 500);

            //Act
            konto.Wplata(0);

            //Assert 
            //ExpectedException sprawdza czy wyrzucany jest wyj¹tek wynikaj¹cy z wplaty 0
        }

        //Test sprawdzaj¹cy czy metoda Wplata wyrzuca wyj¹tek gdy kwota jest ujemna
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestWplataUjemnaKwota()
        {
            //Arrange 
            var konto = new KontoLimit("Piotr Bacior", 1000, 500);

            //Act
            konto.Wplata(-500);

            //Assert
            //ExpectedException sprawdza czy wyrzucany jest wyj¹tek wynikaj¹cy z wplaty ujemnej kwoty
        }

        //Test sprawdzaj¹cy czy metoda Wplata wyrzuca wyj¹tek gdy konto jest zablokowane
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]

        public void TestWplataKontoZablokowane()
        {
            //Arrange
            var konto = new KontoLimit("Piotr Bacior", 1000, 500);

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
            var konto = new KontoLimit("Piotr Bacior", 1000, 500);

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
            var konto = new KontoLimit("Piotr Bacior", 1000, 500);

            //Act 
            konto.Wyplata(0);

            //Assert
            //ExpectedException sprawdza czy wyrzucany jest wyj¹tek wynikaj¹cy z wplaty 0
        }

        //Test sprawdzaj¹cy czy metoda Wyplata wyrzuca wyj¹tek, gdy kwota jest ujemna
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestWyplataUjemnaKwota()
        {
            //Arrange 
            var konto = new KontoLimit("Piotr Bacior", 1000, 500);

            //Act
            konto.Wyplata(-500);

            //Assert
            //ExpectedException sprawdza czy wyrzucany jest wyj¹tek wynikaj¹cy z wplaty ujemnej kwoty
        }

        //Test sprawdzaj¹cy czy metoda Wyplata wyrzuca wyj¹tek, gdy konto jest zablokowane
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestWyplataKontoZablokowane()
        {
            //Arrange 
            var konto = new KontoLimit("Piotr Bacior", 1000, 500);

            //Act
            konto.BlokujKonto();
            konto.Wyplata(500);

            //Assert    
            //ExpectedException sprawdza czy wyrzucany jest wyj¹tek wynikaj¹cy z wplaty na zablokowane konto
        }

        //Test sprawdzaj¹cy czy metoda Wyplata wyrzuca wyj¹tek, gdy próbujemy wyp³aciæ zbyt du¿¹ kwotê
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestWyplataNiewystarczajaceSrodki()
        {
            //Arrange
            var konto = new KontoLimit("Piotr Bacior", 1000, 500);

            //Act
            konto.Wyplata(1500);

            //Assert
            //ExpectedException sprawdza czy wyrzucany jest wyj¹tek wynikaj¹cy z próby wyp³aty zbyt du¿ej iloœci œrodków (niewystarczaj¹ce œrodki)
        }

        //Test sprawdzaj¹cy czy metoda BlokujKonto blokuje konto oraz czy metoda OdblokujKonto odblokowuje konto
        [TestMethod]
        public void TestBlokujOdblokujKonto()
        {
            //Arrange
            var konto = new KontoLimit("Piotr Bacior", 1000, 500);

            //Act oraz Assert
            konto.BlokujKonto();
            Assert.IsTrue(konto.Zablokowane);
            konto.OdblokujKonto();
            Assert.IsFalse(konto.Zablokowane);
        }

        //Test sprawdzaj¹cy czy metoda JednorazowyLimitDebetowy zwraca poprawne dane
        [TestMethod]
        public void TestJednorazowyLimitDebetowy()
        {
            //Arrange
            var konto = new KontoLimit("Piotr Bacior", 1000, 500);

            //Act
            konto.JednorazowyLimitDebetowy = 1000;

            //Assert
            Assert.AreEqual(1000, konto.JednorazowyLimitDebetowy);
        }

        //Test sprawdzaj¹cy czy metoda JednorazowyLimitDebetowyUjemny wyrzuca wyj¹tek, gdy próbujemy ustawiæ ujemny limit debetowy
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestJednorazowyLimitDebetowyUjemny()
        {
            //Arrange
            var konto = new KontoLimit("Piotr Bacior", 1000, 500);

            //Act
            konto.JednorazowyLimitDebetowy = -1000;

            //Assert
            //ExpectedException sprawdza czy wyrzucany jest wyj¹tek wynikaj¹cy z ustawienia ujemnego limitu debetowego

        }

        //Test sprawdzaj¹cy czy metoda KonwertujNaKonto zwraca poprawne dane
        [TestMethod]
        public void TestKonwertujNaKonto()
        {
            //Arrange
            var kontoLimit = new KontoLimit("Piotr Bacior", 1000, 500);

            //Act
            var konto = kontoLimit.KonwertujNaKonto();

            //Assert
            Assert.IsNotNull(konto);
            Assert.AreEqual("Piotr Bacior", konto.Nazwa);
        }

        //Test sprawdzaj¹cy czy metoda BilansZDebetem zwraca poprawne dane
        [TestMethod]
        public void TestBilansZDebetem()
        {
            // Arrange - przygotowanie danych testowych
            var konto = new KontoLimit("Piotr Bacior", 1000, 500);

            // Act - pobranie bilansu
            var bilans = konto.Bilans + konto.JednorazowyLimitDebetowy;

            // Assert - sprawdzenie wyników
            Assert.AreEqual(1500, bilans);
        }

        //Test sprawdzaj¹cy czy metoda BilansBezDebetu zwraca poprawne dane
        [TestMethod]
        public void TestBilansBezDebetu()
        {
            //Arrange
            var konto = new KontoLimit("Piotr Bacior", 1000, 500);

            //Act
            var bilans = konto.Bilans;

            //Assert
            Assert.AreEqual(1000, bilans);
        }

        //Test sprawdzaj¹cy czy metoda DebetWykorzystany zwraca poprawne dane
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