using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rpgcs
{
    internal class Dice
    {
        Random rand;
        public Dice() 
        {
            rand = new Random();
        }

        sbyte d20()
        {
            return (sbyte)(rand.Next(1, 20));
        }
        sbyte d6()
        {
            return (sbyte)(rand.Next(1, 20));
        }
    }
}
