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


        public static Character[] Fabric(){
            return new Character[] { 
                new Character("Tank",       new Statistic(2,2,30, 8,3,4,1,2),   new Magic("Condamnation", "Protection", "Distraction")),
                new Character("Knight",     new Statistic(3,2,20,10,2,2,3,4),   new Magic("Atack", "Atack", "Atack")), //Add
                new Character("Mage",       new Statistic(1,3,15,12,1,3,4,2),   new Magic("Fire Ball", "Lightning", "Sorrow")),
                new Character("Priest",     new Statistic(1,4,15,14,1,2,3,3),   new Magic("Heal", "Blessing", "Divine Chastiment"))
            };
        }

        public override void TakeAnAction(List<Unit> queue, Dice dice)
        {
            base.TakeAnAction(queue, dice);


        }

        byte SpellFinder()
        {
            byte i = 0;
            return i;
        }
    }
}
