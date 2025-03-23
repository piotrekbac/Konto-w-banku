using System;
using System.Security.Cryptography.X509Certificates;
using Bank;
using Microsoft.VisualStudio.TestTools.UnitTesting; //dodanie biblioteki do testowania

namespace TestyBanku
{
    [TestClass]
    public class TestyKontoPlus
    {
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

        [TestMethod]
        [ExpectedException (typeof(InvalidOperationException))]
        public void TestWyplataNiewystarczajaceSrodki()
        {
            //Arrange
            var konto = new KontoPlus("Piotr Bacior", 1000, 500);

            //Act
            konto.Wyplata(1500);

            //Assert
            //ExpectedException sprawdza czy wyrzucany jest wyj¹tek wynikaj¹cy z próby wyp³aty zbyt du¿ej iloœci œrodków (niewystarczaj¹ce œrodki)

        }

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

    }

}
