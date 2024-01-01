using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rpgcs
{
    internal class Enemy : Unit
    {
        Enemy(string name, Statistic atributes, Magic spellbook) : base(name, atributes, spellbook){ }
        Enemy(Enemy enemy) : base(enemy.name, enemy.Atributes, enemy.Spellbook){}

        sbyte AI()
        {
            sbyte id = 0;
            return id;
        }
        public override void TakeAnAction(List<Unit> queue)
        {
            base.TakeAnAction(queue);


        }
    }
}
