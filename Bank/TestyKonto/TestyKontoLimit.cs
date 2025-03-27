using System;
using Bank;
using Microsoft.VisualStudio.TestTools.UnitTesting; //dodanie biblioteki do testowania

namespace TestyBanku
{
    [TestClass]
    public class TestKontoLimit
    {
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

        [TestMethod]
        public void TestKonwertujNaKonto()
        {
            //Arrange
            var konto = new KontoLimit("Piotr Bacior", 1000, 500);

            //Act
            Konto konto2 = konto.KonwertujNaKonto();

            //Assert
            Assert.AreEqual(konto.Nazwa, konto2.Nazwa);
            Assert.AreEqual(konto.Bilans, konto2.Bilans);
            Assert.AreEqual(konto.Zablokowane, konto2.Zablokowane);
        }
    }

}
