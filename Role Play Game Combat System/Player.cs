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
        public Character(string name, Statistic atributes, Magic spellbook) : base(name, atributes, spellbook) { }


        public static Character[] Fabric(){
            return new Character[] { 
                new Character("Tank",       new Statistic(2,2,30, 8,3,4,1,2),   new Magic("Condamnation", "Protection", "Distraction")),
                new Character("Knight",     new Statistic(3,2,20,10,2,2,3,4),   new Magic("Decapitation", "Punishment", "Order_Atack")),
                new Character("Mage",       new Statistic(1,3,15,12,1,3,4,2),   new Magic("Fire_Ball", "Lightning", "Sorrow")),
                new Character("Priest",     new Statistic(1,4,15,14,1,2,3,3),   new Magic("Heal", "Blessing", "Divine_Chastiment"))
            };
        }

        public override void TakeAnAction(List<Unit> queue, Dice dice)
        {
            base.TakeAnAction(queue, dice);

            Console.WriteLine("Spells:");
            foreach (KeyValuePair<string, Spell> spellEntry in Spellbook.spellslot)
            {
                Console.WriteLine($"\t{spellEntry.Value.name}");
            }

            while (true)
            {
                bool success = true;
                try
                {
                    Unit target = null;
                    Spell? spell = null;

                    Console.WriteLine("\nTake an action:");
                    string action = Console.ReadLine();

                    string[] actionParts = action.Split(' ');
                    if (actionParts.Length != 2)
                    {
                        throw new FormatException("Invalid command format");
                    }

                    string spellName = actionParts[0];
                    string targetName = actionParts[1];

                    target = queue.FirstOrDefault(unit => unit.name == targetName);

                    if (Spellbook.spellslot.TryGetValue(spellName, out Spell foundSpell))
                    {
                        spell = foundSpell;
                    }

                    if (target != null && spell != null)
                    {
                        Magic.Cast(this, target, spell.Value, dice.d6(), dice.d20());
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
                    Console.WriteLine($"Wrong command");
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
