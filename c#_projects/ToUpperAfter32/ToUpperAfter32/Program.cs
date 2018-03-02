using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToUpperAfter32
{
    class Program
    {
        static void Transform(ref string sentence)
        {
            var s = sentence.ToCharArray();
            s[0] = char.ToUpper(s[0]);

            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == ' ') s[i + 1] = ((s[i + 1] >= 97) && (s[i + 1] <= 122)) ? (char)(s[i + 1] - 32) : s[i + 1];
            }

            sentence = string.Concat(s);
            sentence = sentence.Replace(' ', (char)7F); 
        }
        static void Main(string[] args)
        {
            string @string = "ASD 123 fgh jkl";
            Console.WriteLine(@string);

            Transform(ref @string);
            Console.WriteLine(@string);

            Console.ReadKey();
        }
    }
}
