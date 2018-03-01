using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace overall_testing
{
    class Program
    {
        static int Factorial(int x)
        {
            if (x == 0) return 1;
            return x * Factorial(x - 1);
        }
        static void Main(string[] args)
        {
            /*  No implicit conversion from default double to float/decimal

              float f = 4.5F;
              decimal m = -1.23M; */

            /* char type can be converted to any unsigned type. shows ASCII number
            
            char c = 'c';
            ushort ch = c;
            Console.Write(ch); */

            /* string escaped = "First Line\r\nSecond Line";
             string verbatim = @"First Line
 Second Line";
             // True if your IDE uses CR-LF line separators:
             Console.WriteLine(escaped == verbatim);
             Console.ReadKey(); */

            /* interpolated strings can hold variables etc.
            string s = $"255 in hex is {byte.MaxValue:X2}"; // X2 = 2-digit Hexadecimal
                                                            // Evaluates to "255 in hex is FF"
            */

            Console.WriteLine(Factorial(10));

            Console.ReadKey();
        }
    }
}
