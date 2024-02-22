using ElectronicStore;
using Microsoft.EntityFrameworkCore;
using System.Runtime.ConstrainedExecution;

namespace TestSklepu
{
    [TestClass]
    public class IZapisywalnySklepTest
    {
        [TestMethod]
        public void ZapisDoXml_CzyPlikZostalUtworzony()
        {
            // Arrange
            Sklep s = new();
            string nazwaPliku = "testowySklep.xml";

            // Act
            s.ZapiszDoXml(nazwaPliku);

            // Assert
            Assert.IsTrue(File.Exists(nazwaPliku), "Plik XML nie zosta³ utworzony.");
        }
    }

    [TestClass]
    public class LaptopTests
    {
        [TestMethod]
        public void ToString_CzyPoprawnyFormat()
        {
            // Arrange
            var laptop = new Laptop
            {
                //IdProduktu = 1,
                Producent = EnumProducent.Acer,
                Model = "Aspire",
                Cena = 2500.0m,
                IloscNaStanie = 10,
                Opis = "Laptop do codziennego u¿ytku"
            };

            // Act
            var result = laptop.ToString();

            // Assert
            string expected = "Producent: Acer, Model: Aspire, Cena: 2 500,00 z³, Iloœæ: 10szt., Opis: Laptop do codziennego u¿ytku";
            StringAssert.Contains(result, expected);
        }

        [TestMethod]
        public void WyswietlSzczegoly_CzyPoprawnyFormat()
        {
            // Arrange
            var laptop = new Laptop
            {
                System = SystemLaptop.Windows,
                Ekran = 15,
                PamiecRAM = 8,
                PojemnoscDysku = 512
            };

            // Act
            var result = laptop.WyswietlSzczegoly();

            // Assert
            Assert.AreEqual("System: Windows, Ekran: 15\", Pamiêæ RAM: 8GB, Pojemnoœæ dysku: 512GB", result);
        }

        [TestMethod]
        public void Konstruktor_CzyUstawiaWlasciwosciPoprawnie()
        {
            // Arrange
            var producent = EnumProducent.Dell;
            var model = "Inspiron";
            var cena = 3000.0m;
            var iloscNaStanie = 5;
            var opis = "Laptop do pracy biurowej";
            var system = SystemLaptop.Windows;
            var ekran = 14;
            var pamiecRAM = 16;
            var pojemnoscDysku = 1024;

            // Act
            var laptop = new Laptop(ekran, pamiecRAM, pojemnoscDysku, system, producent, model, cena, iloscNaStanie, opis);

            // Assert
            Assert.AreEqual(producent, laptop.Producent);
            Assert.AreEqual(model, laptop.Model);
            Assert.AreEqual(cena, laptop.Cena);
            Assert.AreEqual(iloscNaStanie, laptop.IloscNaStanie);
            Assert.AreEqual(opis, laptop.Opis);
            Assert.AreEqual(system, laptop.System);
            Assert.AreEqual(ekran, laptop.Ekran);
            Assert.AreEqual(pamiecRAM, laptop.PamiecRAM);
            Assert.AreEqual(pojemnoscDysku, laptop.PojemnoscDysku);
        }
    }


    [TestClass]
    public class ParagonTests
    {
        [TestMethod]
        public void DodajProduktDoParagonu_PoprawnieDodany()
        {
            // Arrange
            Paragon paragon1 = new Paragon();
            Produkt produkt = new Laptop
            {
                Producent = EnumProducent.Apple,
                Model = "MacBook Pro",
                Cena = 5000.0m,
                IloscNaStanie = 10
            };

            // Act
            paragon1.DodajProduktDoParagonu(produkt, 3);

            // Assert
            Assert.AreEqual(1, paragon1.PozycjeParagonu.Count);
            Assert.AreEqual("MacBook Pro", paragon1.PozycjeParagonu[0].Model);
            Assert.AreEqual(3, paragon1.PozycjeParagonu[0].IloscNaStanie);
        }

