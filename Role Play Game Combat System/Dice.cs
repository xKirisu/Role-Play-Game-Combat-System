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

        public byte d20()
        {
            return (byte)(rand.Next(1, 20));
        }
        public byte d6()
        {
            return (byte)(rand.Next(1, 20));
        }

        public byte random(int size)
        {
            return (byte)(rand.Next(1, size));
        }
    }
}
