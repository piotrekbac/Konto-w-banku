using System;
using Bank; 
using Microsoft.VisualStudio.TestTools.UnitTesting; //dodanie biblioteki do testowania

namespace TestyKonto
{
    [TestClass]
    public class Test1
    {
        [TestMethod]
        public void TestWplata()
        {
            //Arrange
            var konto = new Konto ("Piotr Bacior", 1000);

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
            var konto = new Konto("Piotr Bacior", 1000);

            //Act
            konto.Wplata(0);

            //Assert 
            //ExpectedException sprawdza czy wyrzucany jest wyjątek wynikający z wplaty 0
        }

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
    }
}
