using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ElectronicStore
{
    public enum EnumProducent
    {
        Acer,
        Apple,
        Asus,
        Dell,
        HP,
        Motorola,
        Lenovo,
        Samsung,
        Xiaomi,
    }

    /// <summary>
    /// Reprezentuje abstrakcyjną klasę Produkt, dziedziczącą po interfejsach IEquatable i ICloneable.
    /// </summary>

    [Serializable]
    public abstract class Produkt : BaseModel, IEquatable<Produkt>, ICloneable
    {

        EnumProducent producent;
        private string model;
        public string opis;
        private decimal cena;
        private int iloscNaStanie;


        /// <summary>
        /// Pobiera lub ustawia model produktu.
        /// </summary>
        
        public string Model { get => model; set => model = value; }

        /// <summary>
        /// Pobiera lub ustawia cenę produktu.
        /// </summary>
        public decimal Cena { get => cena; set => cena = value; }
       
        /// <summary>
        /// Pobiera lub ustawia ilość produktu na stanie.
        /// </summary>
        public int IloscNaStanie { get => iloscNaStanie; set => iloscNaStanie = value; }

        /// <summary>
        /// Pobiera lub ustawia opis produktu.
        /// </summary>

        public string Opis { get => opis; set => opis = value; }

        /// <summary>
        /// Pobiera lub ustawia producenta produktu.
        /// </summary>
        public EnumProducent Producent { get => producent; set => producent = value; }


        public Produkt()
        {

        }

        /// <summary>
        /// Inicjalizuje nową instancję klasy Produkt z podanymi danymi.
        /// </summary>
        /// <param name="producent">Producent produktu.</param>
        /// <param name="model">Model produktu.</param>
        /// <param name="cena">Cena produktu.</param>
        /// <param name="iloscNaStanie">Ilość produktu na stanie.</param>
        /// <param name="opis">Opis produktu.</param>


        public Produkt(EnumProducent producent, string model, decimal cena, int iloscNaStanie, string opis) 
            : this()
        {
            this.producent = producent;
            this.model = model;
            this.cena = cena;
            this.iloscNaStanie = iloscNaStanie;
            this.opis = opis;
        }

        /// <summary>
        /// Zwraca tekstową reprezentację obiektu Produkt.
        /// </summary>
        /// <returns>Łańcuch znaków reprezentujący produkt.</returns>

        public override string ToString()
        {
            return $"Producent: {producent}, Model: {model}, Cena: {cena:m2}, Ilość: {iloscNaStanie}szt., Opis: {opis}";
        }

        /// <summary>
        /// Określa, czy bieżący obiekt jest równy innemu obiektowi tego samego typu.
        /// </summary>
        /// <param name="other">Inny obiekt do porównania z tym obiektem.</param>
        /// <returns>
        /// true, jeśli bieżący obiekt jest równy podanemu obiektowi; w przeciwnym razie false.
        /// </returns>

        public bool Equals(Produkt? other)
        {
            if(other is null)
            {
                return false;
            }
            if (this.Producent.Equals(other.Producent))
            {
                return this.Model.Equals(other.Model);
            }
            return false;
        }

        /// <summary>
        /// Tworzy nowy obiekt, który jest kopią bieżącego wystąpienia.
        /// </summary>
        /// <returns>Nowy obiekt, który jest kopią bieżącego wystąpienia.</returns>
        public object Clone()
        {
            return MemberwiseClone(); 
        }
    }
}
