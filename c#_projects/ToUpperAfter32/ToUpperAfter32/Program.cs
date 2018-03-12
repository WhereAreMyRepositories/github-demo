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

        static void Re(StringBuilder text)
        {
            for (int i = 0; i < text.Length; i++)
            {
                if ((text[i] == ' ') && (i != text.Length - 1) && (char.IsLetter(text[i + 1])))
                {
                    text[i + 1] = char.ToUpper(text[i + 1]);
                    text.Remove(i, 1); // or text.Replace(" ", string.Empty);  ?
                }
            }
        }
        static void Main(string[] args)
        {
            /////////////// Format1 //////////////////////////////////////////////////////////////////////////////////////
            string @string = "ASD 123 fgh jkl";
            Console.WriteLine(@string);

            Transform(ref @string);
            Console.WriteLine(@string);

            /////////////// Format2 ////////////////////////////////////////////////////////////////////

            var text = new StringBuilder("I love reading technical docs.");
            Console.WriteLine(text);
            Re(text);
            Console.WriteLine(text);


            Console.ReadKey();
        }
    }
}