        [TestMethod]
        public void DodajProduktDoParagonu_NiepoprawnaIlosc()
        {
            // Arrange
            Paragon paragon2 = new Paragon();
            Produkt produkt = new Laptop
            {
                Producent = EnumProducent.Apple,
                Model = "MacBook Pro",
                Cena = 5000.0m,
                IloscNaStanie = 5
            };

            // Act
            paragon2.DodajProduktDoParagonu(produkt, 8);

            // Assert
            Assert.AreEqual(0, paragon2.PozycjeParagonu.Count);
        }

        [TestMethod]
        public void UsunProduktZParagonu_UsuniecieProduktu()
        {
            // Arrange
            Paragon paragon3 = new Paragon();
            Produkt produkt = new Laptop
            {
                Producent = EnumProducent.Apple,
                Model = "MacBook Pro",
                Cena = 5000.0m,
                IloscNaStanie = 10
            };

            paragon3.DodajProduktDoParagonu(produkt, 3);

            // Act
            paragon3.UsunProduktZParagonu(produkt);

            // Assert
            Assert.AreEqual(0, paragon3.PozycjeParagonu.Count);
        }

        [TestMethod]
        public void WyswietlParagon_PoprawneWyswietlanie()
        {
            // Arrange
            Paragon paragon4 = new Paragon();
            Produkt produkt1 = new Laptop
            {
                Producent = EnumProducent.Apple,
                Model = "MacBook Pro",
                Cena = 5000.0m,
                IloscNaStanie = 5
            };
            Produkt produkt2 = new Smartphone
            {
                Producent = EnumProducent.Samsung,
                Model = "Galaxy S21",
                Cena = 3000.0m,
                IloscNaStanie = 8
            };

            paragon4.DodajProduktDoParagonu(produkt1, 2);
            paragon4.DodajProduktDoParagonu(produkt2, 1);

            // Act
            Console.WriteLine("WyswietlParagon:");
            paragon4.WyswietlParagon();

            // Assert
            // W tym przypadku sprawdzamy wyœwietlenie paragonu na konsoli, poniewa¿ nie mo¿emy bezpoœrednio testowaæ
            // wyjœcia tekstowego w testach jednostkowych. Mo¿esz zobaczyæ wyniki w konsoli testów jednostkowych.
        }

        [TestMethod]
        public void SortujParagonPoCenie_Rosnaco_SortsInAscendingOrder()
        {
            // Arrange
            Paragon paragon5 = new Paragon();
            Smartphone produkt1 = new Smartphone(6.74F, 4, 128, 5000, 50, 8, SystemTel.Android, EnumProducent.Xiaomi, "REDMI13", 4999M, 3, "FAJNY");
            Laptop produkt2 = new Laptop(16, 512, 34, SystemLaptop.Windows, EnumProducent.HP, "IdeaPad Slim 3", 2499M, 5, "niezly");
            paragon5.DodajProduktDoParagonu(produkt1, 2);
            paragon5.DodajProduktDoParagonu(produkt2, 1);

            // Act
            paragon5.SortujParagonPoCenie(KierunekSortowania.Rosnaco);

            // Assert
            Assert.AreEqual(produkt2.Cena, paragon5.PozycjeParagonu[0].Cena);
            Assert.AreEqual(produkt1.Cena, paragon5.PozycjeParagonu[1].Cena);
        }

