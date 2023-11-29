using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HULK
{
    class KeyWord : Node
    {
        public static string[] Keywords = { "let", "in", "function" };

        //Toma el valor de la palabra clave 
        private string _value;
        //Devuelve el valor de _value
        public string value => _value;
        public KeyWord(string value) => _value = value;
    }
}
