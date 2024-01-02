using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rpgcs
{
    internal class Enemy : Unit
    {
        public Enemy(string name, Statistic atributes, Magic spellbook) : base(name, atributes, spellbook) { }
        public Enemy(Enemy enemy) : base(enemy.name, enemy.Atributes, enemy.Spellbook) { }

        public static Enemy[] Fabric()
        {
            return new Enemy[]
            {
                new Enemy("Slime",      new Statistic(2,2,30, 8,3,4,1,2), new Magic("Atack", "Regenerate Mucus", "Toxic Ooze")),
                new Enemy("Potato",     new Statistic(2,2,30, 8,3,4,1,2), new Magic("Atack", "Perish", "Atack")),
                new Enemy("Goblin",     new Statistic(2,2,30, 8,3,4,1,2), new Magic("Atack", "Swat", "Atack Order")),
                new Enemy("Ice mage",   new Statistic(2,2,30, 8,3,4,1,2), new Magic("Ice Shard", "Hail", "Black Tome")),
                new Enemy("Reaper",     new Statistic(2,2,30, 8,3,4,1,2), new Magic("Unshod Shadow", "Deathly Reap", "Virgin Fate")),
                new Enemy("Trickster",  new Statistic(2,2,30, 8,3,4,1,2), new Magic("Atack", "Trick", "Toxic Dagger"))
            };
        }

        sbyte AI()
        {
            sbyte id = 0;
            return id;
        }
        public override void TakeAnAction(List<Unit> queue, Dice dice)
        {
            base.TakeAnAction(queue, dice);


        }

        public static void EnemiesNameCorrector(Enemy[] bestiary, Enemy[] enemies)
        {
            Dictionary<string, int> enemy_counter = new Dictionary<string, int>();

            foreach (Enemy enemy in bestiary)
            {
                enemy_counter.Add(enemy.name, 0);
            }

            foreach (Enemy enemy in enemies)
            {
                if (enemy_counter.ContainsKey(enemy.name))
                {
                    string e_index = enemy.name;
                    enemy_counter[enemy.name] += 1;


                    enemy.name += "0";
                    enemy.name += enemy_counter[e_index];

                }
                else
                {
                    enemy_counter.Add(enemy.name, 1);
                }
            }

        }
    }
}