using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rpgcs
{
    internal class Character : Unit
    {
        public Character(string name, Statistic atributes, Magic spellbook) : base(name, atributes, spellbook) { }


        Character[] Fabric(){
            return new Character[] { 
                new Character("Tank",   new Statistic(2,2,30, 8,3,4,1,2), new Magic("Atack", "Atack", "Atack")),
                new Character("Knight", new Statistic(3,2,20,10,2,2,3,4), new Magic("Atack", "Atack", "Atack")),
                new Character("Tank",   new Statistic(1,3,15,12,1,3,4,2), new Magic("Atack", "Atack", "Atack")),
                new Character("Tank",   new Statistic(1,4,15,14,1,2,3,3), new Magic("Atack", "Atack", "Atack"))
            };
        }

        public override void TakeAnAction(List<Unit> queue)
        {
            base.TakeAnAction(queue);


        }
    }
}
