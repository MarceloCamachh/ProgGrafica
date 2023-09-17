using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace progGrafica
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var game = new Game(1000, 800))
            {
                game.Run(60.0);
            }


        }
    }
}
