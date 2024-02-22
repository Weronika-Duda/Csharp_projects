using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicStore
{
    /// <summary>
    /// Interfejs <c>IZapisywalnySklep</c> definiujący operacje zapisywania obiektu Sklep do pliku XML.
    /// </summary>
    public interface IZapisywalnySklep
    {
        /// <summary>
        /// Zapisuje obiekt Sklep do pliku XML o podanej nazwie.
        /// </summary>
        /// <param name="nazwaPliku">Nazwa pliku XML, do którego ma zostać zapisany obiekt Sklep.</param>
        void ZapiszDoXml(string nazwaPliku);
    }

}
