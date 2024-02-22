using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ElectronicStore
{
    /// <summary>
    /// Reprezentuje kierunek sortowania paragonu.
    /// </summary>
    [Serializable]
    public enum KierunekSortowania
    {
        /// <summary>
        /// Sortowanie rosnąco.
        /// </summary>
        Rosnaco,

        /// <summary>
        /// Sortowanie malejąco.
        /// </summary>
        Malejaco
    }

    /// <summary>
    /// Reprezentuje paragon składający się z listy pozycji (produktów) oraz metody do zarządzania nimi.
    /// </summary>
    public class Paragon
    {
        /// <summary>
        /// Pobiera lub ustawia listę pozycji paragonu.
        /// </summary>
        public List<Produkt> PozycjeParagonu { get; set; }

        /// <summary>
        /// Inicjalizuje nową instancję klasy Paragon.
        /// </summary>
        public Paragon()
        {
            PozycjeParagonu = new List<Produkt>();
        }

        /// <summary>
        /// Dodaje produkt do paragonu w określonej ilości.
        /// </summary>
        /// <param name="produkt">Produkt do dodania.</param>
        /// <param name="ilosc">Ilość produktu.</param>
        public void DodajProduktDoParagonu(Produkt produkt, int ilosc)
        {
            Produkt kopieProduktu = (Produkt)produkt.Clone();
            if (produkt.IloscNaStanie >= ilosc)
            {
                kopieProduktu.IloscNaStanie = ilosc;
                PozycjeParagonu.Add(kopieProduktu);
                Console.WriteLine($"Dodano produkt do paragonu: {kopieProduktu.Producent} {kopieProduktu.Model}, Ilość: {kopieProduktu.IloscNaStanie}");
            }
            else
            {
                Console.WriteLine($"Nie można dodać produktu w podanej ilości do paragonu. Maksymalna ilość: {produkt.IloscNaStanie}");
            }
        }

        /// <summary>
        /// Usuwa produkt z paragonu.
        /// </summary>
        /// <param name="produkt">Produkt do usunięcia.</param>
        public void UsunProduktZParagonu(Produkt produkt)
        {
            if (produkt is not null)
            {
                PozycjeParagonu.Remove(produkt);
            }
        }

        /// <summary>
        /// Wyświetla zawartość paragonu.
        /// </summary>
        public void WyswietlParagon()
        {
            Console.WriteLine("Paragon:");
            foreach (var produkt in PozycjeParagonu)
            {
                Console.WriteLine(produkt.ToString());
            }
        }

        /// <summary>
        /// Sortuje pozycje paragonu według ceny.
        /// </summary>
        /// <param name="kierunek">Kierunek sortowania.</param>
        public void SortujParagonPoCenie(KierunekSortowania kierunek)
        {
            switch (kierunek)
            {
                case KierunekSortowania.Rosnaco:
                    PozycjeParagonu.Sort((p1, p2) => p1.Cena.CompareTo(p2.Cena));
                    break;
                case KierunekSortowania.Malejaco:
                    PozycjeParagonu.Sort((p1, p2) => p2.Cena.CompareTo(p1.Cena));
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Wyszukuje pozycje paragonu według producenta.
        /// </summary>
        /// <param name="wybranyProducent">Producent do wyszukania.</param>
        /// <returns>Lista produktów danego producenta w paragonie.</returns>
        public List<Produkt> WyszukajPoProducencie(EnumProducent wybranyProducent)
        {
            return PozycjeParagonu.OfType<Produkt>().Where(p => p.Producent == wybranyProducent).ToList();
        }

        /// <summary>
        /// Wyświetla produkty danego producenta z paragonu.
        /// </summary>
        /// <param name="wybranyProducent">Producent do wyświetlenia.</param>
        public void WyswietlProduktyDanegoProducenta(EnumProducent wybranyProducent)
        {
            Console.WriteLine($"Produkty {wybranyProducent}:");
            List<Produkt> produktyDanegoProducenta = WyszukajPoProducencie(wybranyProducent);

            foreach (var produkt in produktyDanegoProducenta)
            {
                Console.WriteLine(produkt);
            }
        }
    }
}
