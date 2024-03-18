using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rpgcs
{
    internal class Enemy : Unit
    {
        public Enemy(string name, Statistic atributes, string spell1, string spell2, string spell3) : base(name, atributes, spell1, spell2, spell3) { }
        public Enemy(Enemy enemy) : base(enemy.name, enemy.Atributes, enemy.SpellBook[1], enemy.SpellBook[2], enemy.SpellBook[3]) { }

        public static Enemy[] Fabric()
        {
            return new Enemy[]
            {
                                                    //STR INT VIT MANA DEF RES SPD LCK
                new Enemy("Slime",      new Statistic(2,3,  15,8,   2,2,    1,2), "Attack", "Regenerate_Mucus", "Toxic_Ooze"),
                new Enemy("Potato",     new Statistic(2,2,  8, 8,   1,1,    3,1), "Attack", "Perish", "Attack"),
                new Enemy("Goblin",     new Statistic(2,2,  12,8,   3,1,    2,3), "Attack", "Swat", "Order_Attack"),
                new Enemy("Ice_Mage",   new Statistic(2,2,  15,8,   2,4,    2,2), "Ice_Shard", "Hail", "Black_Tome"),
                new Enemy("Reaper",     new Statistic(2,2,  22,8,   2,3,    3,4), "Unshod_Shadow", "Deathly_Reap", "Virgin_Fate"),
                new Enemy("Trickster",  new Statistic(2,2,  12,8,   4,4,    1,3), "Attack", "Trick", "Toxic_Dagger")
            };
        }

        void AI(List<Unit> queue, Dice dice)
        {
            byte spelli = (byte)(dice.random(4) - 1);

            // Mana checker
            if (Magic.Factory(SpellBook[spelli]).cost > this.Atributes.mana)
            {
                spelli = 0;
            }
            else
            {
                this.Atributes.mana -= Magic.Factory(SpellBook[spelli]).cost;
            }

            // Target select
            if (Magic.Factory(SpellBook[spelli]).is_offensive)
            {
                List<Unit> targets_list = new List<Unit>();

                foreach (Unit target in queue)
                {
                    if (target is Character)
                    {
                        if (target.Status == Status.Distracting)
                        {
                            Magic.Cast(this, target, spelli, dice.d6(), dice.d20());
                            break;
                        }
                        else
                        {
                            if(target.Status != Status.Fainted)
                                targets_list.Add(target);
                        }
                    }
                }

                if(targets_list.Any()) 
                    Magic.Cast(this, targets_list[dice.random(targets_list.Count) - 1], spelli, dice.d6(), dice.d20());
            }
            else
            {
                List<Unit> targets_list = new List<Unit>();

                foreach (Unit target in queue)
                {
                    if ((target is Enemy)&&(target.Status != Status.Fainted))
                    {
                        targets_list.Add(target);
                    }
                }

                Magic.Cast(this, targets_list[dice.random(targets_list.Count) - 1], spelli, dice.d6(), dice.d20());
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
