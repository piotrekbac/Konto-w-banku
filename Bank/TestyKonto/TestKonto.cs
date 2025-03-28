﻿using System;
using Bank; 
using Microsoft.VisualStudio.TestTools.UnitTesting; //dodanie biblioteki do testowania

namespace TestyBanku
{
    [TestClass]
    public class TestKonto
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
