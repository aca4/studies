using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantsGuideToGalaxy
{
    class Program
    {
        /// <summary>
        /// Entry point. Input file path should be passed as argument
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string filePath = args[0];
            InputParser parser = new InputParser();
            parser.Translate(filePath);

            Console.WriteLine("Press any key to close");
            Console.ReadLine();
        }
    }
}
