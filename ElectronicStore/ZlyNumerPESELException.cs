using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicStore
{
    /// <summary>
    /// Wyjątek rzucany w przypadku błędnego numeru PESEL.
    /// </summary>
    public class ZlyNumerPESELException : Exception
    {
        /// <summary>
        /// Inicjuje nowe wystąpienie klasy.
        /// </summary>
        public ZlyNumerPESELException()
        {

        }

        /// <summary>
        /// Inicjuje nowe wystąpienie klasy.
        /// </summary>
        /// <param name="message">Komunikat opisujący błąd.</param>
        public ZlyNumerPESELException(string message) : base(message)
        {

        }
    }
}