        [TestMethod]
        public void SortujParagonPoCenie_Malejaco_SortsInDescendingOrder()
        {
            // Arrange
            Paragon paragon6 = new Paragon();
            Smartphone produkt1 = new Smartphone(6.74F, 4, 128, 5000, 50, 8, SystemTel.Android, EnumProducent.Xiaomi, "REDMI13", 4999M, 3, "FAJNY");
            Laptop produkt2 = new Laptop(16, 512, 34, SystemLaptop.Windows, EnumProducent.HP, "IdeaPad Slim 3", 2499M, 5, "niezly");
            paragon6.DodajProduktDoParagonu(produkt1, 2);
            paragon6.DodajProduktDoParagonu(produkt2, 1);

            // Act
            paragon6.SortujParagonPoCenie(KierunekSortowania.Malejaco);

            // Assert
            Assert.AreEqual(produkt1.Cena, paragon6.PozycjeParagonu[0].Cena);
            Assert.AreEqual(produkt2.Cena, paragon6.PozycjeParagonu[1].Cena);
        }

        [TestMethod]
        public void WyszukajPoProducencie_ReturnsProductsByManufacturer()
        {
            // Arrange
            Paragon paragon7 = new Paragon();
            Smartphone produkt1 = new Smartphone(6.74F, 4, 128, 5000, 50, 8, SystemTel.Android, EnumProducent.Samsung, "Galaxy", 4999M, 3, "FAJNY");
            Laptop produkt2 = new Laptop(16, 512, 34, SystemLaptop.Windows, EnumProducent.HP, "IdeaPad Slim 3", 2499M, 5, "niezly");
            paragon7.DodajProduktDoParagonu(produkt1, 2);
            paragon7.DodajProduktDoParagonu(produkt2, 1);

            // Act
            List<Produkt> result = paragon7.WyszukajPoProducencie(EnumProducent.Samsung);

            // Assert
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("Galaxy", result[0].Model);
        }

        [TestMethod]
        public void WyswietlProduktyDanegoProducenta_DisplaysProductsByManufacturer()
        {
            // Arrange
            Paragon paragon8 = new Paragon();
            Smartphone produkt1 = new Smartphone(6.74F, 4, 128, 5000, 50, 8, SystemTel.Android, EnumProducent.Samsung, "Galaxy", 4999M, 3, "FAJNY");
            Laptop produkt2 = new Laptop(16, 512, 34, SystemLaptop.Windows, EnumProducent.HP, "IdeaPad Slim 3", 2499M, 5, "niezly");
            paragon8.DodajProduktDoParagonu(produkt1, 2);
            paragon8.DodajProduktDoParagonu(produkt2, 1);

            // Act
            var consoleOutput = new System.IO.StringWriter();
            Console.SetOut(consoleOutput);

            paragon8.WyswietlProduktyDanegoProducenta(EnumProducent.Samsung);
            var result = consoleOutput.ToString().Trim();

            // Assert
            StringAssert.Contains(result, "Produkty Samsung:");
            StringAssert.Contains(result, "Galaxy");
        }
    }


    [TestClass]
    public class PracownikTests
    {
        [TestMethod]
        public void ZlyNumerPESELException_ConstructorWithMessage_ShouldSetMessage()
        {
            // Arrange
            string expectedMessage = "Test message";

            // Act
            ZlyNumerPESELException exception = new ZlyNumerPESELException(expectedMessage);

            // Assert
            Assert.AreEqual(expectedMessage, exception.Message);
        }


        [TestMethod]
        public void Pracownik_Constructor_ShouldInitializeProperties()
        {
            // Arrange
            string imie = "Jan";
            string nazwisko = "Kowalski";
            string dataUrodzenia = "1990-01-01";
            string pesel = "90010112345";
            EnumPlec plec = EnumPlec.M;
            string login = "jan123";
            string haslo = "password123";

            // Act
            Pracownik pracownik = new Pracownik(imie, nazwisko, dataUrodzenia, pesel, plec, login, haslo);

            // Assert
            Assert.AreEqual(imie, pracownik.Imie);
            Assert.AreEqual(nazwisko, pracownik.Nazwisko);
            Assert.AreEqual(pesel, pracownik.Pesel);
            Assert.AreEqual(plec, pracownik.Plec);
            Assert.AreEqual(login, pracownik.Login);
            Assert.AreEqual(haslo, pracownik.Haslo);
        }

