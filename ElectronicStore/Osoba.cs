using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ElectronicStore
{
    public enum EnumPlec { K, M }

    /// <summary>
    /// Klasa abstrakcyjna reprezentująca osobę.
    /// </summary>
    public abstract class Osoba : BaseModel, IEquatable<Osoba> 
    {
        public string imie;
        public string nazwisko;
        public DateTime dataUrodzenia;
        public string pesel;
        private EnumPlec plec;
        public static object EnumPlec;

        #region Properties

        /// <summary>
        /// Pobiera lub ustawia imię osoby.
        /// </summary>
        public string Imie { get => imie; set => imie = value; }

        /// <summary>
        /// Pobiera lub ustawia nazwisko osoby.
        /// </summary>
        public string Nazwisko { get => nazwisko; set => nazwisko = value; }

        /// <summary>
        /// Pobiera lub ustawia miasto osoby.
        /// </summary>
        public DateTime DataUrodzenia { get => dataUrodzenia; set => dataUrodzenia = value; }

        /// <summary>
        /// Pobiera lub ustawia numer PESEL osoby.
        /// </summary>
        public string Pesel
        {
            get => pesel;
            set
            {
                if (!Regex.IsMatch(value, @"^\d{11}$"))
                {
                    throw new ZlyNumerPESELException("Niepoprawny numer PESEL!");
                }
                pesel = value;
            }
        }

        /// <summary>
        /// Pobiera lub ustawia płeć osoby.
        /// </summary>
        public EnumPlec Plec { get => plec; set => plec = value; }

        #endregion Properties

        #region Konstruktory

        /// <summary>
        /// Inicjalizuje nowy obiekt klasy <c>Osoba</c> konstruktorem domyślnym.
        /// </summary>
        public Osoba()
        {
            Imie = string.Empty;
            Nazwisko = string.Empty;
            DataUrodzenia = DateTime.Today;
            Pesel = new string('0', 11);
        }

        /// <summary>
        /// Inicjalizuje nowy obiekt klasy <c>Osoba</c> konstruktorem parametrowym.
        /// </summary>
        /// <param name="imie">Imię osoby.</param>
        /// <param name="nazwisko">Nazwisko osoby.</param>
        /// <param name="plec">Płeć osoby.</param>
        public Osoba(string imie, string nazwisko, EnumPlec plec)
            : this()
        {
            Imie = imie;
            Nazwisko = nazwisko;
            this.Plec = plec;
        }

        /// <summary>
        /// Inicjalizuje nowy obiekt klasy <c>Osoba</c> konstruktorem parametrowym z pełnym zestawem informacji.
        /// </summary>
        /// <param name="imie">Imię osoby.</param>
        /// <param name="nazwisko">Nazwisko osoby.</param>
        /// <param name="dataUrodzenia">Data urodzenia osoby w formie tekstowej.</param>
        /// <param name="pesel">Numer PESEL osoby.</param>
        /// <param name="plec">Płeć osoby.</param>
        public Osoba(string imie, string nazwisko, string dataUrodzenia, string pesel, EnumPlec plec)
           : this(imie, nazwisko, plec)
        {
            if (DateTime.TryParseExact(dataUrodzenia, new[] { "dd-MM-yyyy", "yyyy-MM-dd", "yyyy/MM/dd", "MM/dd/yy",
                "dd-MMM-yy"}, null, DateTimeStyles.None, out DateTime date))
            { DataUrodzenia = date; }
            Pesel = pesel;
        }
        #endregion
        /// <summary>
        /// Oblicza i zwraca wiek osoby.
        /// </summary>
        /// <returns>Wiek osoby.</returns>
        public int Wiek()
        {
            return (int)(DateTime.Now - DataUrodzenia).TotalDays / 365;
        }

        /// <summary>
        /// Zwraca ciąg znaków reprezentujący osobę.
        /// </summary>
        /// <returns>Ciąg znaków reprezentujący osobę.</returns>
        public override string ToString()
        {
            return $"{Imie} {Nazwisko.ToUpper()} ({Plec}), ur. {DataUrodzenia:yyyy/MM/dd} (PESEL: {Pesel})";
        }

        /// <summary>
        /// Określa, czy bieżący obiekt jest równy innemu obiektowi tego samego typu.
        /// </summary>
        /// <param name="other">Osoba do porównania.</param>
        /// <returns>True, jeśli bieżący obiekt jest równy podanej osobie; w przeciwnym razie false.</returns>
        public bool Equals(Osoba? other)
        {
            if (other == null) { return false; }
            return Pesel.Equals(other.Pesel);
        }
    }
}
