using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ElectronicStore
{
    /// <summary>
    /// Reprezentuje system operacyjny telefonu.
    /// </summary>
    public enum SystemTel
    {
        Android,
        iOS
    }

    /// <summary>
    /// Delegat dla metody zwracającej ciąg znaków.
    /// </summary>
    /// <returns>Ciąg znaków.</returns>
    public delegate string Output();

    /// <summary>
    /// Klasa reprezentująca smartfon.
    /// </summary>
    [Serializable]
    [XmlInclude(typeof(Smartphone))]
    public class Smartphone : Produkt, ICloneable
    {
        private float wyswietlacz;
        private int pamiecRAM;
        private int pamiecWbudowana;
        private int pojemnoscAkumulatora;
        private int aparatTylny;
        private int aparatPrzedni;
        private SystemTel system;

        public float Wyswietlacz { get => wyswietlacz; set => wyswietlacz = value; }
        public int PamiecRAM { get => pamiecRAM; set => pamiecRAM = value; }
        public int PamiecWbudowana { get => pamiecWbudowana; set => pamiecWbudowana = value; }
        public int PojemnoscAkumulatora { get => pojemnoscAkumulatora; set => pojemnoscAkumulatora = value; }
        public int AparatTylny { get => aparatTylny; set => aparatTylny = value; }
        public int AparatPrzedni { get => aparatPrzedni; set => aparatPrzedni = value; }
        public SystemTel System { get => system; set => system = value; }

        /// <summary>
        /// Inicjalizuje nową instancję klasy <see cref="Smartphone"/>.
        /// </summary>
        public Smartphone() : base()
        {

        }

        /// <summary>
        /// Inicjalizuje nową instancję klasy <see cref="Smartphone"/> z określonymi parametrami.
        /// </summary>
        /// <param name="wyswietlacz">Wielkość wyświetlacza w calach.</param>
        /// <param name="pamiecRAM">Pamięć RAM w gigabajtach.</param>
        /// <param name="pamiecWbudowana">Pamięć wbudowana w gigabajtach.</param>
        /// <param name="pojemnoscAkumulatora">Pojemność akumulatora w mAh.</param>
        /// <param name="aparatTylny">Rozdzielczość aparatu tylnego w megapikselach.</param>
        /// <param name="aparatPrzedni">Rozdzielczość aparatu przedniego w megapikselach.</param>
        /// <param name="system">System operacyjny telefonu.</param>
        /// <param name="producent">Producent smartfona.</param>
        /// <param name="model">Model smartfona.</param>
        /// <param name="cena">Cena smartfona.</param>
        /// <param name="iloscNaStanie">Ilość dostępnych sztuk smartfona.</param>
        /// <param name="opis">Opis smartfona.</param>
        public Smartphone(float wyswietlacz, int pamiecRAM, int pamiecWbudowana, int pojemnoscAkumulatora, int aparatTylny, int aparatPrzedni, SystemTel system, EnumProducent producent, string model, decimal cena, int iloscNaStanie, string opis)
            : base(producent, model, cena, iloscNaStanie, opis)
        {
            this.wyswietlacz = wyswietlacz;
            this.pamiecRAM = pamiecRAM;
            this.pamiecWbudowana = pamiecWbudowana;
            this.pojemnoscAkumulatora = pojemnoscAkumulatora;
            this.aparatTylny = aparatTylny;
            this.aparatPrzedni = aparatPrzedni;
            this.system = system;
        }

        /// <summary>
        /// Generuje ciąg znaków reprezentujący szczegóły smartfona.
        /// </summary>
        /// <returns>Ciąg znaków reprezentujący szczegóły smartfona.</returns>
        public string WyswietlSzczegoly()
        {
            return $"System: {system}, Wyświetlacz: {wyswietlacz}\", Pamięć RAM: {pamiecRAM}GB, Pamięć wbudowana: {pamiecWbudowana}GB, Pojemność akumulatora: {pojemnoscAkumulatora}mAh, Aparat tylny: {aparatTylny}mpx, Aparat przedni: {aparatPrzedni}mpx";
        }

        /// <summary>
        /// Metoda generująca ciąg znaków reprezentujący informacje o smartfonie.
        /// </summary>
        /// <returns>Ciąg znaków reprezentujący informacje o smartfonie.</returns>
        public string GenerateOutput()
        {
            Output outputDelegate = () =>
            {
                return $"Smartphone - ID: {Id}, Producent: {Producent}, Model: {Model}, Cena: {Cena:c}, Ilość: {IloscNaStanie}szt., Opis: {Opis}";
            };

            return outputDelegate.Invoke();
        }

        /// <summary>
        /// Przesłonięta metoda ToString zwracająca informacje o smartfonie.
        /// </summary>
        /// <returns>Informacje o smartfonie.</returns>
        public override string ToString()
        {
            return GenerateOutput();
        }

        /// <summary>
        /// Metoda służąca do klonowania obiektu <see cref="Smartphone"/>.
        /// </summary>
        /// <returns>Nowa instancja sklonowanego smartfona.</returns>
        public object Clone()
        {
            return new Smartphone(
                wyswietlacz,
                pamiecRAM,
                pamiecWbudowana,
                pojemnoscAkumulatora,
                aparatTylny,
                aparatPrzedni,
                system,
                Producent,
                Model,
                Cena,
                IloscNaStanie,
                Opis
            );
        }
    }
}