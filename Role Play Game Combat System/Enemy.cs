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

        void AI(List<Unit> queue, Dice dice)
        {
            byte i = 0;
            byte id = (byte)(dice.random(4) - 1);
            string name = "Atack";

            // Getspell
            foreach (KeyValuePair<string, Spell> spell in Spellbook.spellslot)
            {
                if(id == i)
                {
                    name = spell.Value.name;
                    break;
                }
                i++;
            }

            // Mana checker
            if(Spellbook.spellslot[name].cost > this.Atributes.mana)
            {
                id = 0;
            }
            else
            {
                this.Atributes.mana -= Spellbook.spellslot[name].cost;
            }

            // Target select
            if (Spellbook.spellslot[name].is_offensive)
            {
                List<Unit> targets_list = new List<Unit>();

                foreach (Unit target in queue)
                {
                    if (target is Character)
                    {
                        if (target.Status == Status.Distracting)
                        {
                            Magic.Cast(this, target, Spellbook.spellslot[name], dice.d6(), dice.d20());
                            break;
                        }
                        else
                        {
                            targets_list.Add(target);
                        }
                    }
                }

                Magic.Cast(this, targets_list[dice.random(targets_list.Count) - 1], Spellbook.spellslot[name], dice.d6(), dice.d20());
            }
            else
            {
                List<Unit> targets_list = new List<Unit>();

                foreach (Unit target in queue)
                {
                    if (target is Enemy)
                    {
                        targets_list.Add(target);
                    }
                }

                Magic.Cast(this, targets_list[dice.random(targets_list.Count) - 1], Spellbook.spellslot[name], dice.d6(), dice.d20());
            }
        }
        public override void TakeAnAction(List<Unit> queue, Dice dice)
        {
            base.TakeAnAction(queue, dice);
            AI(queue, dice);
            PressToContinue();
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

            foreach (Enemy e in bestiary)
            {

                if (enemy_counter[e.name] == 1)
                {
                    string correct = e.name;
                    string one = e.name;
                    one += "01";
                    foreach (Enemy enemy in enemies)
                    {
                        if (enemy.name == one)
                        {
                            enemy.name = correct;
                        }
                    }
                }


            }
        }
    }
}