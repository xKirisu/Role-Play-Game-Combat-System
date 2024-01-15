using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace rpgcs
{
    internal class Character : Unit
    {
        public Character(string name, Statistic atributes, string spell1, string spell2, string spell3) : base(name, atributes, spell1, spell2, spell3) { }


        public static Character[] Fabric(){
            return new Character[] { 
                                                //STR INT VIT MANA DEF RES SPD LCK
                new Character("Tank",       new Statistic(2,2,30, 8,3,4,1,2),  "Condamnation", "Protection", "Distraction"),
                new Character("Knight",     new Statistic(3,2,20,10,2,2,3,4),   "Decapitation", "Punishment", "Order_Attack"),
                new Character("Mage",       new Statistic(1,3,15,12,1,3,4,2),   "Fire_Ball", "Lightning", "Sorrow"),
                new Character("Priest",     new Statistic(1,4,15,14,1,2,3,3),  "Heal", "Blessing", "Divine_Chastiment")
            };
        }

        public override void TakeAnAction(List<Unit> queue, Dice dice)
        {
            base.TakeAnAction(queue, dice);

            Console.WriteLine("Spells:");
            foreach (string spell in SpellBook)
            {
                string dash = new string(' ', 20 - spell.Length);

                Console.WriteLine($"\t{spell}{dash}(-{Magic.Factory(spell).cost})");
            }

            while (true)
            {
                bool success = true;
                try
                {
                    Unit target = null;


                    Console.WriteLine("\nTake an action:");
                    string action = Console.ReadLine();

                    string[] actionParts = action.Split(' ');

                    if (actionParts.Length != 2)
                    {
                        throw new FormatException("Invalid command format");
                    }

                    //Target finding
                    string targetName = actionParts[1];
                    target = queue.FirstOrDefault(unit => unit.name == targetName);

                    //Spell finding
                    byte spelli = 0;
                    string spellName = actionParts[0];
                    while (spelli < 4)
                    {
                        if (spellName == SpellBook[spelli])
                        {
                            break;
                        }
                        spelli++;
                    }


                    // Cast or not
                    if (target != null && spelli < 3)
                    {
                        // Mana collector
                        if (Magic.Factory(SpellBook[spelli]).cost > this.Atributes.mana)
                        {
                            throw new FormatException("Not enough mana");
                        }
                        else
                        {
                            this.Atributes.mana -= Magic.Factory(SpellBook[spelli]).cost;
                            Magic.Cast(this, target, spelli, dice.d6(), dice.d20());
                        }
                    }
                    else
                    {
                        success = false;
                        Console.WriteLine("Invalid target or spell.");
                    }
                }
                catch (FormatException e)
                {
                    success = false;
                    if (!string.IsNullOrEmpty(e.Message))
                    {
                        Console.WriteLine($"{e.Message}");
                    }
                    else
                    {
                        Console.WriteLine($"Wrong command");
                    }
                }
                catch (IndexOutOfRangeException ex)
                {
                    success = false;
                    Console.WriteLine($"Wrong spell");
                }
                catch (Exception err)
                {
                    success = false;
                    Console.WriteLine($"An error occurred: {err.Message}");
                }

                if(success)
                {
                    break;
                }
            }

            PressToContinue();
            
        }
    }
}
