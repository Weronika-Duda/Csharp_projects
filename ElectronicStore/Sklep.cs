using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ElectronicStore
{
    [Serializable]
    [XmlInclude(typeof(Smartphone))]
    [XmlInclude(typeof(Laptop))]
    public class Sklep : BaseModel, IZapisywalnySklep
    {

        private List<Produkt> produktyNaStanie;
        private List<Pracownik> pracownicy;

        /// <summary>
        /// Pobiera lub ustawia listę produktów na stanie sklepu.
        /// </summary>
        public List<Produkt> ProduktyNaStanie { get => produktyNaStanie; set => produktyNaStanie = value; }

        /// <summary>
        /// Pobiera lub ustawia listę pracowników sklepu.
        /// </summary>
        public List<Pracownik> Pracownicy { get => pracownicy; set => pracownicy = value; }



        /// <summary>
        /// Inicjalizuje nową instancję klasy Sklep.
        /// </summary>

        public Sklep()
        {            
            produktyNaStanie = new List<Produkt>();
            pracownicy = new List<Pracownik>();
        }

        /// <summary>
        /// Dodaje pracownika do sklepu.
        /// </summary>
        /// <param name="pracownik">Pracownik do dodania.</param>

        public void DodajPracownika(Pracownik pracownik)
        {
            if(pracownik is not null && !pracownicy.Contains(pracownik))
            {
                pracownicy.Add(pracownik);
                Console.WriteLine($"Dodano pracownika: {pracownik}");

            }
        }

        /// <summary>
        /// Dodaje nowy produkt do stanu sklepu.
        /// </summary>
        /// <param name="produkt">Produkt do dodania.</param>

        public void DodajNowyProdukt(Produkt produkt)
        {
            if(produkt is not null && !produktyNaStanie.Contains(produkt))
            {
                produktyNaStanie.Add(produkt);
                Console.WriteLine($"Dodano nowy produkt do stanu sklepu: {produkt}");
            }
        }

        /// <summary>
        /// Usuwa produkt ze stanu sklepu.
        /// </summary>
        /// <param name="produkt">Produkt do usunięcia.</param>
        public void UsunProdukt(Produkt produkt)
        {
            if (produkt is not null && produktyNaStanie.Contains(produkt))
            {
                produktyNaStanie.Remove(produkt);
                Console.WriteLine($"Usunięto produkt: {produkt}");
            }
        }


        /// <summary>
        /// Zwiększa ilość danego produktu na stanie sklepu.
        /// </summary>
        /// <param name="produkt">Produkt, którego ilość ma zostać zwiększona.</param>
        /// <param name="iloscDoDodania">Ilość do dodania do aktualnej ilości produktu.</param>

        public void ZwiekszIloscProduktu(Produkt produkt, int iloscDoDodania)
        {
            if (produktyNaStanie.Contains(produkt))
            {
                produkt.IloscNaStanie += iloscDoDodania;
                Console.WriteLine($"Zwiększono ilość produktu w sklepie: {produkt.Producent} {produkt.Model}, Nowa ilość: {produkt.IloscNaStanie}");

            }
            else
            {
                Console.WriteLine($"Brak produktu na stanie.");
            }
        }

        /// <summary>
        /// Wyświetla listę produktów na stanie sklepu.
        /// </summary>

        public void WyswietlProduktyNaStanie()
        {
            Console.WriteLine("Produkty na stanie sklepu:");
            foreach (var produkt in produktyNaStanie)
            {

                Console.WriteLine(produkt.ToString());

            }
        }


        /// <summary>
        /// Realizuje zakupy na podstawie podanego paragonu, zmniejszając ilość produktów na stanie.
        /// </summary>
        /// <param name="paragon">Paragon zawierający produkty do zakupu.</param>


        public void RealizujZakupy(Paragon paragon)
        {
            if (paragon.PozycjeParagonu.All(pozycjaParagonu => produktyNaStanie.Any(p => p.Equals(pozycjaParagonu))))
            {
                Console.WriteLine("Zrealizowano zakup produktów: ");

                foreach (var pozycja in paragon.PozycjeParagonu)
                {
                    Produkt? produktWSklepie = produktyNaStanie.Find(p => p.Equals(pozycja));

                    if (produktWSklepie != null)
                    {
                        Console.WriteLine($"Produkt: {produktWSklepie.Model}, Ilość: {pozycja.IloscNaStanie}");
                        produktWSklepie.IloscNaStanie -= pozycja.IloscNaStanie;

                        if(produktWSklepie.IloscNaStanie == 0)
                        {
                            produktyNaStanie.Remove(produktWSklepie);
                        }
                    }
                }

                Console.WriteLine("Zakupione produkty zostały odebrane. Dziękujemy za zakupy!");
            }
            else
            {
                Console.WriteLine("Brak zakupów do zrealizowania");
            }
        }


        /// <summary>
        /// Zapisuje dane sklepu do pliku XML.
        /// </summary>
        /// <param name="nazwaPliku">Ścieżka do pliku XML.</param>


        public void ZapiszDoXml(string nazwaPliku)
        {
            using StreamWriter sw = new(nazwaPliku);
            XmlSerializer xs = new(typeof(Sklep));
            xs.Serialize(sw, this);
        }


        /// <summary>
        /// Odczytuje dane sklepu z pliku XML.
        /// </summary>
        /// <param name="nazwaPliku">Ścieżka do pliku XML.</param>
        /// <returns>Obiekt Sklep odczytany z pliku XML lub null, jeśli plik nie istnieje.</returns>

        public static Sklep? OdczytajZXml(string nazwaPliku)
        {
            if (!File.Exists(nazwaPliku))
                return null;
            using StreamReader sw = new(nazwaPliku);
            XmlSerializer xs = new(typeof(Sklep));
            return xs.Deserialize(sw) as Sklep;
        }

        /// <summary>
        /// Zapisuje dane sklepu do bazy danych.
        /// </summary>
        public void ZapiszDoBazy(SklepDbContext dbContext)
        {
            // Spróbuj odnaleźć sklep w bazie danych
            var existingSklep = dbContext.Sklepy.FirstOrDefault();
            if (existingSklep != null)
            {
                existingSklep.Pracownicy = this.Pracownicy;
                existingSklep.ProduktyNaStanie = this.ProduktyNaStanie;
            }
            else
            {
                // Sklep nie istnieje, dodaj nowy
                dbContext.Add(this);
            }

            dbContext.SaveChanges();
            Console.WriteLine("Zapisano do bazy danych."); // stosowny komunikat
        }

        /// <summary>
        /// Odczytuje dane sklepu z bazy danych.
        /// </summary>
        /// <returns>Obiekt Sklep odczytany z bazy danych lub null, jeśli sklep o podanym ID nie istnieje.</returns>

        public static Sklep OdczytajZBazy(SklepDbContext dbContext)
        {
            int? sklepid = dbContext.Sklepy.Max(s => s.Id);
            Sklep s = new Sklep();


            var sklep =
                dbContext.Sklepy.Include(s => s.ProduktyNaStanie)
                         .Include(s => s.Pracownicy)
                         .FirstOrDefault(s => s.Id == sklepid);

            if (sklep != null)
            {
                Console.WriteLine("Odczytano z bazy danych.");
                return sklep;
            }
            else
            {
                Console.WriteLine("Nie znaleziono sklepu w bazie danych.");
                return null;
            }
        }


        /// <summary>
        /// Wyszukuje produkty na stanie sklepu według podanego typu.
        /// </summary>
        /// <param name="typProduktu">Typ produktu, według którego mają być wyszukane produkty.</param>
        /// <returns>Lista produktów danego typu na stanie sklepu.</returns>


        public List<Produkt> WyszukajProduktyWedlugTypu(Type typProduktu)
        {
            return ProduktyNaStanie.Where(p => typProduktu.IsAssignableFrom(p.GetType())).ToList();
        }


    }
}
