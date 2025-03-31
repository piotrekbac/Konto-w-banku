using System;
using Bank;
using Microsoft.VisualStudio.TestTools.UnitTesting; //dodanie biblioteki do testowania

namespace TestyBanku
{
    [TestClass]
    public class TestKonto
    {
        //Test sprawdzający czy metoda Wplata zwiększa bilans konta
        [TestMethod]
        public void TestWplata()
        {
            //Arrange
            var konto = new Konto("Piotr Bacior", 1000);

            //Act
            konto.Wplata(500);

            //Assert
            Assert.AreEqual(1500, konto.Bilans);
        }

        //Test sprawdzający czy metoda Wplata wyrzuca wyjątek gdy kwota jest równa 0
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestWplataZero()
        {
            //Arrange 
            var konto = new Konto("Piotr Bacior", 1000);

            //Act
            konto.Wplata(0);

            //Assert 
            //ExpectedException sprawdza czy wyrzucany jest wyjątek wynikający z wplaty 0
        }

        //Test sprawdzający czy metoda Wplata wyrzuca wyjątek, gdy kwota jest ujemna
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestWplataUjemnaKwota()
        {
            //Arrange 
            var konto = new Konto("Piotr Bacior", 1000);

            //Act
            konto.Wplata(-500);

            //Assert
            //ExpectedException sprawdza czy wyrzucany jest wyjątek wynikający z wplaty ujemnej kwoty
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]

        //Test sprawdzający czy metoda Wplata wyrzuca wyjątek gdy konto jest zablokowane
        public void TestWplataKontoZablokowane()
        {
            //Arrange
            var konto = new Konto("Piotr Bacior", 1000);

            //Act
            konto.BlokujKonto();
            konto.Wplata(500);

            //Assert
            //ExpectedException sprawdza czy wyrzucany jest wyjątek wynikający z wplaty na zablokowane konto
        }

        //Test sprawdzający czy metoda Wyplata zmniejsza bilans konta
        [TestMethod]
        public void TestWyplata()
        {
            //Arrange 
            var konto = new Konto("Piotr Bacior", 1000);

            //Act
            konto.Wyplata(500);

            //Assert
            Assert.AreEqual(500, konto.Bilans);
        }

        //Test sprawdzający czy metoda Wyplata wyrzuca wyjątek gdy kwota jest równa 0
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestWyplataZero()
        {
            //Arrange
            var konto = new Konto("Piotr Bacior", 1000);

            //Act 
            konto.Wyplata(0);

            //Assert
            //ExpectedException sprawdza czy wyrzucany jest wyjątek wynikający z wplaty 0
        }

        //Test sprawdzający czy metoda Wyplata wyrzuca wyjątek, gdy kwota jest ujemna
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestWyplataUjemnaKwota()
        {
            //Arrange 
            var konto = new Konto("Piotr Bacior", 1000);

            //Act
            konto.Wyplata(-500);

            //Assert
            //ExpectedException sprawdza czy wyrzucany jest wyjątek wynikający z wplaty ujemnej kwoty
        }

        //Test sprawdzający czy metoda Wyplata wyrzuca wyjątek, gdy konto jest zablokowane
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestWyplataKontoZablokowane()
        {
            //Arrange 
            var konto = new Konto("Piotr Bacior", 1000);

            //Act
            konto.BlokujKonto();
            konto.Wyplata(500);

            //Assert    
            //ExpectedException sprawdza czy wyrzucany jest wyjątek wynikający z wplaty na zablokowane konto
        }

        //Test sprawdzający czy metoda Wyplata wyrzuca wyjątek, gdy próbujemy wypłacić zbyt dużą kwotę
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestWyplataNiewystarczajaceSrodki()
        {
            //Arrange
            var konto = new Konto("Piotr Bacior", 1000);

            //Act
            konto.Wyplata(1500);

            //Assert
            //ExpectedException sprawdza czy wyrzucany jest wyjątek wynikający z próby wypłaty zbyt dużej ilości środków (niewystarczające środki)
        }

        //Test sprawdzający czy metoda BlokujKonto oraz OdblokujKonto działają poprawnie
        [TestMethod]
        public void TestBlokujOdblokujKonto()
        {
            //Arrange
            var konto = new Konto("Piotr Bacior", 1000);

            //Act oraz Assert
            konto.BlokujKonto();
            Assert.IsTrue(konto.Zablokowane);
            konto.OdblokujKonto();
            Assert.IsFalse(konto.Zablokowane);
        }

        //Test sprawdzający czy metoda KonwertujNaKontoPlus zwraca obiekt klasy KontoPlus
        [TestMethod]
        public void TestKonwertujNaKontoLimit()
        {
            //Arrange
            var konto = new Konto("Piotr Bacior", 1000);

            //Act
            var kontoPlus = konto.KonwertujNaKontoLimit(500);

            //Assert
            Assert.AreEqual("Piotr Bacior", kontoPlus.Nazwa);
            Assert.AreEqual(1000, kontoPlus.Bilans);
            Assert.AreEqual(500, kontoPlus.JednorazowyLimitDebetowy);
        }

        //Test sprawdzający czy metoda KonwertujNaKontoPlus zwraca poprawne dane
        [TestMethod]
        public void TestKonwertujNaKontoPlus()
        {
            //Arrange
            var konto = new Konto("Piotr Bacior", 1000);

            //Act
            var kontoPlus = konto.KonwertujNaKontoPlus(500);

            //Assert
            Assert.AreEqual("Piotr Bacior", kontoPlus.Nazwa);
            Assert.AreEqual(1000, kontoPlus.Bilans);
            Assert.AreEqual(500, kontoPlus.JednorazowyLimitDebetowy);
        }
    }
}