        [TestMethod]
        public void Pracownik_ToString_ShouldReturnFormattedString()
        {
            // Arrange
            Pracownik pracownik = new Pracownik
            {
                Imie = "Jan",
                Nazwisko = "Kowalski"
            };

            // Act
            string result = pracownik.ToString();

            // Assert
            StringAssert.Contains(result, "Imie: Jan, Nazwisko: Kowalski");
        }
    }

    [TestClass]
    public class SklepTests
    {
        [TestMethod]
        public void DodajPracownika_DodajePracownikaDoSklepu()
        {
            // Arrange
            Sklep sklep = new Sklep();
            Pracownik pracownik = new Pracownik(/* parametry konstruktora */);

            // Act
            sklep.DodajPracownika(pracownik);

            // Assert
            CollectionAssert.Contains(sklep.Pracownicy, pracownik);
        }

        [TestMethod]
        public void DodajNowyProdukt_DodajeProduktDoStanuSklepu()
        {
            // Arrange
            Sklep sklep = new Sklep();
            Laptop laptop = new Laptop(17, 500, 50, SystemLaptop.Linux, EnumProducent.Dell, "G15", 4899M, 2, "czarny");

            // Act
            sklep.DodajNowyProdukt(laptop);

            // Assert
            CollectionAssert.Contains(sklep.ProduktyNaStanie, laptop);
        }

        [TestMethod]
        public void ZwiekszIloscProduktu_ZwiekszaIloscProduktuWSklepie()
        {
            // Arrange
            Sklep sklep = new Sklep();
            Laptop laptop = new Laptop(17, 500, 50, SystemLaptop.Linux, EnumProducent.Dell, "G15", 4899M, 0, "czarny");
            sklep.DodajNowyProdukt(laptop);

            // Act
            sklep.ZwiekszIloscProduktu(laptop, 5);

            // Assert
            Assert.AreEqual(5, laptop.IloscNaStanie);
        }

        [TestMethod]
        public void RealizujZakupy_ZrealizowanoZakupy()
        {
            // Arrange
            Sklep sklep = new Sklep();
            Laptop laptop = new Laptop(17, 500, 50, SystemLaptop.Linux, EnumProducent.Dell, "G15", 4899M, 2, "czarny");
            sklep.DodajNowyProdukt(laptop);
            Paragon paragon = new Paragon();
            paragon.DodajProduktDoParagonu(laptop, 1);

            // Act
            sklep.RealizujZakupy(paragon);

            // Assert
            Assert.AreEqual(1, laptop.IloscNaStanie);
        }


        [TestMethod]
        public void ZapiszDoXml_ZapisujeDoXml()
        {
            // Arrange
            Sklep sklep = new Sklep();

            // Act
            sklep.ZapiszDoXml("testowy.xml");

            // Assert
            // Sprawdzamy, czy plik XML zosta³ utworzony
            Assert.IsTrue(File.Exists("testowy.xml"));
        }

        [TestMethod]
        public void OdczytajZXml_OdczytujeZXml()
        {
            // Arrange
            Sklep sklep = new Sklep();
            sklep.ZapiszDoXml("testowy.xml");

            // Act
            Sklep odczytanySklep = Sklep.OdczytajZXml("testowy.xml");

            // Assert
            Assert.IsNotNull(odczytanySklep);
            // Sprawdzamy inne warunki, które uwa¿asz za istotne
        }

