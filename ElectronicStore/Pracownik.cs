using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicStore
{
    /// <summary>
    /// Reprezentuje pracownika sklepu, dziedziczącego po klasie Osoba.
    /// </summary>
    public class Pracownik : Osoba, IComparable<Pracownik>
    {
        static int nr;

        string login;
        string haslo;

        /// <summary>
        /// Pobiera lub ustawia unikalny numer identyfikacyjny pracownika.
        /// </summary>
        public static int Nr { get; set; }


        /// <summary>
        /// Pobiera lub ustawia login pracownika.
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Pobiera lub ustawia hasło pracownika.
        /// </summary>
        public string Haslo { get; set; }

        /// <summary>
        /// Inicjalizuje statyczne pola klasy Pracownik.
        /// </summary>
        static Pracownik()
        {
            nr = 1;
        }

        public Pracownik()
        {

        }

        /// <summary>
        /// Inicjalizuje nową instancję klasy Pracownik z podanymi danymi.
        /// </summary>
        /// <param name="imie">Imię pracownika.</param>
        /// <param name="nazwisko">Nazwisko pracownika.</param>
        /// <param name="dataUrodzenia">Data urodzenia pracownika.</param>
        /// <param name="pesel">PESEL pracownika.</param>
        /// <param name="plec">Płeć pracownika.</param>
        /// <param name="login">Login pracownika.</param>
        /// <param name="haslo">Hasło pracownika.</param>
        public Pracownik(string imie, string nazwisko, string dataUrodzenia, string pesel, EnumPlec plec, string login, string haslo)
            : base(imie, nazwisko, dataUrodzenia, pesel, plec)
        {
            Login = login;
            Haslo = haslo;
            Nr++;
        }

        /// <summary>
        /// Zwraca tekstową reprezentację obiektu Pracownik.
        /// </summary>
        /// <returns>Łańcuch znaków reprezentujący pracownika.</returns>
        public override string ToString()
        {
            return $"ID: {Nr}, Imię: {Imie}, Nazwisko: {Nazwisko}";
        }

        public int CompareTo(Pracownik? other)
        {
            if(other == null)
            {
                return 1;
            }
            int cmp = this.Nazwisko.CompareTo(other.Nazwisko);
            if (cmp ==0){
                cmp = this.Imie.CompareTo(other.Imie);
            }
            return cmp;
        }
    }
}
