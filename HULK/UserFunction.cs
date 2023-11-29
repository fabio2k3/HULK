using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HULK
{
    public class UserFunction
    {
        public string Name;
        public List<Token> FunBody;
        public List<string> ParamNames;
        public static List<UserFunction> UserFunctions = new();



        public UserFunction(string name, List<string> paramNames, List<Token> funBody)
        {
            Name = name;
            FunBody = funBody;
            ParamNames = paramNames;
        }
        // Agrega la funcion a la lista USerFunctions
        public static void AddFunction(UserFunction f)
        {
            UserFunction? uf = FindFunction(f.Name);
            if (uf == null)
                UserFunctions.Add(f);
        }

        // Bsucar la funcion en UserFunctions
        public static UserFunction? FindFunction(string funName)
        {
            return UserFunctions.Find(x => x.Name == funName);
        }
    }
}