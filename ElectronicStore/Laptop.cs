using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ElectronicStore
{
    /// <summary>
    /// Reprezentuje typ systemu operacyjnego laptopa.
    /// </summary>
    public enum SystemLaptop
    {
        Windows,
        MacOS,
        Linux
    }

    /// <summary>
    /// Klasa <c>Laptop</c> reprezentująca produkt - laptop w sklepie elektronicznym.
    /// </summary>
    [Serializable]
    public class Laptop : Produkt
    {
        private float ekran;
        private int pamiecRAM;
        private int pojemnoscDysku;
        private SystemLaptop system;

        /// <summary>
        /// Pobiera lub ustawia rozmiar ekranu laptopa.
        /// </summary>
        public float Ekran { get => ekran; set => ekran = value; }

        /// <summary>
        /// Pobiera lub ustawia ilość pamięci RAM laptopa.
        /// </summary>
        public int PamiecRAM { get => pamiecRAM; set => pamiecRAM = value; }

        /// <summary>
        /// Pobiera lub ustawia pojemność dysku laptopa.
        /// </summary>
        public int PojemnoscDysku { get => pojemnoscDysku; set => pojemnoscDysku = value; }

        /// <summary>
        /// Pobiera lub ustawia system operacyjny laptopa.
        /// </summary>
        public SystemLaptop System { get => system; set => system = value; }

        /// <summary>
        /// Inicjalizuje nowy obiekt klasy <c>Laptop</c> bezparametrowo.
        /// </summary>
        public Laptop() : base()
        {

        }

        /// <summary>
        /// Inicjalizuje nowy obiekt klasy <c>Laptop</c> parametrami opisującymi laptop.
        /// </summary>
        /// <param name="ekran">Rozmiar ekranu laptopa.</param>
        /// <param name="pamiecRAM">Ilość pamięci RAM laptopa.</param>
        /// <param name="pojemnoscDysku">Pojemność dysku laptopa.</param>
        /// <param name="system">System operacyjny laptopa.</param>
        /// <param name="producent">Producent laptopa.</param>
        /// <param name="model">Model laptopa.</param>
        /// <param name="cena">Cena laptopa.</param>
        /// <param name="iloscNaStanie">Ilość dostępnych sztuk laptopa na magazynie.</param>
        /// <param name="opis">Opis laptopa.</param>
        public Laptop(float ekran, int pamiecRAM, int pojemnoscDysku, SystemLaptop system, EnumProducent producent, string model, decimal cena, int iloscNaStanie, string opis)
            : base(producent, model, cena, iloscNaStanie, opis)
        {
            this.system = system;
            this.ekran = ekran;
            this.pamiecRAM = pamiecRAM;
            this.pojemnoscDysku = pojemnoscDysku;
        }

        /// <summary>
        /// Zwraca tekstową reprezentację laptopa.
        /// </summary>
        /// <returns>Łańcuch znaków reprezentujący laptop.</returns>
        public override string ToString()
        {
            return $"Laptop - ID: {Id}, Producent: {Producent}, Model: {Model}, Cena: {Cena:c}, Ilość: {IloscNaStanie}szt., Opis: {Opis}";
        }

        /// <summary>
        /// Zwraca szczegółowe informacje o laptopie, takie jak system operacyjny, rozmiar ekranu, pamięć RAM i pojemność dysku.
        /// </summary>
        /// <returns>Łańcuch znaków reprezentujący szczegóły laptopa.</returns>
        public string WyswietlSzczegoly()
        {
            return $"System: {system}, Ekran: {ekran}\", Pamięć RAM: {pamiecRAM}GB, Pojemność dysku: {pojemnoscDysku}GB";
        }
    }

}