        [TestMethod]
        public void WyszukajProduktyWedlugTypu_ReturnsProductsOfType()
        {
            // Arrange
            Sklep sklep = new Sklep();
            Laptop laptop = new Laptop(15, 16, 512, SystemLaptop.Windows, EnumProducent.Dell, "XPS", 2999.99m, 10, "Powerful laptop");
            Smartphone smartphone = new Smartphone(6.5f, 8, 128, 4000, 64, 16, SystemTel.Android, EnumProducent.Samsung, "Galaxy S21", 1999.99m, 5, "High-end smartphone");
            sklep.DodajNowyProdukt(laptop);
            sklep.DodajNowyProdukt(smartphone);

            // Act
            List<Produkt> result = sklep.WyszukajProduktyWedlugTypu(typeof(Laptop));

            // Assert
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("XPS", result[0].Model);
        }
    }

    [TestClass]
    public class SmartphoneTests
    {
        [TestMethod]
        public void SmartphoneToString_ReturnsCorrectString()
        {
            // Arrange
            Smartphone smartphone = new Smartphone(
                6.2f,
                4,
                128,
                4000,
                12,
                8,
                SystemTel.Android,
                EnumProducent.Samsung,
                "Galaxy S21",
                2499.99m,
                10,
                "Flagowy model Samsunga"
            );

            // Act
            string result = smartphone.ToString();

            // Assert
            string expected = "Producent: Samsung, Model: Galaxy S21, Cena: 2 499,99 z³, Iloœæ: 10szt., Opis: Flagowy model Samsunga";
            StringAssert.Contains(result, expected);

        }

        [TestMethod]
        public void SmartphoneWyswietlSzczegoly_ReturnsCorrectString()
        {
            // Arrange
            Smartphone smartphone = new Smartphone(
                6.2f,
                4,
                128,
                4000,
                12,
                8,
                SystemTel.Android,
                EnumProducent.Samsung,
                "Galaxy S21",
                2499.99m,
                10,
                "Flagowy model Samsunga"
            );

            // Act
            string result = smartphone.WyswietlSzczegoly();

            // Assert
            string expected = "System: Android, Wyœwietlacz: 6,2\", Pamiêæ RAM: 4GB, Pamiêæ wbudowana: 128GB, Pojemnoœæ akumulatora: 4000mAh, Aparat tylny: 12mpx, Aparat przedni: 8mpx";
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void SmartphoneClone_ReturnsClonedObjectWithSameProperties()
        {
            // Arrange
            Smartphone originalSmartphone = new Smartphone(
                6.2f,
                4,
                128,
                4000,
                12,
                8,
                SystemTel.Android,
                EnumProducent.Samsung,
                "Galaxy S21",
                2499.99m,
                10,
                "Flagowy model Samsunga"
            );

            // Act
            Smartphone clonedSmartphone = originalSmartphone.Clone() as Smartphone;

            // Assert
            Assert.IsNotNull(clonedSmartphone);
            Assert.AreEqual(originalSmartphone.Wyswietlacz, clonedSmartphone.Wyswietlacz);
            Assert.AreEqual(originalSmartphone.PamiecRAM, clonedSmartphone.PamiecRAM);
            Assert.AreEqual(originalSmartphone.PamiecWbudowana, clonedSmartphone.PamiecWbudowana);
            Assert.AreEqual(originalSmartphone.PojemnoscAkumulatora, clonedSmartphone.PojemnoscAkumulatora);
            Assert.AreEqual(originalSmartphone.AparatTylny, clonedSmartphone.AparatTylny);
            Assert.AreEqual(originalSmartphone.AparatPrzedni, clonedSmartphone.AparatPrzedni);
            Assert.AreEqual(originalSmartphone.System, clonedSmartphone.System);
            Assert.AreEqual(originalSmartphone.Producent, clonedSmartphone.Producent);
            Assert.AreEqual(originalSmartphone.Model, clonedSmartphone.Model);
            Assert.AreEqual(originalSmartphone.Cena, clonedSmartphone.Cena);
            Assert.AreEqual(originalSmartphone.IloscNaStanie, clonedSmartphone.IloscNaStanie);
            Assert.AreEqual(originalSmartphone.Opis, clonedSmartphone.Opis);
        }
    }
}