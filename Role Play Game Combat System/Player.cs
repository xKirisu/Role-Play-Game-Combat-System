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
                new Character("Knight",     new Statistic(3,2,20,10,2,2,3,4),   new Magic("Decapitation", "Punishment", "Atack Order")),
                new Character("Mage",       new Statistic(1,3,15,12,1,3,4,2),   new Magic("Fire Ball", "Lightning", "Sorrow")),
                new Character("Priest",     new Statistic(1,4,15,14,1,2,3,3),   new Magic("Heal", "Blessing", "Divine Chastiment"))
            };
        }

        public override void TakeAnAction(List<Unit> queue, Dice dice)
        {
            base.TakeAnAction(queue, dice);

            Console.WriteLine("Spells:");
            foreach (KeyValuePair<string, Spell> spell in Spellbook.spellslot)
            {
                Console.WriteLine($"\t{spell.Value.name}");
            }

            Unit target = this;
            string spell_name = "Atack";
            string target_name = "";

            bool correct = false;
            while(correct == false)
            {
                Console.WriteLine("\nTake and action:");
                string action = Console.ReadLine();
                
                string[] splited_action = action.Split(' ');

                if (Spellbook.spellslot.ContainsKey(splited_action[0]))
                {
                    spell_name = action.Split(' ')[0];

                    for(int i = 1; i < splited_action.Length; i++)
                    {
                        if(i == splited_action.Length-1)
                        {
                            target_name += splited_action[i];
                        }
                        else
                        {
                            target_name += splited_action[i] + " ";
                        }
                        
                    }

                }
                else if (Spellbook.spellslot.ContainsKey(action.Split(' ')[0] + action.Split(' ')[1]))
                {
                    spell_name = action.Split(' ')[0] + action.Split(' ')[1];

                    for (int i = 2; i < splited_action.Length; i++)
                    {
                        if (i == splited_action.Length-1)
                        {
                            target_name += splited_action[i];
                        }
                        else
                        {
                            target_name += splited_action[i] + " ";
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Wrong spell name");
                }

                // Check enemy avaliable
                foreach(Unit check_target in queue)
                {
                    if(check_target.name == target_name)
                    {
                        target = check_target;
                        correct = true;
                        break;
                    }
                }
                if(correct == false)
                {
                    Console.WriteLine("Wrong target name");

                    Console.WriteLine($"DEBUG: {target.name}");
                    Console.WriteLine($"DEBUG: {target_name}spacae");
                    Console.WriteLine($"DEBUG: {spell_name}");
                    Console.WriteLine($"DEBUG: {Spellbook.spellslot[spell_name].name}");
                }
            }
            if (correct)
            {
                Magic.Cast(this, target, Spellbook.spellslot[spell_name], dice.d6(), dice.d20());
            }

            PressToContinue();
        }
    }
}
